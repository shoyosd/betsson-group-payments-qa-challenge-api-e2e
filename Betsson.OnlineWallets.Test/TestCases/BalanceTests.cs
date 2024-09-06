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

        [SetUp]
        public void Setup()
        {
            _balanceEndpoint = new BalanceEndpoint();
            _depositEndpoint = new DepositEndpoint();
            _withdrawEndpoint = new WithdrawEndpoint();
        }

        [Test]
        public async Task GetInitialBalance_ShouldReturnZero()
        {
            // Escenario 1: Recuperar el balance inicial
            BalanceResponse balance = await _balanceEndpoint.GetBalanceAsync();
            Assert.That(balance.Amount, Is.EqualTo(0), "El balance inicial no es 0.");
        }
        
        [Test]
        public async Task GetBalanceAfterDeposit_ShouldReturnCorrectAmount()
        {
            // Escenario 2: Recuperar el balance después de un depósito
            await _depositEndpoint.DepositAsync(100);
            BalanceResponse balance = await _balanceEndpoint.GetBalanceAsync();
            Assert.That(balance.Amount, Is.EqualTo(100), "El balance después del depósito no es correcto.");
        }

        [Test]
        public async Task GetBalanceAfterWithdrawal_ShouldReturnCorrectAmount()
        {
            // Escenario 3: Recuperar el balance después de un retiro exitoso
            await _depositEndpoint.DepositAsync(100);
            await _withdrawEndpoint.WithdrawAsync(50);
            BalanceResponse balance = await _balanceEndpoint.GetBalanceAsync();
            Assert.That(balance.Amount, Is.EqualTo(50), "El balance después del retiro no es correcto.");
        }

        [Test]
        public async Task GetBalanceAfterMultipleTransactions_ShouldReturnCorrectAmount()
        {
            // Escenario 4: Verificar respuesta al balance después de múltiples depósitos y retiros
            await _depositEndpoint.DepositAsync(100);
            await _depositEndpoint.DepositAsync(200);
            await _withdrawEndpoint.WithdrawAsync(50);
            await _withdrawEndpoint.WithdrawAsync(100);
            BalanceResponse balance = await _balanceEndpoint.GetBalanceAsync();
            Assert.That(balance.Amount, Is.EqualTo(150), "El balance después de múltiples transacciones no es correcto.");
        }
    }
}
