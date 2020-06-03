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
using System.Windows.Controls.Primitives;

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

            this.Languages.Add("English");
            this.Languages.Add("Portugues");
            this.Translations = new List<List<string>>();
            this.Translations.Add(new List<string>());
            this.Translations.Add(new List<string>());

            this.ResertList();
        }

        private void ResertList()
        {
            this.Grid = new Grid();
            //this.RootGrid.Children.Clear();
            this.RootGrid.Children.Add(Grid);
            this.CreateMenu();
            this.CreateTable(this.Languages.Count() + 1, this.Tags.Count() + 1);

            this.Content = this.RootGrid;
        }

        Grid Grid;
        public void CreateMenu()
        {
            
            DockPanel DockPanel = new DockPanel();
            DockPanel.Background = Brushes.White;

            ToolBarTray ToolBarTray = new ToolBarTray();
            ToolBarTray.Background = Brushes.White;

            // File
            ToolBar ToolBar = new ToolBar();
            ToolBar.Background = Brushes.White;
            
            // Buttons
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
            
            //Editor Location
            ToolBar ToolBarEditor = new ToolBar();
            ToolBarEditor.Background = Brushes.White;

            Button NewLanguageBtn = new Button();
            NewLanguageBtn.Name = "Add_Language";
            NewLanguageBtn.Background = Brushes.White;
            NewLanguageBtn.Content = "Add Language";
            ToolBarEditor.Items.Add(NewLanguageBtn);

            // set all buttons on toolbar
            ToolBarTray.ToolBars.Add(ToolBar);
            ToolBarTray.ToolBars.Add(ToolBarEditor);
            DockPanel.Children.Add(ToolBarTray);

            this.Grid.Children.Add(DockPanel);
        }

        /// <summary>
        /// Create a new Table Content
        /// </summary>

        public  List<string> Languages = new List<string>();
        public  List<string> Tags = new List<string>();
        public  List<List<string>> Translations;
        public void CreateTable(int columns, int rows)
        {
            for (int x = 0; x < columns; x++)
                Grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int y = 1; y < rows + 2; y++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = GridLength.Auto;
                Grid.RowDefinitions.Add(r);
            }

            for (int x = 0; x < columns; x++)
            {
                for (int y = 1; y < rows + 1; y++)
                {
                    if(x == 0)
                    {
                        TextBlock text = new TextBlock();
                        if(y == 1)
                        {
                            text.Text = "Tags";
                            text.FontWeight = FontWeights.Bold;
                        } else if(y > 1) text.Text = this.Tags[y - 2];
                        Grid.SetColumn(text, x);
                        Grid.SetRow(text, y);
                        Grid.Children.Add(text);
                    }
                    else
                    {
                        if (y == 1)
                        {
                            TextBlock text = new TextBlock();
                            text.Text = this.Languages[x-1];
                            text.FontWeight = FontWeights.Bold;
                            Grid.SetColumn(text, x);
                            Grid.SetRow(text, y);
                            Grid.Children.Add(text);
                        }
                        else if(y > 1)
                        {
                            TextBox tb = new TextBox();
                            tb.Name = this.Tags[y - 2] + "_" + this.Languages[x - 1];
                            tb.Text = this.Translations[x - 1][y - 2];
                            Grid.SetColumn(tb, x);
                            Grid.SetRow(tb, y);
                            Grid.Children.Add(tb);
                        }
                    }
                }
            }

            this.CreateAddLineBtn(rows);
        }

        private Button AddLineBtn;
        private void CreateAddLineBtn(int rows)
        {
            RowDefinition r = new RowDefinition();
            r.Height = GridLength.Auto;
            Grid.RowDefinitions.Add(r);

            AddLineBtn = new Button();
            AddLineBtn.Name = "AddLineBtn";
            AddLineBtn.Content = "Add +";
            AddLineBtn.FontSize = 25;
            AddLineBtn.Background = Brushes.White;
            AddLineBtn.Click += OpenAddTag;
            Grid.SetRow(AddLineBtn, rows+2);
            Grid.SetColumnSpan(AddLineBtn, 50);
            Grid.Children.Add(AddLineBtn);
        }


        private void UpdateTranlations()
        {
            for (int x = 0; x < this.Languages.Count(); x++)
            {
                for(int y = 0; y < this.Tags.Count(); y++)
                {
                    Console.Write(x);
                    Console.Write(y);
                    Console.WriteLine(this.Tags[y] + "_" + this.Languages[x]);
                    Console.WriteLine((TextBox)this.RootGrid.FindName(this.Tags[y] + "_" + this.Languages[x]) != null);
                }
            }
        }

        private AddTag AddTagWindow;
        private void OpenAddTag(object sender, EventArgs e)
        {
            AddTagWindow = new AddTag();
            AddTagWindow.MainWindow = this;
            AddTagWindow.Show();
        }


        public void AddTagOnList(string tag)
        {
            this.UpdateTranlations();
            this.Tags.Add(tag);
            for (int i = 0; i < this.Languages.Count(); i++) this.Translations[i].Add(tag);
            this.ResertList();
        }

        private void AddNewLanguageOnList(string language)
        {
            this.Languages.Add(language);
            this.Translations.Add(new List<string>());
            for (int i = 0; i < this.Tags.Count(); i++) {
                this.Translations[this.Translations.Count() - 1].Add(this.Tags[i]);
            }
        }
    }
}
