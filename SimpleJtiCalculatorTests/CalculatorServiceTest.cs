using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleJtiCalculator.Models;
using SimpleJtiCalculator.Services;
using Assert = NUnit.Framework.Assert;

namespace SimpleJtiCalculatorTests
{
    /// <summary>
    /// This class contains all CalculatorService unit tests
    ///</summary>
    [TestClass()]
    public class CalculatorServiceTest
    {
        #region Properties

        private CalculatorService Service { get; set; }

        #endregion

        [TestInitialize]
        public void Initialize()
        {
            Service = new CalculatorService();
        }

        #region DeleteLastInput

        [TestMethod]
        public void DeleteLastInput_ValidOperation_WithInputNot0()
        {
            // Arrange
            var displayedValue = "100";
            var expectedValue  = "10";
            // Act
            var res = Service.DeleteLastInput(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void DeleteLastInput_ValidOperation_WithInput0()
        {
            // Arrange
            var displayedValue = "0";
            var expectedValue  = "0";
            // Act
            var res = Service.DeleteLastInput(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void DeleteLastInput_ValidOperation_WithInputNull()
        {
            // Arrange
            var displayedValue = "";
            var expectedValue  = "0";
            // Act
            var res = Service.DeleteLastInput(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }
        #endregion

        #region ButtonPlusMinusAction

        [TestMethod]
        public void ButtonPlusMinusAction_ValidOperation_WithInputPosNumber()
        {
            // Arrange
            var displayedValue = "100";
            var expectedValue  = "-100";
            // Act
            var res = Service.ButtonPlusMinusAction(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonPlusMinusAction_ValidOperation_WithInputNegNumber()
        {
            // Arrange
            var displayedValue = "-100";
            var expectedValue  = "100";
            // Act
            var res = Service.ButtonPlusMinusAction(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonPlusMinusAction_ValidOperation_WithInputZero()
        {
            // Arrange
            var displayedValue = "0";
            var expectedValue  = "0";
            // Act
            var res = Service.ButtonPlusMinusAction(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonPlusMinusAction_ValidOperation_WithEmptyInput()
        {
            // Arrange
            var displayedValue = "";
            var expectedValue  = "0";
            // Act
            var res = Service.ButtonPlusMinusAction(displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        #endregion

        #region ButtonPeriodAction
        [TestMethod]
        public void ButtonPeriodAction_ValidOperation_WithEmptyInput()
        {
            // Arrange
            var newDisplayRequired = true;
            var displayedValue     = "";
            var expectedValue      = "0,";
            // Act
            var res = Service.ButtonPeriodAction(newDisplayRequired, displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonPeriodAction_ValidOperation_WithInputIsZero()
        {
            // Arrange
            var newDisplayRequired = true;
            var displayedValue     = "0";
            var expectedValue      = "0,";
            // Act
            var res = Service.ButtonPeriodAction(newDisplayRequired, displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonPeriodAction_ValidOperation_WithInputAlreadyHasPeriod()
        {
            // Arrange
            var newDisplayRequired = false;
            var displayedValue     = "10,23";
            var expectedValue      = "10,23";
            // Act
            var res = Service.ButtonPeriodAction(newDisplayRequired, displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonPeriodAction_ValidOperation_WithInputAlreadyDoestNotHasPeriod()
        {
            // Arrange
            var newDisplayRequired = false;
            var displayedValue     = "10";
            var expectedValue      = "10,";
            // Act
            var res = Service.ButtonPeriodAction(newDisplayRequired, displayedValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }
        #endregion

        #region ButtonDigitAction

        [TestMethod]
        public void ButtonDigitAction_ValidOperation()
        {
            // Arrange
            var newDisplayRequired = true;
            var displayedValue     = "";
            var buttonValue        = "2";
            var expectedValue      = "2";
            // Act
            var res = Service.ButtonDigitAction(newDisplayRequired, displayedValue, buttonValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }

        [TestMethod]
        public void ButtonDigitAction_ValidOperation_WithTwoDigits()
        {
            // Arrange
            var newDisplayRequired = false;
            var displayedValue     = "2";
            var buttonValue        = "3";
            var expectedValue      = "23";
            // Act
            var res = Service.ButtonDigitAction(newDisplayRequired, displayedValue, buttonValue);
            // Assert
            Assert.AreEqual(expectedValue, res);
        }
        #endregion

        #region CalculateResult

        [TestMethod]
        public void CalculateResult_ValidOperation_Addition()
        {
            // Arrange
            var model = new CalculationModel
            {
                FirstOperand  = "3",
                SecondOperand = "10",
                Operation     = "+"
            };
            // Act
            Service.CalculateResult(model);
            // Assert
            Assert.AreEqual(model.FirstOperand,  "3");
            Assert.AreEqual(model.SecondOperand, "10");
            Assert.AreEqual(model.Operation,     "+");
            Assert.AreEqual(model.Result,        "13");
        }

        [TestMethod]
        public void CalculateResult_ValidOperation_Substraction()
        {
            // Arrange
            var model = new CalculationModel
            {
                FirstOperand  = "3",
                SecondOperand = "10",
                Operation     = "-"
            };
            // Act
            Service.CalculateResult(model);
            // Assert
            Assert.AreEqual(model.FirstOperand,  "3");
            Assert.AreEqual(model.SecondOperand, "10");
            Assert.AreEqual(model.Operation,     "-");
            Assert.AreEqual(model.Result,        "-7");
        }
        
        [TestMethod]
        public void CalculateResult_ValidOperation_Multiplication()
        {
            // Arrange
            var model = new CalculationModel
            {
                FirstOperand  = "3",
                SecondOperand = "10",
                Operation     = "*"
            };
            // Act
            Service.CalculateResult(model);
            // Assert
            Assert.AreEqual(model.FirstOperand,  "3");
            Assert.AreEqual(model.SecondOperand, "10");
            Assert.AreEqual(model.Operation,     "*");
            Assert.AreEqual(model.Result,        "30");
        }
        
        [TestMethod]
        public void CalculateResult_ValidOperation_Division()
        {
            // Arrange
            var model = new CalculationModel
            {
                FirstOperand  = "9",
                SecondOperand = "3",
                Operation     = "/"
            };
            // Act
            Service.CalculateResult(model);
            // Assert
            Assert.AreEqual(model.FirstOperand,  "9");
            Assert.AreEqual(model.SecondOperand, "3");
            Assert.AreEqual(model.Operation,     "/");
            Assert.AreEqual(model.Result,        "3");
        }

        [TestMethod]
        public void CalculateResult_ValidOperation_Division_CannotDivideByZero()
        {
            // Arrange
            var model = new CalculationModel
            {
                FirstOperand  = "9",
                SecondOperand = "0",
                Operation     = "/"
            };
            // Act
            Service.CalculateResult(model);
            // Assert
            Assert.AreEqual(model.FirstOperand,  "9");
            Assert.AreEqual(model.SecondOperand, "0");
            Assert.AreEqual(model.Operation,     "/");
            Assert.IsTrue(model.Result.Contains("Cannot divide by 0"));
        }
        #endregion 
    }
}
