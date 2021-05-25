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
    /// Interaktionslogik für EinzelschichtView.xaml
    /// </summary>
    public partial class EinzelschichtView : UserControl
    {
        public EinzelschichtView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AnzeigetextDP =
    DependencyProperty.Register(
    "Anzeigetext", typeof(string),
    typeof(EinzelschichtView)
    );
        public string Anzeigetext
        {
            get { return (string)GetValue(AnzeigetextDP); }
            set { SetValue(AnzeigetextDP, value); }
        }
}
}
