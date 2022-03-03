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
    /// Interaktionslogik für CanvasView.xaml
    /// </summary>
    public partial class CanvasView : Canvas
    {
        public CanvasView()
        {
            InitializeComponent();
        }

        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }

        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register(nameof(LoadCommand), typeof(ICommand), typeof(CanvasView), new PropertyMetadata(null));

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoadCommand != null)
            {
                LoadCommand.Execute(null);
            }
        }
    }
}
