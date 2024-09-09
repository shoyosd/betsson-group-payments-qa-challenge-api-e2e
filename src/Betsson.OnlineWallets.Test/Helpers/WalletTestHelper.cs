using Betsson.OnlineWallets.Tests.Endpoints;

namespace Betsson.OnlineWallets.Tests.Helpers
{
    public static class WalletTestHelper
    {
        // Helper method to validate balance after any operation (deposit/withdrawal)
        public static async Task ValidateBalanceAfterOperation(BalanceEndpoint balanceEndpoint, decimal initialBalance, decimal balanceChange, string errorMessage)
        {
            var newBalance = await balanceEndpoint.GetBalanceAmountAsync();
            var expectedBalance = initialBalance + balanceChange;
            Assert.That(newBalance, Is.EqualTo(expectedBalance), errorMessage);
        }
    }
}
