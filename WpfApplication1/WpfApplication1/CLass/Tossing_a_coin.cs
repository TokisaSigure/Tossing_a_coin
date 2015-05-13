using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.CLass
{
    /// <summary>
    /// じゃんけん用のクラス
    /// 簡単に勝敗だせないかな～って
    /// </summary>
    class Tossing_a_coin
    {

        public string name;
        Random rand = new Random();


        public int Check(int num)
        {
            int check=0;//勝敗判定用、0でドロー、1で勝ち、2で負け
            int random = (int)rand.Next(0, 3);
            switch (random)
            {
                case 0 : name="グー！"; break;
                case 1: name = "チョキ！"; break;
                case 2: name = "パー！"; break;

            }
            switch(num-random)
            {
                case -1: check = 1; break;//勝ち
                case 0:check=0 ; break;//Drow
                case 2: check = 1; break;//勝ち
                default: check = 2; break;//負け
            }
            return check;
        }

    }
}
