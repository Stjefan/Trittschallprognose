using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trittschallprognose
{
    /// <summary>
    /// Interaktionslogik für DefaultSchichtView.xaml
    /// </summary>
    public partial class DefaultSchichtView : UserControl
    {
        public static readonly DependencyProperty SchichtbezeichnungProperty = DependencyProperty.Register(
  "Schichtbezeichnung",
  typeof(string),
  typeof(DefaultSchichtView),
  new FrameworkPropertyMetadata()
);
        public string Schichtbezeichnung
        {
            get { return (string)GetValue(SchichtbezeichnungProperty); }
            set { SetValue(SchichtbezeichnungProperty, value); }
        }


        public DefaultSchichtView()
        {
            InitializeComponent();
            DataContext = this;
        }

    }
}
