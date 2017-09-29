using SimpleJtiCalculator.Models;

namespace SimpleJtiCalculator.Services
{
    /// <summary>
    ///     Service containings basic methods for the calculator.
    /// </summary>
    public interface ICalculatorService
    {
        /// <summary>
        ///     Calculate the operation an set the result in the given model.
        /// </summary>
        /// <param name="model"></param>
        void CalculateResult (CalculationModel model);

        /// <summary>
        ///     Delete last input.
        ///     If displayed value = 2000, then result of this method is 200.
        /// </summary>
        /// <param name="displayedValue"></param>
        /// <returns></returns>
        string DeleteLastInput(string displayedValue);

        /// <summary>
        ///     Change display value when +/- action is required.
        ///     Remove minus symbol if it exists in displayed value or if the displayed value is 0. Else, add minus symbol.
        /// </summary>
        /// <param name="displayedValue"></param>
        /// <returns></returns>
        string ButtonPlusMinusAction(string displayedValue);

        /// <summary>
        ///     Change display value when "period" action is required.
        ///     Add period symbol or removed it.
        /// </summary>
        /// <param name="newDisplayRequired"> Indicates if a new display is required. </param>
        /// <param name="displayedValue"></param>
        /// <returns></returns>
        string ButtonPeriodAction(bool newDisplayRequired, string displayedValue);

        /// <summary>
        ///     Add a digit to current display.
        /// </summary>
        /// <param name="newDisplayRequired"> Indicates if a new display is required.  </param>
        /// <param name="displayedValue"></param>
        /// <param name="buttonValue"></param>
        /// <returns></returns>
        string ButtonDigitAction(bool newDisplayRequired, string displayedValue, string buttonValue);

        /// <summary>
        ///     Perform an addition between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Addition result. </returns>
        string OperationAddition(string firstOperand, string secondOperand);

        /// <summary>
        ///     Perform a substraction between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Division result. </returns>
        string OperationSubstraction(string firstOperand, string secondOperand);

        /// <summary>
        ///     Perform a multiplication between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Multiplication result. </returns>
        string OperationMultiplication(string firstOperand, string secondOperand);

        /// <summary>
        ///     Perform a division between first and sevond operand.
        /// </summary>
        /// <param name="firstOperand"> First operand. </param>
        /// <param name="secondOperand"> Second operand. </param>
        /// <returns> Division result. </returns>
        string OperationDivision(string firstOperand, string secondOperand);
    }
}
