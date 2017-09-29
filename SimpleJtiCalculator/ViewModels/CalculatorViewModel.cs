using System;
using System.Windows.Input;
using SimpleJtiCalculator.Commands;
using SimpleJtiCalculator.Constants;
using SimpleJtiCalculator.Models;
using SimpleJtiCalculator.Services;

namespace SimpleJtiCalculator.ViewModels
{
    /// <summary>
    ///     This class is the View Model of our Calculator.
    /// </summary>
    public class CalculatorViewModel : ViewModelBase
    {
        #region Private members

        /// <summary>
        ///     Service to do calculation operation.
        /// </summary>
        private ICalculatorService _calculatorService;

        /// <summary>
        ///     The model used by our calculator.
        /// </summary>
        private CalculationModel calculation;

        /// <summary>
        ///     Delegate commands run when a digit button is pressed.
        /// </summary>
        private DelegateCommand<string> _digitButtonPressCommand;

        /// <summary>
        ///     Delegate commands run when an operation button is pressed.
        /// </summary>
        private DelegateCommand<string> _operationButtonPressCommand;

        /// <summary>
        ///     Delegate commands run when a memory button is pressed.
        /// </summary>
        private DelegateCommand<string> _memoryButtonPressCommand;

        /// <summary>
        ///     Last operation.
        /// </summary>
        private string _lastOperation;

        /// <summary>
        ///     Boolean indicating if a new displayed is required.
        ///     Ex: if you type "4 + 2 * " it should display "8" and expect a second operand for the multiplication.
        /// </summary>
        private bool _newDisplayRequired;

        /// <summary>
        ///     Full operation and its result.
        /// </summary>
        private string _fullExpression;

        /// <summary>
        ///     Value currently displayed.
        /// </summary>
        private string _display;

        /// <summary>
        ///     Indicates if a value is stored in memory.
        ///     Will be empty if not.
        ///     Will display an 'M' if so.
        ///     TODO JTI : rename this, this should not be named like a boolean.
        /// </summary>
        private string _hasMemory;

        #endregion

        #region Constructor

        public CalculatorViewModel(ICalculatorService calculatorService)
        {
            calculation = new CalculationModel();
            RefreshAllProperties();
            _calculatorService = calculatorService;
        }

        #endregion

        #region Public Properties

        #region Model

        public string FirstOperand
        {
            get { return calculation.FirstOperand; }
            set { calculation.FirstOperand = value; }
        }

        public string SecondOperand
        {
            get { return calculation.SecondOperand; }
            set { calculation.SecondOperand = value; }
        }

        public string Operation
        {
            get { return calculation.Operation; }
            set { calculation.Operation = value; }
        }

        public string Memory
        {
            get { return calculation.Memory; }
            set { calculation.Memory = value; }
        }

        public string Result
        {
            get { return calculation.Result; }
            set { calculation.Result = value; }
        }

        #endregion

        public string LastOperation
        {
            get { return _lastOperation; }
            set { _lastOperation = value; }
        }

        public string Display
        {
            get { return _display; }
            set
            {
                _display = value;
                OnPropertyChanged("Display");
            }
        }

        public string FullExpression
        {
            get { return _fullExpression; }
            set
            {
                _fullExpression = value;
                OnPropertyChanged("FullExpression");
            }
        }

        public string HasMemory
        {
            get { return _hasMemory;}
            set
            {
                _hasMemory = value;
                OnPropertyChanged("HasMemory");
            }
        }

        #endregion

        #region Commands

        #region Operation Button

        public ICommand OperationButtonPressCommand
        {
            get
            {
                if (_operationButtonPressCommand == null)
                {
                    _operationButtonPressCommand = new DelegateCommand<string>(OperationButtonPress, CanOperationButtonPress);
                }
                return _operationButtonPressCommand;
            }
        }

        private static bool CanOperationButtonPress(string number)
        {
            return true;
        }

        public void OperationButtonPress(string operation)
        {
            ResetAfterError();
            if (FirstOperand == string.Empty || LastOperation == "=")
            {
                FirstOperand  = _display;
                LastOperation = operation;
            }
            else
            {
                SecondOperand  = _display;
                Operation      = LastOperation;
                _calculatorService.CalculateResult(calculation);
                LastOperation  = operation;
                Display        = Result;
                FullExpression = EvalFullExpression();
                FirstOperand   = _display;
            }
            _newDisplayRequired = true;
        }

        #endregion

        #region Memory Button

        public ICommand MemoryButtonPressCommand
        {
            get
            {
                if (_memoryButtonPressCommand == null)
                {
                    _memoryButtonPressCommand = new DelegateCommand<string>(MemoryButtonPress, CanMemoryButtonPress);
                }
                return _memoryButtonPressCommand;
            }
        }

        private static bool CanMemoryButtonPress(string button)
        {
            return true;
        }

