using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data;
using System.Data.SqlClient;

namespace IamTestingMySelf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Тестовая строка.
        string ssc = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\creat\Documents\DB.mdf;Integrated Security=True;Connect Timeout=30";
        string expressionFTable = "CREATE TABLE Products ([ID] [int] NOT NULL, [Category] [nvarchar](50) NULL, [Price] [money] NOT NULL, [Count] [int] NOT NULL, CONSTRAINT [PK_IDProducts] PRIMARY KEY ([ID]), CONSTRAINT [FK_Category] FOREIGN KEY (ID) REFERENCES Categories(ID))";
        string expressionSTable = "CREATE TABLE Categories ([ID] [int] NOT NULL, [Category] [nvarchar](50) NOT NULL, CONSTRAINT [PK_ID] PRIMARY KEY ([ID]))";
        Controller controller;

        public MainWindow()
        {
            InitializeComponent();
            controller = new Controller();
            SetConnectionString();
        }

        public void SetConnectionString()
        {
            this.Visibility = Visibility.Hidden;
            Window window = new Window() { Left = 100, Top = 100, MinHeight = 190, MinWidth = 390, MaxHeight = 200, MaxWidth = 400, Topmost = true };
            Grid grid = CreateGrid();
            //Поле для ввода строки подключения; IndexOf == 1
            TextBox textBox = new TextBox() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Width = 100 };
            Grid.SetRow(textBox, 1);
            Grid.SetColumn(textBox, 2);
            grid.Children.Add(textBox);

            //Кнопка подключения. По умолчанию SQL
            Button button = new Button() { Content = "Подключить", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            button.Click += (object o, RoutedEventArgs e) =>
            {
                try
                {
                    controller.Connect(textBox.Text);
                    MessageBox.Show(controller.GetMachine);
                    this.Visibility = Visibility.Visible;
                    window.Close();
                    controller.RunProcedure(expressionSTable);
                    controller.RunProcedure(expressionFTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    controller.DeleteData();
                    //TODO: Close?
                }
            };
            Grid.SetRow(button, 2);
            Grid.SetColumn(button, 1);
            grid.Children.Add(button);
            window.Content = grid;
            window.Show();
        }

        //Создаем сетку окна.
        public Grid CreateGrid()
        {
            //Чертим таблицу
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(2, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

            //Информационная надпись
            TextBlock textBlock = new TextBlock() { Text = "Строка подключения:", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, TextWrapping = TextWrapping.Wrap };
            Grid.SetRow(textBlock, 1);
            Grid.SetColumn(textBlock, 0);
            grid.Children.Add(textBlock);

            return grid;
        }


    }
}
