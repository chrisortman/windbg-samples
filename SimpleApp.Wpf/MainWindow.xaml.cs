using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
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
        private DeadlockService _deadlockService = new DeadlockService();
        private BackgroundWorker _bg;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            _deadlockService.TaskExample();

            _deadlockService.ClassicExample();

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _bg = new BackgroundWorker();
            _bg.DoWork += (o, args) =>
            {
                Thread.Sleep(3000);
                throw new ArgumentOutOfRangeException("Invalid ID must be greater than 0", "ID");
            };

            _bg.RunWorkerCompleted += (o, args) =>
            {

            };

        }

    }
}
