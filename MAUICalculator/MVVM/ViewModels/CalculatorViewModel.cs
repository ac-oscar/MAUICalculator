using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

namespace MAUICalculator.MVVM.ViewModels
{
	public class CalculatorViewModel : INotifyPropertyChanged
	{
        public string operation { get; set; } = "";

        public string result { get; set; } = "0";

        public string Operator { get; set; } = null;

        public double FirstNumber { get; set; } = 0;

        public double SecondNumber { get; set; } = 0;

        public string Result
        {
            set
            {
                if (result != value)
                {
                    result = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Result"));
                }
            }
            get
            {
                return result;
            }
        }

        public string Operation
        {
            set
            {
                operation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Operation"));
            }
            get
            {
                return operation;
            }
        }

        public ICommand DigitCommand => new Command<string>(DigitCommandMethod);
        public ICommand OperatorCommand => new Command<string>(OperatorCommandMethod);
        public ICommand PercentageCommand => new Command<string>(PercentageCommandMethod);
        public ICommand BackspaceCommand => new Command(BackspaceCommandMethod);
        public ICommand ClearCommand => new Command(ClearCommandMethod);
        public ICommand DoCalculationCommand => new Command(DoCalculationCommandMethod);

        public event PropertyChangedEventHandler PropertyChanged;

        private void DigitCommandMethod(string parameterValue)
        {
            Operation += parameterValue;

            //Eliminar el cero a la izquierda
            if (Operation.StartsWith("00"))
            {
                Operation = Operation.Substring(1);
            }

            //Controlar puntos flotantes
            if (Operation.StartsWith("."))
            {
                Operation = "0.";
            }
        }

        private void OperatorCommandMethod(string parameterValue)
        {
            Operator = parameterValue;
            Operation += parameterValue;
        }

        private void PercentageCommandMethod(string parameterValue)
        {
            string number;

            try
            {
                if (Operator == "" || Operator == null)
                {
                    number = Operation;
                    Operation = (Convert.ToDouble(number) / 100).ToString();
                }
                else
                {
                    number = Operation.Substring(Operation.LastIndexOf(Operator) + 1);
                    Operation = Operation.Substring(0, Operation.LastIndexOf(Operator) + 1) + (Convert.ToDouble(number) / 100).ToString();
                }

            }
            catch
            {

            }
        }

        private void BackspaceCommandMethod()
        {
            if (Operation.Length > 1 || Operation != "")
            {
                Operation = Operation.Substring(0, Operation.Length - 1);
            }
        }

        private void ClearCommandMethod()
        {
            Operation = String.Empty;
            Operator = null;
            FirstNumber = 0;
            SecondNumber = 0;
            Result = "0";
        }

        private void DoCalculationCommandMethod()
        {
            double rstl = 0;

            try
            {
                if (String.IsNullOrEmpty(Operation))
                {
                    return;
                }

                if (Result != "0" && Result != null && Result != "Syntax Error")
                {
                    FirstNumber = Convert.ToDouble(Result);
                }

                if (Operation.LastIndexOf(Operator) > -1)
                {
                    if (FirstNumber == 0)
                    {
                        FirstNumber = Convert.ToDouble(Operation.Substring(0, Operation.LastIndexOf(Operator)));
                    }

                    SecondNumber = Convert.ToDouble(Operation.Substring(Operation.LastIndexOf(Operator) + 1));
                }

                switch (Operator)
                {
                    case "/":
                        rstl = FirstNumber / SecondNumber;
                        break;
                    case "-":
                        rstl = FirstNumber - SecondNumber;
                        break;
                    case "*":
                        rstl = FirstNumber * SecondNumber;
                        break;
                    case "+":
                        rstl = FirstNumber + SecondNumber;
                        break;
                    default:
                        break;
                }

                Result = rstl.ToString();
            }
            catch
            {
                Result = "Syntax Error";
                Operation = String.Empty;
            }
        }
    }
}

