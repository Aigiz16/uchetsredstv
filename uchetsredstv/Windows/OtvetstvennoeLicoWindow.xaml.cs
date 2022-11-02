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
    /// Логика взаимодействия для OtvetstvennoeLicoWindow.xaml
    /// </summary>
    public partial class OtvetstvennoeLicoWindow : Window
    {
        public OtvetstvennoeLicoWindow()
        {
            InitializeComponent();
            dgOtvetstvennoeLico.ItemsSource = (new Models.uchet_sredstvEntities().Ответственное_Лицо).ToList();
        }

        private void dgOtvetstvennoeLico_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
