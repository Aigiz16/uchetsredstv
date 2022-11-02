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

namespace uchetsredstv
{
    /// <summary>
    /// Логика взаимодействия для UchetWindow.xaml
    /// </summary>
    public partial class UchetWindow : Window
    {
        public UchetWindow()
        {
            InitializeComponent();
           
        }

        private void SredtsvoButton_Click(object sender, RoutedEventArgs e)
        {
            SredstvoWindow sredstvoWindow = new SredstvoWindow();
            sredstvoWindow.Show();
        }

        private void OtvetsvvennoeLicoButton_Click(object sender, RoutedEventArgs e)
        {
            OtvetstvennoeLicoWindow otvetstvennoeLicoWindow = new OtvetstvennoeLicoWindow();
            otvetstvennoeLicoWindow.Show();
        }
    }
}
