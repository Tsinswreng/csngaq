using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace MyAvaloniaApp
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			AvaloniaXamlLoader.Load(this);
		}

		private void CopyTextButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
		{
			if (TextBoxA != null && TextBoxB != null)
			{
				TextBoxB.Text = TextBoxA.Text;
			}
		}

		//Optional:  Update TextBoxB immediately as TextBoxA changes.
		private void TextBoxA_TextChanged(object sender, Avalonia.Interactivity.TextChangedEventArgs e)
		{
			if (TextBoxA != null && TextBoxB != null)
			{
				TextBoxB.Text = TextBoxA.Text;
			}
		}
	}
}
