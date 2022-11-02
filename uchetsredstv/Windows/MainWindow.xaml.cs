using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace uchetsredstv
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            UchetWindow uchet = new UchetWindow();
            uchet.Show();
            main = this;

        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("www.vk.com");
        }

        private void Hyperlink_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("www.instagram.com");
        }

        private void FeedBackForm_Click(object sender, RoutedEventArgs e)
        {
            FeedBackForm feedbackform = new FeedBackForm();
            feedbackform.Show();
        }
    }
}
