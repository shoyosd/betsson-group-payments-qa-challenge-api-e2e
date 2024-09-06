using NUnit.Framework;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Endpoints;
using Betsson.OnlineWallets.ApiTestsE2E.TestData;

namespace Betsson.OnlineWallets.Tests.TestCases
{
    [TestFixture]
    public class DepositTests
    {
        private BalanceEndpoint _balanceEndpoint;
        private DepositEndpoint _depositEndpoint;

        private const decimal ValidDepositAmount = 50m;
        private const decimal NegativeDepositAmount = -50m;
        private const decimal ZeroDepositAmount = 0m;
        private const decimal FirstDepositAmount = 100m;
        private const decimal SecondDepositAmount = 200m;

        private decimal _initialBalance;

        [SetUp]
        public async Task Setup()
        {
            _balanceEndpoint = new BalanceEndpoint();
            _depositEndpoint = new DepositEndpoint();

            // Store the initial balance before each test
            _initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
        }

        [Test]
        public async Task ValidDeposit_ShouldReturn200OkAndUpdateBalance()
        {
            var initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            await _depositEndpoint.DepositAsync(ValidDepositAmount);
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();

            var expectedBalance = initialBalance + ValidDepositAmount;
            Assert.That(newBalance, Is.EqualTo(expectedBalance), $"The balance should be {expectedBalance} after the deposit, but it is {newBalance}.");
        }

        [Test]
        public async Task DepositNegativeAmount_ShouldReturnErrorAndNotUpdateBalance()
        {
            var initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            var response = await _depositEndpoint.DepositAsync(NegativeDepositAmount);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for a negative deposit.");
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            Assert.That(newBalance, Is.EqualTo(initialBalance), $"The balance should remain {initialBalance} after a failed deposit attempt, but it is {newBalance}.");
        }

        [Test]
        public async Task DepositZeroAmount_ShouldReturnErrorAndNotUpdateBalance()
        {
            var initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            var response = await _depositEndpoint.DepositAsync(ZeroDepositAmount);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for a deposit of zero.");
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            Assert.That(newBalance, Is.EqualTo(initialBalance), $"The balance should remain {initialBalance} after a failed deposit attempt, but it is {newBalance}.");
        }

        [Test]
        public async Task MultipleDeposits_ShouldUpdateBalanceCorrectly()
        {
            var initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            await _depositEndpoint.DepositAsync(FirstDepositAmount);
            await _depositEndpoint.DepositAsync(SecondDepositAmount);
            var newBalance = await _balanceEndpoint.GetBalanceAmountAsync();

            var expectedBalance = initialBalance + FirstDepositAmount + SecondDepositAmount;
            Assert.That(newBalance, Is.EqualTo(expectedBalance), $"The balance should be {expectedBalance} after multiple deposits, but it is {newBalance}.");
        }
    }
}
