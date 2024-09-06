using NUnit.Framework;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Endpoints;

namespace Betsson.OnlineWallets.Tests.TestCases
{
    [TestFixture]
    public class WithdrawTests
    {
        private BalanceEndpoint _balanceEndpoint;
        private DepositEndpoint _depositEndpoint;
        private WithdrawEndpoint _withdrawEndpoint;

        // Constants for deposit and withdrawal amounts
        private const decimal ValidDepositAmount = 100m;
        private const decimal ValidWithdrawAmount = 50m;
        private const decimal ExcessiveWithdrawAmount = 150m;
        private const decimal NegativeWithdrawAmount = -50m;
        private decimal _initialBalance;

        [SetUp]
        public async Task Setup()
        {
            _balanceEndpoint = new BalanceEndpoint();
            _depositEndpoint = new DepositEndpoint();
            _withdrawEndpoint = new WithdrawEndpoint();

            // Perform an initial deposit to ensure sufficient balance for tests
            await _depositEndpoint.DepositAsync(ValidDepositAmount);

            // Store the initial balance before each test
            _initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
        }

        [Test]
        public async Task ValidWithdraw_ShouldReturn200OkAndUpdateBalance()
        {
            // Perform a valid withdrawal
            await _withdrawEndpoint.WithdrawAsync(ValidWithdrawAmount);

            // Get the new balance
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();

            // Verify that the balance has decreased correctly
            var expectedBalance = _initialBalance - ValidWithdrawAmount;
            Assert.That(newBalance, Is.EqualTo(expectedBalance), $"The balance should be {expectedBalance} after the withdrawal, but it is {newBalance}.");
        }

        [Test]
        public async Task WithdrawMoreThanAvailable_ShouldReturnErrorAndNotUpdateBalance()
        {
            // Attempt to withdraw more than the current balance
            var response = await _withdrawEndpoint.WithdrawAsync(ExcessiveWithdrawAmount);

            // Verify that the server returned a 400 Bad Request or similar error
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for an excessive withdrawal.");

            // Get the new balance
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();

            // Verify that the balance has not changed
            Assert.That(newBalance, Is.EqualTo(_initialBalance), $"The balance should remain {_initialBalance} after a failed withdrawal attempt, but it is {newBalance}.");
        }

        [Test]
        public async Task WithdrawNegativeAmount_ShouldReturnErrorAndNotUpdateBalance()
        {
            // Attempt to perform a negative withdrawal
            var response = await _withdrawEndpoint.WithdrawAsync(NegativeWithdrawAmount);

            // Verify that the server returned a 400 Bad Request or similar error
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for a negative withdrawal.");

            // Get the new balance
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();

            // Verify that the balance has not changed
            Assert.That(newBalance, Is.EqualTo(_initialBalance), $"The balance should remain {_initialBalance} after a failed withdrawal attempt, but it is {newBalance}.");
        }

        [Test]
        public async Task WithdrawFullBalance_ShouldReturn200OkAndSetBalanceToZero()
        {
            // Perform a withdrawal of the full balance
            await _withdrawEndpoint.WithdrawAsync(_initialBalance);

            // Get the new balance
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();

            // Verify that the balance is 0 after the full withdrawal
            Assert.That(newBalance, Is.EqualTo(0), $"The balance should be 0 after withdrawing the full amount, but it is {newBalance}.");
        }
    }
}
