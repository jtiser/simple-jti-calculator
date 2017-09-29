namespace SimpleJtiCalculator.Models
{
    /// <summary>
    ///     Data model containing properties used by our calculator.
    /// </summary>
    public class CalculationModel
    {
        #region Constructors

        public CalculationModel()
        {
            FirstOperand  = string.Empty;
            SecondOperand = string.Empty;
            Operation     = string.Empty;
            Result        = string.Empty;
            Memory        = string.Empty;
        }

        #endregion

        #region Public properties

        /// <summary>
        ///     The first operand.
        ///     Corresponds to 4 if current operation is 4 + 2 = 6
        /// </summary>
        public string FirstOperand { get; set; }

        /// <summary>
        ///     The second operand.
        ///     Corresponds to 2 if current operation is 4 + 2 = 6
        /// </summary>
        public string SecondOperand { get; set; }

        /// <summary>
        ///     The current operation.
        ///     Corresponds to + if current operation is 4 + 2 = 12
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        ///     The result of our operation.
        ///     Corresponds to 12 if current operation is 4 + 2 = 12
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        ///     Data stored in memory.
        /// </summary>
        public string Memory { get; set; }

        #endregion
    }
}
