using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /**
     * Author: El Mahdi
     * Date: 2021-03-12
     * TODO: Modulo %, CE, C, Decimal, History
     */
    public partial class MainWindow : Window
    {
        // We define the possible operators to protect our calculations.
        private readonly string[] _possibleOperators = {"+", "/", "*", "-"};
        // We use it to know when to stop adding to the first value
        private bool _isOperatorSelected = false;
        // We keep our math operator here
        private string _mathOperator = "";
        // Contains the first value
        private long _firstValue = 0;
        // Contains the second value
        private long _secondValue = 0;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GetPadNumber(object sender, RoutedEventArgs e)
        {
            // We get the button element from the event object.
            Button button = (Button) e.Source;
            // We get the button value from the button element.
            string buttonValue = (string) button.Content;

            // We reset the valaue and we return, otherwise an error will be thrown.
            if (buttonValue == "CE")
            {
                ResetCalculator();
                return;
            }
            
            // We do not need to go further if the user wants the result
            if (buttonValue == "=")
            {
                CalculateResult();
                // If we do not return the rest of the script will be executed, an error will be thrown
                return;
            }
            
            // We set the mat operator, we reset the label and return, otherwise an error will be thrown
            if (_possibleOperators.Contains(buttonValue))
            {
                _mathOperator = buttonValue;
                _isOperatorSelected = true;
                ResultLabel.Content = 0;
                return;
            }
            // We are pretty sure that we have the number now, we parse it and do our math
            int number = Int16.Parse(buttonValue);
            
            // To avoid duplicated code we work with references.
            ref long selectedValue = ref !_isOperatorSelected ? ref _firstValue : ref _secondValue;
            ConcatenateNumber(ref selectedValue, number);
        }

        /**
         * Calculate the result with the selected operator, show it and reset.
         */
        private void CalculateResult()
        {
            long result = 0;
            switch (_mathOperator)
            {
                case "+":
                    result = (_firstValue + _secondValue);
                    break;
                case "-":
                    result = (_firstValue - _secondValue);
                    break;
                case "*":
                    result = (_firstValue * _secondValue);
                    break;
                case "/":
                    result = (_firstValue / _secondValue);
                    break;
            }

            ResultLabel.Content = result;
            ResetCalculator(false);
        }

        /**
         * Reset the calculators and it's private properties.
         */
        private void ResetCalculator(bool resetResult = true)
        {
            if (resetResult)
            {
                ResultLabel.Content = 0;
            }
            _firstValue = 0;
            _secondValue = 0;
            _isOperatorSelected = false;
            _mathOperator = "";
        }

        /**
         * Whenever a number is clicked we calculate the correspondent value.
         */
        private void ConcatenateNumber(ref long value, int addition)
        {
            // We dont need to directly set the value or return, as it is a reference.
            value = (value * 10) + addition;
            ResultLabel.Content = value.ToString();
        }
    }
}