using NUnit.Framework;
using System.Threading.Tasks;
using Betsson.OnlineWallets.Tests.Endpoints;
using Betsson.OnlineWallets.Tests.Models;

namespace Betsson.OnlineWallets.Tests.TestCases
{
    [TestFixture]
    public class BalanceTests
    {
        private BalanceEndpoint _balanceEndpoint;
        private DepositEndpoint _depositEndpoint;
        private WithdrawEndpoint _withdrawEndpoint;
        private decimal _initialBalance;

        // Constants for deposit and withdrawal amounts
        private const decimal DepositAmount100 = 100m;
        private const decimal DepositAmount200 = 200m;
        private const decimal WithdrawAmount50 = 50m;
        private const decimal WithdrawAmount100 = 100m;

        [SetUp]
        public async Task Setup()
        {
            _balanceEndpoint = new BalanceEndpoint();
            _depositEndpoint = new DepositEndpoint();
            _withdrawEndpoint = new WithdrawEndpoint();

            // Store the initial balance value before each test
            _initialBalance = await _balanceEndpoint.GetBalanceAmountAsync();
        }

        [Test]
        public async Task GetInitialBalance_ShouldReturn200Ok()
        {
            var response = await _balanceEndpoint.GetBalanceAsync();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK), "The HTTP status code is not 200 OK.");
        }

        [Test]
        public async Task Deposit_ShouldIncreaseBalanceBy100()
        {
            await _depositEndpoint.DepositAsync(DepositAmount100);

            // Verify that the expected balance is the initial balance + the deposit of 100
            await VerifyBalanceChange(DepositAmount100);
        }

        [Test]
        public async Task Withdraw_ShouldDecreaseBalanceBy50()
        {
            // Make an initial deposit to ensure sufficient funds
            await _depositEndpoint.DepositAsync(DepositAmount100);

            // Perform the withdrawal of 50
            await _withdrawEndpoint.WithdrawAsync(WithdrawAmount50);

            // Verify that the balance has been adjusted correctly
            await VerifyBalanceChange(DepositAmount100 - WithdrawAmount50);
        }

        [Test]
        public async Task MultipleDepositsAndWithdrawals_ShouldReflectCorrectBalance()
        {
            await _depositEndpoint.DepositAsync(DepositAmount100);
            await _depositEndpoint.DepositAsync(DepositAmount200);
            await _withdrawEndpoint.WithdrawAsync(WithdrawAmount50);
            await _withdrawEndpoint.WithdrawAsync(WithdrawAmount100);

            decimal expectedBalanceChange = DepositAmount100 + DepositAmount200 - WithdrawAmount50 - WithdrawAmount100;
            await VerifyBalanceChange(expectedBalanceChange);
        }

        // Helper method to verify the change in balance
        private async Task VerifyBalanceChange(decimal expectedChange)
        {
            decimal newBalance = await _balanceEndpoint.GetBalanceAmountAsync();
            decimal expectedBalance = _initialBalance + expectedChange;
            Assert.That(newBalance, Is.EqualTo(expectedBalance), $"The expected balance is {expectedBalance}, but the actual balance is {newBalance}.");
        }
    }
}
