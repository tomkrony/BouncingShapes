using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Shapes.Controls
{
    /// <summary>
    /// Interaktionslogik für LabeledItemView.xaml
    /// </summary>
    public partial class LabeledItemView : UserControl
    {
        public LabeledItemView()
        {
            InitializeComponent();
        }
	public static readonly DependencyProperty LabelProperty = DependencyProperty
		.Register(nameof(Label),
		typeof(string),
		typeof(LabeledItemView),
		new FrameworkPropertyMetadata("Unnamed Label"));

	public string Label
	{
	    get { return (string)GetValue(LabelProperty); }
	    set { SetValue(LabelProperty, value); }
	}
    }
}
