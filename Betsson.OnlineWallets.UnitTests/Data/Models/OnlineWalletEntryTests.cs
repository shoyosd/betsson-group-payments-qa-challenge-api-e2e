using NUnit.Framework;
using System;
using Betsson.OnlineWallets.Data.Models;

namespace Betsson.OnlineWallets.UnitTests.Data.Models
{
    [TestFixture]
    public class OnlineWalletEntryTests
    {
        [Test]
        public void OnlineWalletEntry_ShouldInitializeWithDefaultValues()
        {
            var entry = new OnlineWalletEntry();

            Assert.IsNotNull(entry.Id);
            Assert.That(entry.Id.Length, Is.EqualTo(Guid.NewGuid().ToString().Length)); // Check if it's a valid GUID
            Assert.That(entry.EventTime, Is.EqualTo(DateTimeOffset.Now).Within(TimeSpan.FromSeconds(1)));
        }
    }
}
