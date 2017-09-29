using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJtiCalculator.ViewModels;
using Moq;
using SimpleJtiCalculator.Models;
using SimpleJtiCalculator.Services;
using Assert = NUnit.Framework.Assert;

namespace SimpleJtiCalculatorTests
{
    /// <summary>
    /// This class contains all CalculatorViewModel unit tests
    ///</summary>
    [TestClass()]
    public class CalculatorViewModelTest
    {
        #region Properties

        private Mock<ICalculatorService> ServiceMock { get; set; }
        private CalculatorViewModel      ViewModel   { get; set; }

        #endregion

        [TestInitialize()]
        public void Initialize()
        {
            ServiceMock = new Mock<ICalculatorService>();
            ViewModel   = new CalculatorViewModel(ServiceMock.Object);
        }

        /// <summary>
        /// Verify that all expected mock calls are effectively called.
        /// </summary>
        [TestCleanup]
        public void VerifyAllMocks()
        {
            ServiceMock.VerifyAll();
        }

        /// <summary>
        /// A test for the CalculatorViewModel constructor
        ///</summary>
        [TestMethod]
        public void CalculatorViewModel_ConstructorTest()
        {
            Assert.IsTrue(ViewModel.GetType() == typeof(CalculatorViewModel));
            Assert.AreEqual(ViewModel.Display,        "0");
            Assert.AreEqual(ViewModel.FirstOperand,   string.Empty);
            Assert.AreEqual(ViewModel.SecondOperand,  string.Empty);
            Assert.AreEqual(ViewModel.Operation,      string.Empty);
            Assert.AreEqual(ViewModel.LastOperation,  string.Empty);
            Assert.AreEqual(ViewModel.FullExpression, string.Empty);
        }

