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

namespace Umbrella_Location_Editor
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "UmbrellaToolKit Editor [0.0.1] Beta";
            this.Grid = new Grid();
            this.CreateMenu();
            //this.CreateTable();


            this.Content = this.Grid;
        }

        public void CreateMenu()
        {
            
            DockPanel DockPanel = new DockPanel();
            DockPanel.Background = Brushes.White;

            ToolBarTray ToolBarTray = new ToolBarTray();
            ToolBarTray.Background = Brushes.White;

            ToolBar ToolBar = new ToolBar();
            ToolBar.Background = Brushes.White;

            Button NewBtn = new Button();
            NewBtn.Name = "New";
            NewBtn.Background = Brushes.White;
            NewBtn.Content = "New";
            ToolBar.Items.Add(NewBtn);

            Button OpenBtn = new Button();
            OpenBtn.Name = "Open";
            OpenBtn.Background = Brushes.White;
            OpenBtn.Content = "Open";
            ToolBar.Items.Add(OpenBtn);

            Button SaveBtn = new Button();
            SaveBtn.Name = "Save";
            SaveBtn.Background = Brushes.White;
            SaveBtn.Content = "Save";
            ToolBar.Items.Add(SaveBtn);

            ToolBarTray.ToolBars.Add(ToolBar);
            DockPanel.Children.Add(ToolBarTray);

            this.Grid.Children.Add(DockPanel);
        }

        /// <summary>
        /// Create a new Table Content
        /// </summary>
        Grid Grid;
        public void CreateTable()
        {
            Grid = new Grid();

            int columns = 3;
            int rows = 3;

            for (int x = 0; x < columns; x++)
                Grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int y = 0; y < rows; y++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = GridLength.Auto;
                Grid.RowDefinitions.Add(r);
            }

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if(x == 0)
                    {
                        TextBlock text = new TextBlock();
                        text.Text = y.ToString();
                        Grid.SetColumn(text, x);
                        Grid.SetRow(text, y);
                        Grid.Children.Add(text);
                    }
                    else
                    {
                        TextBox tb = new TextBox();
                        tb.Text = x + " " + y;
                        Grid.SetColumn(tb, x);
                        Grid.SetRow(tb, y);
                        Grid.Children.Add(tb);
                    }
                }
            }

            this.Content = Grid;
        }
    }
}
