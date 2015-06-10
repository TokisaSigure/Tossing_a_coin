using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace WpfApplication1.CLass
{
    /// <summary>
    /// 画像変更用のクラス、画像の名前を拡張子込みで投げると画像を切り替えるためのURIを返してくれるはず
    /// </summary>
    class ImageClass
    {
        string rootPath = System.Environment.CurrentDirectory;//カレントディレクトリまでのパス
        string Resource = @"\Image\";//画像フォルダの名前

        public BitmapImage InputImage(string imagePath)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri(rootPath+Resource+imagePath));
            return bitmapImage;
        }

    }
}
