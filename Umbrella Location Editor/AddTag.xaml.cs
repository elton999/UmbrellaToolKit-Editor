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
using System.Windows.Shapes;

namespace Umbrella_Location_Editor
{
    /// <summary>
    /// Lógica interna para AddTag.xaml
    /// </summary>
    public partial class AddTag : Window
    {
        public MainWindow MainWindow;
        public AddTag()
        {
            InitializeComponent();
            this.Title = "Add Tag";
            this.Tag = "";
        }
        

        public void Save(object sender, RoutedEventArgs e)
        {
            this.MainWindow.AddTagOnList(this.TagString.Text);
            this.Close();
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
