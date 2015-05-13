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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WOL(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WOL(1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WOL(2);
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
        }

    }
}
