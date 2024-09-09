0using NUnit.Framework;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Endpoints;
using Betsson.OnlineWallets.Tests.Helpers;

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
            await _withdrawEndpoint.WithdrawAsync(ValidWithdrawAmount);
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, -ValidWithdrawAmount, "The balance should be updated correctly after a valid withdrawal.");
        }

        [Test]
        public async Task WithdrawMoreThanAvailable_ShouldReturnErrorAndNotUpdateBalance()
        {
            var response = await _withdrawEndpoint.WithdrawAsync(ExcessiveWithdrawAmount);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for an excessive withdrawal.");
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, 0, "The balance should remain unchanged after an attempt to withdraw more than available.");
        }

        [Test]
        public async Task WithdrawNegativeAmount_ShouldReturnErrorAndNotUpdateBalance()
        {
            var response = await _withdrawEndpoint.WithdrawAsync(NegativeWithdrawAmount);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for a negative withdrawal.");
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, 0, "The balance should remain unchanged after an attempt to withdraw a negative amount.");
        }

        [Test]
        public async Task WithdrawFullBalance_ShouldReturn200OkAndSetBalanceToZero()
        {
            await _withdrawEndpoint.WithdrawAsync(_initialBalance);
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, -_initialBalance, "The balance should be 0 after withdrawing the full amount.");
        }
    }
}
