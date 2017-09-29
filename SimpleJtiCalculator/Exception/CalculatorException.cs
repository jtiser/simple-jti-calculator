using System;
namespace SimpleJtiCalculator.Exception
{
    /// <summary>
    ///     This class represent a customized exception thrown in the application.
    /// </summary>
    public class CalculatorException : System.Exception
    {
        #region Constructors

        public CalculatorException(string message) : base(message)
        {
        }

        #endregion
    }
}
