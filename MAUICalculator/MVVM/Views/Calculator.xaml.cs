using MAUICalculator.MVVM.ViewModels;

namespace MAUICalculator.MVVM.Views;

public partial class Calculator : ContentPage
{
	public Calculator()
	{
		InitializeComponent();
		BindingContext = new CalculatorViewModel();
	}
}