        public void MemoryButtonPress(string button)
        {
            switch (button)
            {
                case CalculationConst.ButtonMemClear:
                    // Button MC is pressed: clean memory
                    Memory    = CalculationConst.Display0;
                    HasMemory = string.Empty;
                    break;
                case CalculationConst.ButtonMemRecall:
                    // Button MR is pressed: display current memory
                    Display = Memory;
                    break;
                case CalculationConst.ButtonMemSave:
                    // Button MS is pressed: save current display into memory
                    if (!HasErrors())
                    {
                        Memory    = Display;
                        HasMemory = CalculationConst.DisplayMemory;
                    }
                    break;
                case CalculationConst.ButtonMemPlus:
                    // Button M+ is pressed: add displayed value stored in memory 
                    Memory    = _calculatorService.OperationAddition(Memory, Display);
                    HasMemory = CalculationConst.DisplayMemory;
                    break;
                case CalculationConst.ButtonMemMinus:
                    // Button M- is pressed: substract displayed value stored in memory 
                    Memory    = _calculatorService.OperationSubstraction(Memory, Display);
                    HasMemory = CalculationConst.DisplayMemory;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Digit Button Press

        public ICommand DigitButtonPressCommand
        {
            get
            {
                if (_digitButtonPressCommand == null)
                {
                    _digitButtonPressCommand = new DelegateCommand<string>(DigitButtonPress, CanDigitButtonPress);
                }
                return _digitButtonPressCommand;
            }
        }

        // TODO JTI can be simplified like this. Need to get used to expression body :)
        //public ICommand DigitButtonPressCommand => _digitButtonPressCommand ??
        //                                         (_digitButtonPressCommand = new DelegateCommand<string>(DigitButtonPress, CanDigitButtonPress));


        private static bool CanDigitButtonPress(string button)
        {
            return true;
        }

        /// <summary>
        ///     Deals with input button.    
        /// </summary>
        /// <param name="button"></param>
        public void DigitButtonPress(string button)
        {
            ResetAfterError();
            switch (button)
            {
                case CalculationConst.ButtonC:
                    // Button C is pressed: clean everything
                    Display        = CalculationConst.Display0;
                    HasMemory      = string.Empty;
                    FirstOperand   = string.Empty;
                    SecondOperand  = string.Empty;
                    Operation      = string.Empty;
                    LastOperation  = string.Empty;
                    FullExpression = string.Empty;
                    break;
                case CalculationConst.ButtonCE:
                    // Button CE is pressed: remove current display
                    Display = CalculationConst.Display0;
                    break;
                case CalculationConst.ButtonDel:
                    // Button Del is pressed: remove last symbol
                    Display = _calculatorService.DeleteLastInput(_display);
                    break;
                case CalculationConst.ButtonPM:
                    // Button +/m is pressed: add or remove minus symbol
                    Display = _calculatorService.ButtonPlusMinusAction(_display);
                    break;
                case CalculationConst.ButtonPeriod:
                    // Button , is pressed: add or remove period symbol
                    Display = _calculatorService.ButtonPeriodAction(_newDisplayRequired, _display);
                    break;
                default:
                    // Another button is pressed: add a digit to current display
                    Display = _calculatorService.ButtonDigitAction(_newDisplayRequired, _display, button);
                    break;
            }
            _newDisplayRequired = false;
        }

        #endregion

        #region Private methods

        /// <summary>
        ///     Refresh all properties to empty or 0.
        /// </summary>
        private void RefreshAllProperties()
        {
            _display        = CalculationConst.Display0;
            Memory          = CalculationConst.Display0;
            _fullExpression = string.Empty;
            LastOperation   = string.Empty;
            FirstOperand    = string.Empty;
            SecondOperand   = string.Empty;
            Operation       = string.Empty;
        }

        /// <summary>
        ///     Reset current state if an error occured.
        /// </summary>
        private void ResetAfterError()
        {
            if (HasErrors())
            {
                RefreshAllProperties();
            }
        }

        /// <summary>
        ///     Check if an error is displayed.
        /// </summary>
        /// <returns> True if an error is displayed. Else if everything is ok. </returns>
        private bool HasErrors()
        {
            // TODO JTI ugly, improve this
            return !string.IsNullOrEmpty(_display) && _display.Contains(CalculationConst.DisplayError);
        }

        /// <summary>
        ///     Evaluate full expression and what should be displayed.
        ///     Ex: 4 + 2 = 6
        ///         9 / 0 = Error: cannot divide by 0
        /// </summary>
        /// <returns></returns>
        private string EvalFullExpression()
        {
            // TODO JTI put this in a service + clean
            var firstOperand    = Math.Round(Convert.ToDouble(FirstOperand),  CalculationConst.MantissaLength);
            var secondOperation = Math.Round(Convert.ToDouble(SecondOperand), CalculationConst.MantissaLength);

            // If result has no error, we can convert it to double and do a round on it.
            var result = HasErrors() ? "Error" : Math.Round(Convert.ToDouble(Result), CalculationConst.MantissaLength).ToString();
            // TODO JTI if there is an error, display the message... but here, sometimes it is too big for the textbox, that's why I simply put error. To fix

            return firstOperand + " " + Operation + " " + secondOperation + " = " + result;
        }
        #endregion

        #endregion

    }
}