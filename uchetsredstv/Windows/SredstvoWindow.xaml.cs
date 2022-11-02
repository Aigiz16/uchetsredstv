using Microsoft.Office.Interop.Word;
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
using Word = Microsoft.Office.Interop.Word;


namespace uchetsredstv
{
    /// <summary>
    /// Логика взаимодействия для SredstvoWindow.xaml
    /// </summary>
    public partial class SredstvoWindow : System.Windows.Window
    {
        public static SredstvoWindow sredstvoWindow;
        public SredstvoWindow()
        {
            InitializeComponent();
            dgSredstvo.ItemsSource = (new Models.uchet_sredstvEntities().Средство).ToList();
            sredstvoWindow = this;
        }

        /// <summary>
        /// Метод для редактирования параметров существующих данных
        /// </summary>
       
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddSredstvo window = new Windows.AddSredstvo
            (this, (sender as Button).DataContext as Models.Средство);
            this.Hide();
            window.ShowDialog();

        }

        /// <summary>
        /// Метод для удаления данных из таблицы
        /// </summary>
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Средство user = (sender as Button).DataContext as Models.Средство;
                using (Models.uchet_sredstvEntities db = new Models.uchet_sredstvEntities())
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                    db.Средство.Remove(user);
                    db.SaveChanges();
                }
                MessageBox.Show("Удаление произошло успешно");
                dgSredstvo.ItemsSource = (new Models.uchet_sredstvEntities().Средство).ToList();
            }
            catch
            {
                MessageBox.Show("Этот элемент не может быть удален");
            }
        }
        private void dgSredstvo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.AddSredstvo addSredstvo = new Windows.AddSredstvo();
            addSredstvo.Show();
        }

        /// <summary>
        /// метод для сохранения в PDF формате
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToPDF_Click(object sender, RoutedEventArgs e)
        {

            List<Models.Средство> allSredstva;
            using (Models.uchet_sredstvEntities userEntities = new Models.uchet_sredstvEntities())
            {
                allSredstva = userEntities.Средство.ToList().OrderBy(s => s.Стоимость).ToList();
            }
            var app = new Word.Application();
            Word.Document document = app.Documents.Add();
            var sredstvaCategories = allSredstva.OrderBy(o => o.Стоимость).GroupBy(s => s.ИД_Средства)
                    .ToDictionary(g => g.Key, g => g.Select(s => new { s.ИД_Средства, s.Инвентарный_номер, s.Модель, s.Дата_приобретения, s.Стоимость }).ToArray());

            var data = sredstvaCategories.Where(p => p.Value != null);
            Word.Paragraph paragraph = document.Paragraphs.Add();
            Word.Range range = paragraph.Range;

            paragraph.set_Style("Заголовок 1");
            range.InsertParagraphAfter();
            var tableParagraph = document.Paragraphs.Add();
            var tableRange = tableParagraph.Range;
            var ServicesTable = document.Tables.Add(tableRange, data.Select(s => s.Value.Length).Sum() + 1, 5);
            ServicesTable.Borders.InsideLineStyle = ServicesTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            ServicesTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            Word.Range cellRange;
            cellRange = ServicesTable.Cell(1, 1).Range;
            cellRange.Text = "ИД";
            cellRange = ServicesTable.Cell(1, 2).Range;
            cellRange.Text = "Инвентарный номер";
            cellRange = ServicesTable.Cell(1, 3).Range;
            cellRange.Text = "Модель";
            cellRange = ServicesTable.Cell(1, 4).Range;
            cellRange.Text = "Дата приобретения";
            ServicesTable.Rows[1].Range.Bold = 1;
            cellRange = ServicesTable.Cell(1, 5).Range;
            cellRange.Text = "Стоимость";
            ServicesTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            int row = 1;
            var stepSize = 1;
            foreach (var sredstva in data)
            {
                foreach (var currentCost in sredstva.Value)
                {
                    cellRange = ServicesTable.Cell(row + stepSize, 1).Range;
                    cellRange.Text = currentCost.ИД_Средства.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = ServicesTable.Cell(row + stepSize, 2).Range;
                    cellRange.Text = currentCost.Инвентарный_номер.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = ServicesTable.Cell(row + stepSize, 3).Range;
                    cellRange.Text = currentCost.Модель;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = ServicesTable.Cell(row + stepSize, 4).Range;
                    cellRange.Text = currentCost.Дата_приобретения.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    cellRange = ServicesTable.Cell(row + stepSize, 5).Range;
                    cellRange.Text = currentCost.Стоимость.ToString();
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    row++;
                }
            }
            Word.Paragraph countCostParagraph = document.Paragraphs.Add();
            Word.Range countCostRange = countCostParagraph.Range;
            countCostRange.Text = $"Количество средств - {data.Select(s => s.Value.Length).Sum()} ";
            countCostRange.Font.Color = Word.WdColor.wdColorGreen;
            countCostRange.Font.Size = 14;
            countCostRange.InsertParagraphAfter();
            document.Words.Last.InsertBreak(Word.WdBreakType.wdSectionBreakNextPage);

            app.Visible = true;
            document.SaveAs2(@"D:\outputFileWord.docx");
            document.SaveAs2(@"D:\outputFilePdf.pdf",
            Word.WdExportFormat.wdExportFormatPDF);
        }
    }
}

