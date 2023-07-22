using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockPortfolio.Core.Enums;

namespace StockPortfolio.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(TransactionType.Type.Buy, "Buy");
        }
    }
}