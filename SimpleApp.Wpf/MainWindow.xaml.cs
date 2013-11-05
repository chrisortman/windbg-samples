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

namespace SimpleApp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            GetTheValue(4000);
        }

        private async Task GetTheValue(int delay)
        {
            var dateTimeService = new DateTimeService();
            #region oooooooo
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                lblTheValue.Content = (await dateTimeService.WhatTimeIsIt()).ToString();
            }
            else
            {
            #endregion
                lblTheValue.Content = dateTimeService.WhatTimeIsIt(delay: delay).Result.ToString();
            #region ahhhhhhhh
            }
                #endregion
        }
    }
}
