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

namespace uchetsredstv.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddSredstvo.xaml
    /// </summary>

    public partial class AddSredstvo : Window
    {
        private Models.Средство _user;
        public AddSredstvo(SredstvoWindow window, Models.Средство user = null)
        {
            InitializeComponent();
            
            _user = user;
            if (_user != null)
            {
                TbInventNomer.Text = Convert.ToString(_user.Инвентарный_номер);
                TbModel.Text = _user.Модель;
                TbData.Text = Convert.ToString(_user.Дата_приобретения);
                TbPrice.Text = Convert.ToString(_user.Стоимость);
            }
        }
    
        /// <summary>
        /// Добавление нового средства
        /// </summary>
        public AddSredstvo()
        {
            InitializeComponent();
            if (_user != null)
            {
                TbInventNomer.Text = Convert.ToString(_user.Инвентарный_номер);
                TbModel.Text = _user.Модель;
                TbData.Text = Convert.ToString(_user.Дата_приобретения);
                TbPrice.Text = Convert.ToString(_user.Стоимость);
            }
        }

        private void AddSredstvoButton_Click(object sender, RoutedEventArgs e)
        {
           
                using (Models.uchet_sredstvEntities db = new Models.uchet_sredstvEntities())
                {
                    if ((TbInventNomer.Text.Equals("")) || (TbModel.Text.Equals("")) || (TbData.Text.Equals("")) || (TbPrice.Text.Equals("")))
                    {
                        MessageBox.Show("Вы не ввели все необходимые данные!");
                        return;
                    }
                      
                    try
                    {
                        int InventNomer = Convert.ToInt32(TbInventNomer.Text);

                    }
                    catch
                    {
                        MessageBox.Show("Инвентарный номер должно быть числом!");
                        return;
                    }

                try
                {
                    DateTime Data = Convert.ToDateTime(TbData.Text);
                }
                catch
                {
                    MessageBox.Show("Введите корректную дату! Например: 10.10.2022");
                    return;
                }

                try
                {
                    int Price = Convert.ToInt32(TbPrice.Text);
                }
                catch
                {
                    MessageBox.Show("Стоимость должна быть числом!");
                    return;
                }
                            db.Средство.Add(new Models.Средство()
                    {
                        Инвентарный_номер = Convert.ToInt32(TbInventNomer.Text),
                        Модель = TbModel.Text,
                        Дата_приобретения = Convert.ToDateTime(TbData.Text),
                        Стоимость= Convert.ToInt32(TbPrice.Text)
                    });
                    db.SaveChanges();
                }
                MessageBox.Show("Добавление выполнено");
                this.Hide();
                SredstvoWindow.sredstvoWindow.dgSredstvo.ItemsSource =
                    (new Models.uchet_sredstvEntities().Средство).ToList();
               
            
            
        }

        /// <summary>
        /// Изменение параметров существующих данных
        /// </summary>
       
        private void EditSredstvoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Models.uchet_sredstvEntities db = new Models.uchet_sredstvEntities())
                {
                    if ((TbInventNomer.Text.Equals("")) || (TbModel.Text.Equals("")) || (TbData.Text.Equals("")) || (TbPrice.Text.Equals("")))
                    {
                        MessageBox.Show("Вы не ввели все необходимые данные!");
                        return;
                    }
                    try
                    {
                        int InventNomer = Convert.ToInt32(TbInventNomer.Text);

                    }
                    catch
                    {
                        MessageBox.Show("Инвентарный номер должно быть числом!");
                        return;
                    }
                    try
                    {
                        DateTime Data = Convert.ToDateTime(TbData.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Введите корректную дату! Например: день.месяц.год");
                        return;
                    }

                    try
                    {
                        int Price = Convert.ToInt32(TbPrice.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Стоимость должна быть числом!");
                        return;
                    }
                    db.Средство.Attach(_user);
                    _user.Инвентарный_номер = Convert.ToInt32(TbInventNomer.Text);
                    _user.Модель = TbModel.Text;
                    _user.Дата_приобретения = Convert.ToDateTime(TbData.Text);
                    _user.Стоимость = Convert.ToInt32(TbPrice.Text);

                    db.SaveChanges();
                }
                MessageBox.Show("Данные изменены");
                this.Hide();
                SredstvoWindow.sredstvoWindow.dgSredstvo.ItemsSource = (new Models.uchet_sredstvEntities().Средство).ToList();
                SredstvoWindow.sredstvoWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Данные не изменены");
            }

        }
    }
}
