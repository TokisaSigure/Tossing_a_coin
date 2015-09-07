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
using System.Windows.Threading;
using Microsoft.Kinect;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /*-----------フィールド変数------------------------*/
        CLass.ImageClass Image = new CLass.ImageClass();
        BitmapImage bitmapImage = new BitmapImage();
        Boolean flag = false;//指さし状態かそうじゃないかを判定
        CLass.State state = new CLass.State();//現在の状態を保存するためのクラス
        CLass.KinectUtil kinectUtil = new CLass.KinectUtil();
        CLass.Judge jaudge = new CLass.Judge();
        /*-----------フィールド変数------------------------*/
        
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                kinectUtil.kinectInitializeComponent();
                kinectUtil.BFR.FrameArrived += bodyFrameReader_FrameArrived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            bitmapImage = Image.InputImage("Start.png");
            CLass.SE SE = new CLass.SE();
            SE.playSE(@"Music\じゃんけん.wav");
        }

        private void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;

            using (var bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame == null)
                {
                    return;
                }
                else
                {
                    if (kinectUtil.Bodies == null)
                    {
                        kinectUtil.Bodies = new Body[bodyFrame.BodyCount];
                    }
                    bodyFrame.GetAndRefreshBodyData(kinectUtil.Bodies);
                    dataReceived = true;
                }

                if (dataReceived)
                {
                    // ボディデータを取得する
                    bodyFrame.GetAndRefreshBodyData(kinectUtil.Bodies);
                    //認識しているBodyに対して
                    foreach (var body in kinectUtil.Bodies.Where(b => b.IsTracked))
                    {
                        if (body.HandRightState == HandState.Closed)
                        {
                            System.Diagnostics.Debug.WriteLine("グー");
                            ++jaudge.Count_Closed;
                            if(jaudge.Count_Closed >= jaudge.LIMIT)
                            {
                                WOL(0);
                            }
                        }
                        if (body.HandRightState == HandState.Open)
                        {
                            System.Diagnostics.Debug.WriteLine("パー");
                            ++jaudge.Count_Open;
                            if (jaudge.Count_Open >= jaudge.LIMIT)
                            {
                                WOL(2);
                            }

                        }
                        if (body.HandRightState == HandState.Lasso)
                        {
                            System.Diagnostics.Debug.WriteLine("チョキ");
                            ++jaudge.Count_Lasso;
                            if (jaudge.Count_Lasso >= jaudge.LIMIT)
                            {
                                WOL(1);
                            }

                        }
                    }
                }
            }
        }

        #region ボタン処理群　いずれ取り除く予定
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            state.setCount(false);
            WOL(0);
            if (!state.getCount())
            {
                if (flag) Up_Down(1);
            }
            this.Image1.Source = bitmapImage;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            state.setCount(false);
            WOL(1);
            if (!state.getCount())
            {
                if (flag) Up_Down(2);
            }
            this.Image1.Source = bitmapImage;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            state.setCount(false);
            WOL(2);
            if (!state.getCount())
            {
                if (flag) Up_Down(4);
            }
            this.Image1.Source = bitmapImage;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Up_Down(3);
            this.Image1.Source = bitmapImage;
        }

        #endregion

        /// <summary>
        /// じゃんけん判定用処理を呼び出すための関数
        /// Judgeクラスの変数リセットなどもここで行う
        /// </summary>
        /// <param name="num"></param>
        private void WOL(int num)
        {
            if (!flag)
            {
                CLass.Tossing_a_coin TAC = new CLass.Tossing_a_coin();
                int check = TAC.Check(num);
                switch (check)
                {
                    case 0: TextBlock1.Text = (TAC.name + "\n引き分けです、\nあいこで・・・"); break;
                    case 1: TextBlock1.Text = (TAC.name + "\n貴方の勝ちです、\nでは、あっちむいて・・・"); ChangeButton(); state.setWinner(true); break;
                    case 2: TextBlock1.Text = (TAC.name + "\n残念、貴方の負けです。\nじゃあ、あっちむいて・・・"); ChangeButton(); state.setWinner(false); break;
                }
                jaudge.Reset();
                ImageSource(check);
                this.Image1.Source = bitmapImage;
            }
        }

        private void ImageSource( int check)
        {
            
            switch (check)
            {
                case 0: bitmapImage = Image.InputImage("Drow.png"); break;
                case 1: bitmapImage = Image.InputImage("Win.png"); break;
                case 2: bitmapImage = Image.InputImage("Lose.png"); break;
            }
        }

        private void ChangeButton()
        {
            if (!flag)
            {
                this.Button1.Content = "ひだり！";
                this.Button2.Content = "した！";
                this.Button3.Content = "みぎ！";
                this.Button4.Visibility = Visibility.Visible;
                flag = !flag;//フラグ反転処理
            }
            else
            {
                this.Button1.Content = "グー！";
                this.Button2.Content = "チョキ！";
                this.Button3.Content = "パー！";
                this.Button4.Visibility = Visibility.Hidden;
                flag = !flag;//フラグ反転処理
            }
            state.setCount(true);//両方から呼び出される関数なので、ここでジャンケンと向きのスイッチが切り替わるはず
        }

        private void Up_Down(int num)//あっち向いてホイ、処理関数
        {
            CLass.Up_Down up_down = new CLass.Up_Down();
            /*ここに処理を書いてぇぇぇぇぇぇぇぇ！！！！*/
            if (flag) 
            {
                if (up_down.Checking(num))
                {
                    if (state.getWinner())
                    {
                        TextBlock1.Text = (up_down.str + "\n貴方の勝ちです、\nふふ、私もまだまだですね。");
                        ChangeButton();
                        ImageSource(1);
                    }
                    else
                    {
                        TextBlock1.Text = (up_down.str + "\n残念、貴方の負けです。\nまだまだですね・・・");
                        ChangeButton();
                        ImageSource(2);
                    }
                }
                else
                {
                    TextBlock1.Text = (up_down.str + "\n引き分けです、\nもう一度勝負です");
                    ChangeButton();
                    ImageSource(0);
                }
            }
        }

        private void WINDOW_CLOSED(object sender, EventArgs e)
        {
            kinectUtil.Kinect_Close();
        }

    }
}
