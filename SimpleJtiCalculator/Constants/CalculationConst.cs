namespace SimpleJtiCalculator.Constants
{
    /// <summary>
    ///     This class contains constantes used in the application.
    /// </summary>
    public static class CalculationConst
    {

        //     TODO JTI improv doc

        public const string Display0        = "0";
        public const string DisplayMinus    = "-";
        public const string DisplayPeriod   = ",";
        public const string DisplayError    = "Error: ";
        public const string DisplayMemory   = "M ";


        public const string ButtonDel       = "Del"; 
        public const string ButtonCE        = "CE";
        public const string ButtonC         = "C";
        public const string ButtonPeriod    = ",";
        public const string ButtonPM        = "+/-";

        public const string ButtonDevide    = "/";
        public const string ButtonMultiply  = "*";
        public const string ButtonMinus     = "-";
        public const string ButtonPlus      = "+";

        public const string ButtonMemClear  = "MC";
        public const string ButtonMemRecall = "MR";
        public const string ButtonMemSave   = "MS";
        public const string ButtonMemPlus   = "M+";
        public const string ButtonMemMinus  = "M-";

        /// <summary>
        /// Mantissa is part of a number in scientific notation or a floating-point number, consisting of its significant digits. 
        /// I use this term to set the number of digit after the , symbol.
        /// See https://en.wikipedia.org/wiki/Significand
        /// </summary>
        public const int MantissaLength = 5;

    }
}
