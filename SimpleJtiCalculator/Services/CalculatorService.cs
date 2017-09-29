using System;
using SimpleJtiCalculator.Constants;
using SimpleJtiCalculator.Exception;
using SimpleJtiCalculator.Models;

namespace SimpleJtiCalculator.Services
{
    /// <summary>
    ///     Service containings basic methods for the calculator.
    /// </summary>
    public class CalculatorService : ICalculatorService
    {
        #region Constructors

        public CalculatorService()
        {
        }

        #endregion

        #region Public methods 

        /// <summary>
        ///     Calculate the operation an set the result in the given model.
        /// </summary>
        /// <param name="model"></param>
        public void CalculateResult(CalculationModel model)
        {
            try
            {
                ValidateData(model);
                // TODO JTI format somewhere number of digit after ,
                switch (model.Operation)
                {
                    case (CalculationConst.ButtonPlus):
                        model.Result = OperationAddition(model.FirstOperand, model.SecondOperand);
                        break;

                    case (CalculationConst.ButtonMinus):
                        model.Result = OperationSubstraction(model.FirstOperand, model.SecondOperand);
                        break;

                    case (CalculationConst.ButtonMultiply):
                        model.Result = OperationMultiplication(model.FirstOperand, model.SecondOperand);
                        break;

                    case (CalculationConst.ButtonDevide):
                        model.Result = OperationDivision(model.FirstOperand, model.SecondOperand);
                        break;
                }
            }
            catch (CalculatorException e)
            {
                model.Result = e.Message;
            }
            catch (System.Exception)
            {
                // TODO JTI put in a const/res
                model.Result = BuildErrorMessage("Error whilst calculating");
            }
        }

        /// <summary>
        ///     Delete the last input.
        /// </summary>
        /// <param name="displayedValue"></param>
        /// <returns></returns>
        public string DeleteLastInput(string displayedValue)
        {
            if (string.IsNullOrEmpty(displayedValue))
                return CalculationConst.Display0;

            return displayedValue.Length > 1
                ? displayedValue.Substring(0, displayedValue.Length - 1)
                : CalculationConst.Display0;
        }

        /// <summary>
        ///     Change display value when +/- action is required.
        ///     Remove minus symbol if it exists in displayed value or if the displayed value is 0. Else, add minus symbol.
        /// </summary>
        /// <param name="displayedValue"></param>
        /// <returns></returns>
        public string ButtonPlusMinusAction(string displayedValue)
        {
            if (string.IsNullOrEmpty(displayedValue) || displayedValue == CalculationConst.Display0)
                return CalculationConst.Display0;
            if (displayedValue.Contains(CalculationConst.DisplayMinus))
                return displayedValue.Remove(displayedValue.IndexOf(CalculationConst.DisplayMinus), 1);
            else
                return CalculationConst.DisplayMinus + displayedValue;
        }

        /// <summary>
        ///     Change display value when "period" action is required.
        ///     Add period symbol or removed it.
        /// </summary>
        /// <param name="newDisplayRequired"></param>
        /// <param name="displayedValue"></param>
        /// <returns></returns>
        public string ButtonPeriodAction(bool newDisplayRequired, string displayedValue)
        {
            if (string.IsNullOrEmpty(displayedValue))
                displayedValue = CalculationConst.Display0;

            var newDisplayedValue = displayedValue;

            if (newDisplayRequired)
            {
                newDisplayedValue = CalculationConst.Display0 + CalculationConst.DisplayPeriod;
            }
            else
            {
                if (!displayedValue.Contains(CalculationConst.DisplayPeriod))
                {
                    newDisplayedValue += CalculationConst.DisplayPeriod;
                }
            }

            return newDisplayedValue;
        }

        /// <summary>
        ///     Add a digit to current display.
        /// </summary>
        /// <param name="newDisplayRequired"> Indicates if a new display is required.  </param>
        /// <param name="displayedValue"></param>
        /// <param name="buttonValue"></param>
        /// <returns></returns>
        public string ButtonDigitAction(bool newDisplayRequired, string displayedValue, string buttonValue)
        {
            if (string.IsNullOrEmpty(displayedValue) || displayedValue == CalculationConst.Display0 ||
                newDisplayRequired)
                return buttonValue;
            else
                return displayedValue + buttonValue;
        }

        /// <summary>
        ///     Perform an addition between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Addition result. </returns>
        public string OperationAddition(string firstOperand, string secondOperand)
        {
            // TODO JTI protect against exception if operands are null/empty/cannot be converted to double
            return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
        }

        /// <summary>
        ///     Perform a substraction between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Division result. </returns>
        public string OperationSubstraction(string firstOperand, string secondOperand)
        {
            // TODO JTI protect against exception if operands are null/empty/cannot be converted to double
            return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
        }

        /// <summary>
        ///     Perform a multiplication between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Multiplication result. </returns>
        public string OperationMultiplication(string firstOperand, string secondOperand)
        {
            // TODO JTI protect against exception if operands are null/empty/cannot be converted to double
            return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
        }

        /// <summary>
        ///     Perform a division between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Division result. </returns>
        public string OperationDivision(string firstOperand, string secondOperand)
        {
            // TODO JTI protect against exception if operands are null/empty/cannot be converted to double

            if (secondOperand == CalculationConst.Display0)
                throw new CalculatorException(BuildErrorMessage("Cannot divide by 0"));
            // TODO JTI put in a const/res

            return (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand)).ToString();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// TODO JTI document
        /// </summary>
        /// <param name="operand"></param>
        private void ValidateOperand(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (System.Exception)
            {
                // TODO JTI put in a const/res
                throw new CalculatorException(BuildErrorMessage($"Invalid number: {operand}"));
            }
        }

        /// <summary>
        /// TODO JTI document
        /// </summary>
        /// <param name="model"></param>
        private void ValidateData(CalculationModel model)
        {
            switch (model.Operation)
            {
                case CalculationConst.ButtonDevide:
                case CalculationConst.ButtonMultiply:
                case CalculationConst.ButtonMinus:
                case CalculationConst.ButtonPlus:
                    ValidateOperand(model.FirstOperand);
                    ValidateOperand(model.SecondOperand);
                    break;
                default:
                    // TODO JTI put in a const/res
                    throw new CalculatorException(BuildErrorMessage($"Unknown operation: {model.Operation}"));
            }
        }

        /// <summary>
        /// TODO JTI document
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string BuildErrorMessage(string message)
        {
            return CalculationConst.DisplayError + message;
        }

        #endregion
    }
}