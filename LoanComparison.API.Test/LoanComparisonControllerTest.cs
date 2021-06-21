using LoanComparison.API.Controllers;
using LoanComparison.Common.DTO;
using LoanComparison.Service.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LoanComparison.API.Test
{
    [TestClass]
    public class LoanComparisonControllerTest
    {

        [TestMethod]
        public void GetSavingsAmount_ReturnsDecimal()
        {
            //create mock ILoanComparisonService
            var mock = new Mock<ILoanComparisonService>();
            mock.Setup(service =>service.GetSavingAmountAsync(It.IsAny<LoanDetail>())).ReturnsAsync((decimal)25000.25);
            //create controller injecting the mock ILoanComparisonService
            LoanComparisonController controller = new LoanComparisonController(mock.Object);
            decimal result =   controller.GetSavingAmount(new LoanDetail() { BorrowingAmount=100000, CustomerRate=0.25}).Result;
            Assert.AreEqual((decimal)25000.25, result);
        }
    }
}
