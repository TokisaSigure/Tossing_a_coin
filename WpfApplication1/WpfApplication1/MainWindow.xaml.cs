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
        BitmapImage bitmapImage = new BitmapImage(new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Start.png"));
        public MainWindow()
        {
            InitializeComponent();
            /*
            bitmapImage.BeginInit();//bitmapImage初期化開始の合図
            bitmapImage.UriSource = 
            bitmapImage.EndInit();//初期化完了の合図！！
             * */
            CLass.SE SE = new CLass.SE();
            SE.playSE(@"Music\じゃんけん.wav");
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
                case 0: TextBlock1.Text = (TAC.name + "\n引き分けです、\nもう一度勝負です"); break;
                case 1: TextBlock1.Text = (TAC.name + "\n貴方の勝ちです、\nふふ、私もまだまだですね。"); break;
                case 2: TextBlock1.Text = (TAC.name + "\n残念、貴方の負けです。\nまだまだですね・・・"); break;
            }
            ImageSource(check);
        }

        private void ImageSource( int check)
        {
            
            switch (check)
            {
                case 0: bitmapImage = new BitmapImage (new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Drow.png")); break;
                case 1: bitmapImage = new BitmapImage (new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Win.png")); break;
                case 2: bitmapImage = new BitmapImage (new Uri(@"C:\Users\s1223077\GitHub\Tossing_a_coin\WpfApplication1\WpfApplication1\Image\Lose.png")); break;
            }
        }

    }
}
