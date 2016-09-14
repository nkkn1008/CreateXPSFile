using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace CreateXPSFile
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument flowDocument = new FlowDocument();
            flowDocument.Background = Brushes.AliceBlue;

            Paragraph paragraph = new Paragraph(new Run("あいうえお"));

            flowDocument.Blocks.Add(paragraph);

            // FlowDocument クラスには DocumentPaginator プロパティはないが、
            // IDocumentPaginatorSource インターフェースを継承するので
            // キャストすれば取得可能
            DocumentPaginator docPaginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;

            // ページのサイズを設定する（ここでは A4 版 にした）
            docPaginator.PageSize = new Size(794, 1123);

            XpsDocument xpsDoc = new XpsDocument(@"C:\Users\kenichi\Desktop\work", FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDoc);

            writer.Write(docPaginator);

            // 必須
            xpsDoc.Close();
        }
    }
}
