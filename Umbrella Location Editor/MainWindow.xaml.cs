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
            this.CreateAddLineBtn();
            this.Content = this.RootGrid;
        }

        private void ResertList()
        {
            this.Grid = new Grid();
            //this.RootGrid.Children.Clear();
            this.LayoutTable.Children.Add(Grid);
            this.CreateMenu();
            this.CreateTable(this.Languages.Count() + 1, this.Tags.Count() + 1);
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

        private List<List<TextBox>> TextBoxList = new List<List<TextBox>>();

        private void UpdateListTextBox()
        {
            if (this.TextBoxList.Count() < this.Tags.Count())
            {
                this.TextBoxList.Add(new List<TextBox>());
                int LastTextBox = this.Tags.Count() -1;

                for (int y = this.TextBoxList[LastTextBox].Count(); y < this.Languages.Count(); y++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Name = this.Tags[LastTextBox] + "_" + this.Languages[y];
                    textBox.Text = this.Tags[LastTextBox];
                    this.TextBoxList[LastTextBox].Add(textBox);
                }
            }
            
            if(this.TextBoxList.Count() > 0 && this.TextBoxList[0].Count() < this.Languages.Count())
            {
                for (int x = 0; x < this.Tags.Count(); x++)
                {
                    for (int y = this.TextBoxList[0].Count(); y < this.Languages.Count(); y++)
                    {
                        TextBox textBox = new TextBox();
                        textBox.Name = this.Tags[x] + "_" + this.Languages[y];
                        textBox.Text = this.Tags[this.Tags.Count() - 1];
                        this.TextBoxList[x].Add(textBox);
                    }
                }
            }
        }


        private void AddRowTable()
        {
            RowDefinition r = new RowDefinition();
            r.Height = GridLength.Auto;
            Grid.RowDefinitions.Add(r);
        }

        public void CreateTable(int columns, int rows)
        {
            this.UpdateListTextBox();

            for (int x = 0; x < columns; x++)
                Grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int y = 1; y < rows + 2; y++)
            {
                this.AddRowTable();
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
                            TextBox textBox = this.TextBoxList[y - 2][x - 1];
                            Grid.SetColumn(textBox, x);
                            Grid.SetRow(textBox, y);
                            Grid.Children.Add(textBox);
                        }
                    }
                }
            }
        }

        private Button AddLineBtn;
        private void CreateAddLineBtn()
        {
            this.AddRowTable();

            AddLineBtn = new Button();
            AddLineBtn.Name = "AddLineBtn";
            AddLineBtn.Content = "Add +";
            AddLineBtn.FontSize = 25;
            AddLineBtn.Background = Brushes.White;
            AddLineBtn.Click += OpenAddTag;
            this.LayoutAddBtn.Children.Add(AddLineBtn);
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
            this.Tags.Add(tag);
            for (int i = 0; i < this.Languages.Count(); i++) this.Translations[i].Add(tag);
            this.UpdateListTextBox();
            
            this.AddRowTable();

            TextBlock text = new TextBlock();
            text.Text = tag;
            Grid.SetColumn(text, 0);
            Grid.SetRow(text, this.Tags.Count() + 2);
            Grid.Children.Add(text);

            for (int x = this.Tags.Count() - 1; x < this.Tags.Count(); x ++)
            {
                Console.WriteLine(this.TextBoxList.Count());
                Console.WriteLine(this.TextBoxList[this.Tags.Count() - 1].Count());
                for (int y = 0; y < this.Languages.Count(); y++)
                {
                    TextBox textBox = this.TextBoxList[x][y];
                    Grid.SetColumn(textBox, y + 1);
                    Grid.SetRow(textBox, this.Tags.Count()+2);
                    Grid.Children.Add(textBox);
                }
            }
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
