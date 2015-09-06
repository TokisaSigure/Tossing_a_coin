using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace WpfApplication1.CLass
{
    /// <summary>
    /// キネクトでの入力判定を行う
    /// </summary>
    class Judge
    {
        int ans = 0, count_Closed = 0, count_Open = 0, count_Lasso = 0;//判定用の変数,グーカウント、パーカウント、チョキカウント
        const int limit = 30;//判定基準
        /*----------------------------------------------------------------------
         ゲッタ、セッタ宣言
        ----------------------------------------------------------------------*/
        public int Ans
        {
            set { this.ans = value; }
            get { return ans; }
        }

        public int judge(Body body)
        {

            if (body.HandRightState == HandState.Closed && ans == 0)
            {
                System.Diagnostics.Debug.WriteLine("グー");
                ++count_Closed;
                ans = Limit(count_Closed);
            }
            if (body.HandRightState == HandState.Open)
            {
                System.Diagnostics.Debug.WriteLine("パー");
                ++count_Open;
                ans = Limit(count_Open);
            }
            if (body.HandRightState == HandState.Lasso)
            {
                System.Diagnostics.Debug.WriteLine("チョキ");
                ++count_Lasso;
                ans = Limit(count_Lasso);
            }

            return ans;
        }
        private int Limit(int num)
        {
            return num > limit ? num : 5;//都合上5を返す。(0だと判定が出てしまうため)
        }
    }
}
