using NUnit.Framework;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Endpoints;
using Betsson.OnlineWallets.Tests.Helpers;
using Betsson.OnlineWallets.ApiTestsE2E.TestData;

namespace Betsson.OnlineWallets.Tests.TestCases
{
    [TestFixture]
    public class DepositTests
    {
        private BalanceEndpoint _balanceEndpoint;
        private DepositEndpoint _depositEndpoint;

        // Constants for deposit amounts
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
            await _depositEndpoint.DepositAsync(ValidDepositAmount);
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, ValidDepositAmount, $"The balance should be {_initialBalance + ValidDepositAmount} after the deposit.");
        }

        [Test]
        public async Task DepositNegativeAmount_ShouldReturnErrorAndNotUpdateBalance()
        {
            var response = await _depositEndpoint.DepositAsync(NegativeDepositAmount);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for a negative deposit.");
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, 0, "The balance should remain unchanged after a failed negative deposit.");
        }

        [Test]
        public async Task DepositZeroAmount_ShouldReturnErrorAndNotUpdateBalance()
        {
            var response = await _depositEndpoint.DepositAsync(ZeroDepositAmount);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest), "The server should return a 400 Bad Request for a deposit of zero.");
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, 0, "The balance should remain unchanged after a failed zero deposit.");
        }

        [Test]
        public async Task MultipleDeposits_ShouldUpdateBalanceCorrectly()
        {
            await _depositEndpoint.DepositAsync(FirstDepositAmount);
            await _depositEndpoint.DepositAsync(SecondDepositAmount);
            await WalletTestHelper.ValidateBalanceAfterOperation(_balanceEndpoint, _initialBalance, FirstDepositAmount + SecondDepositAmount, $"The balance should be {_initialBalance + FirstDepositAmount + SecondDepositAmount} after multiple deposits.");
        }
    }
}
