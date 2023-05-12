using System;
using System.Windows.Input;

namespace MAUICalculator.MVVM.ViewModels
{
	public class CalculatorViewModel
	{
        public double FirstNumber { get; set; }

        public double SecondNumber { get; set; }

        public double Result { get; set; }

		public ICommand AddCommand => new Command(() => Result = FirstNumber + SecondNumber);
    }
}

