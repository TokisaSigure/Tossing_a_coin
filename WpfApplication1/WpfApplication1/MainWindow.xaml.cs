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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage bitmapImage = new BitmapImage();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WOL(0);
            this.Image1.Source = bitmapImage; 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WOL(1);
            this.Image1.Source = bitmapImage; 
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WOL(2);
            this.Image1.Source = bitmapImage; 
        }

        private void WOL(int num)
        {
            CLass.Tossing_a_coin TAC = new CLass.Tossing_a_coin();
            int check = TAC.Check(num);
            switch(check)
            {
                case 0: TextBlock1.Text = (TAC.name + "\n引き分け！もう一回勝負！"); break;
                case 1: TextBlock1.Text = (TAC.name + "\n君の勝ちかぁ・・・、まだまだ！もう一回勝負！"); break;
                case 2: TextBlock1.Text = (TAC.name + "\n残念！君の負けだよっ！まだまだだね・・・っ！"); break;
            }
            ImageSource(check);
        }

        private void ImageSource( int check)
        {
            bitmapImage.BeginInit();//bitmapImage処理開始の合図
            switch (check)
            {
                case 0: bitmapImage.UriSource = new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Drow.png"); break;
                case 1: bitmapImage.UriSource = new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Drow.png"); break;
                case 2: bitmapImage.UriSource = new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Drow.png"); break;
            }
            bitmapImage.EndInit();//処理完了の合図！！
        }

    }
}