        /// <summary>
        /// A test for the CalculatorViewModel valid operation using button inputs
        ///</summary>
        [TestMethod]
        public void CalculatorViewModel_ValidOperationTest()
        {
            // try -7 + 42 = 35

            // Arrange
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "0", "7")).Returns("7");
            ServiceMock.Setup(x => x.ButtonPlusMinusAction("7")).Returns("-7");
            ServiceMock.Setup(x => x.ButtonDigitAction(true, "-7", "4")).Returns("4");
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "4", "2")).Returns("42");
            ServiceMock.Setup(x => x.CalculateResult(It.Is<CalculationModel>(
                p => p.FirstOperand == "-7" &&
                p.Operation == "+" &&
                p.SecondOperand == "42"
                ))).Callback(() => ViewModel.Result = "35");
           
            // Act
            ViewModel.DigitButtonPress("7");
            ViewModel.DigitButtonPress("+/-");

            ViewModel.OperationButtonPress("+");
            ViewModel.DigitButtonPress("4");
            ViewModel.DigitButtonPress("2");

            // Assert
            Assert.AreEqual(ViewModel.FirstOperand, "-7");
            Assert.AreEqual(ViewModel.Display, "42");

            // Act
            ViewModel.OperationButtonPress("=");

            // Assert
            Assert.AreEqual(ViewModel.Operation, "+");
            Assert.AreEqual(ViewModel.SecondOperand, "42");

            Assert.AreEqual(ViewModel.Display, "35");
            Assert.AreEqual(ViewModel.FullExpression, "-7 + 42 = 35");
        }
        
        /// <summary>
        /// A test for the CalculatorViewModel valid operation using button inputs and ClearButton after
        ///</summary>
        [TestMethod]
        public void CalculatorViewModel_ValidOperationTestThenClear()
        {
            // try -7 + 42 = 35
            // Arrange
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "0", "7")).Returns("7");
            ServiceMock.Setup(x => x.ButtonPlusMinusAction("7")).Returns("-7");
            ServiceMock.Setup(x => x.ButtonDigitAction(true, "-7", "4")).Returns("4");
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "4", "2")).Returns("42");
            ServiceMock.Setup(x => x.CalculateResult(It.Is<CalculationModel>(
                p => p.FirstOperand == "-7" &&
                p.Operation == "+" &&
                p.SecondOperand == "42"
                ))).Callback(() => ViewModel.Result = "35");

            // Act
            ViewModel.DigitButtonPress("7");
            ViewModel.DigitButtonPress("+/-");

            ViewModel.OperationButtonPress("+");
            ViewModel.DigitButtonPress("4");
            ViewModel.DigitButtonPress("2");

            // Assert
            Assert.AreEqual(ViewModel.FirstOperand, "-7");
            Assert.AreEqual(ViewModel.Display, "42");
            // Act
            ViewModel.OperationButtonPress("=");
            // Assert
            Assert.AreEqual(ViewModel.Operation, "+");
            Assert.AreEqual(ViewModel.SecondOperand, "42");

            Assert.AreEqual(ViewModel.Display, "35");
            Assert.AreEqual(ViewModel.FullExpression, "-7 + 42 = 35");

            // Act
            // ClearAll
            //ServiceMock.Setup(x => x.ButtonDigitAction(false, "35", "C")).Returns("0");
            ViewModel.DigitButtonPress("C");
            // Assert
            Assert.AreEqual(ViewModel.Display, "0");
            Assert.AreEqual(ViewModel.FirstOperand,   string.Empty);
            Assert.AreEqual(ViewModel.SecondOperand,  string.Empty);
            Assert.AreEqual(ViewModel.Operation,      string.Empty);
            Assert.AreEqual(ViewModel.LastOperation,  string.Empty);
            Assert.AreEqual(ViewModel.FullExpression, string.Empty);
        }
        
        /// <summary>
        /// A test for the CalculatorViewModel invalid operation using button inputs
        ///</summary>
        [TestMethod]
        public void CalculatorViewModel_InValidOperationTest()
        {
            // try -7 + 42 = 35
            // Arrange
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "0", "7")).Returns("7");
            ServiceMock.Setup(x => x.ButtonPlusMinusAction("7")).Returns("-7");
            ServiceMock.Setup(x => x.ButtonDigitAction(true, "-7", "4")).Returns("4");
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "4", "2")).Returns("42");
            ServiceMock.Setup(x => x.CalculateResult(It.Is<CalculationModel>(
                p => p.FirstOperand == "-7" &&
                p.Operation == "@" &&
                p.SecondOperand == "42"
                ))).Callback(() => ViewModel.Result = "Error: Unknown operation: @");

            // Act
            ViewModel.DigitButtonPress("7");
            ViewModel.DigitButtonPress("+/-");

            ViewModel.OperationButtonPress("@");
            ViewModel.DigitButtonPress("4");
            ViewModel.DigitButtonPress("2");

            // Assert
            Assert.AreEqual(ViewModel.FirstOperand, "-7");
            Assert.AreEqual(ViewModel.Display, "42");

            // Act
            ViewModel.OperationButtonPress("=");
            // Assert
            Assert.AreEqual(ViewModel.Operation, "@");
            Assert.AreEqual(ViewModel.SecondOperand, "42");

            Assert.AreEqual(ViewModel.Display, "Error: Unknown operation: @");
        }

        /// <summary>
        /// A test for the CalculatorViewModel valid composite operations using button inputs
        ///</summary>
        [TestMethod]
        public void CalculatorViewModel_CompositeOperationTestThenClear()
        {
            // try -7 + 42 = 35
            // Arrange
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "0", "7")).Returns("7");
            ServiceMock.Setup(x => x.ButtonPlusMinusAction("7")).Returns("-7");
            ServiceMock.Setup(x => x.ButtonDigitAction(true, "-7", "4")).Returns("4");
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "4", "2")).Returns("42");

            ServiceMock.Setup(x => x.CalculateResult(It.Is<CalculationModel>(
                p => p.FirstOperand == "-7" &&
                p.Operation == "+" &&
                p.SecondOperand == "42"
                ))).Callback(() => ViewModel.Result = "35");


            ViewModel.DigitButtonPress("7");
            ViewModel.DigitButtonPress("+/-");

            ViewModel.OperationButtonPress("+");
            ViewModel.DigitButtonPress("4");
            ViewModel.DigitButtonPress("2");

            // Assert
            Assert.AreEqual(ViewModel.FirstOperand, "-7");
            Assert.AreEqual(ViewModel.Display, "42");
            // Act
            ViewModel.OperationButtonPress("+");

            // Assert
            Assert.AreEqual(ViewModel.Operation, "+");
            Assert.AreEqual(ViewModel.SecondOperand, "42");

            Assert.AreEqual(ViewModel.Display, "35");
            Assert.AreEqual(ViewModel.FullExpression, "-7 + 42 = 35");
            
            // Arrange
            ServiceMock.Setup(x => x.ButtonDigitAction(true, "35", "1")).Returns("1");
            ServiceMock.Setup(x => x.ButtonDigitAction(false, "1", "0")).Returns("10");

            ServiceMock.Setup(x => x.CalculateResult(It.Is<CalculationModel>(
                p => p.FirstOperand == "35" &&
                p.Operation == "+" &&
                p.SecondOperand == "10"
                ))).Callback(() => ViewModel.Result = "45");
            // Act
            ViewModel.DigitButtonPress("1");
            ViewModel.DigitButtonPress("0");

            ViewModel.OperationButtonPress("=");

            // Assert
            Assert.AreEqual(ViewModel.Display, "45");
            Assert.AreEqual(ViewModel.FullExpression, "35 + 10 = 45");
        }

        // TODO JTI cover ButtonCE, ButtonDel, ButtonPeriod, M+, M-, etc
    }
}