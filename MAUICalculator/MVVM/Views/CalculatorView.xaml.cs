using MAUICalculator.MVVM.ViewModels;

namespace MAUICalculator.MVVM.Views;

public partial class CalculatorView : ContentPage
{
	public CalculatorView()
	{
		InitializeComponent();
		BindingContext = new CalculatorViewModel();
	}
}
