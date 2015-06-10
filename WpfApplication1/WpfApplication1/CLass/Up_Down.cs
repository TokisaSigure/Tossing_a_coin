using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.CLass
{
    /// <summary>
    /// あっちむいてほいの判定にどうぞ、1.左,2.下,3.上,4.右 一致でtrue,不一致でfalse
    /// </summary>
    class Up_Down
    {
        public string str;//NPC向き表示用文字列
        Random rand = new Random();//NPC向き決定用乱数
        public Boolean Checking(int num)
        {
            int npc_num = (int)rand.Next(1,5);
            switch (npc_num)
            {
                case 1: str = "ひだり！"; break;
                case 2: str = "した！"; break;
                case 3: str = "うえ！"; break;
                case 4: str = "みぎ！"; break;
            }
            if(num==npc_num)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
