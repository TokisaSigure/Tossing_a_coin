using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace WpfApplication1.CLass
{
    /// <summary>
    /// 入力されたかの判定をするクラス
    /// 変数群と思ってもらっても大丈夫かもしれない
    /// </summary>
    class Judge
    {
        int ans = 5, count_Closed = 0, count_Open = 0, count_Lasso = 0;//判定用の変数,グーカウント、パーカウント、チョキカウント
        const int limit = 15;//判定基準
        /*----------------------------------------------------------------------
         ゲッタ、セッタ宣言
        ----------------------------------------------------------------------*/
        public int Ans
        {
            set { this.ans = value; }
            get { return ans; }
        }

        public int Count_Closed
        {
            set { this.count_Closed = value; }
            get { return count_Closed; }
        }

        public int Count_Open
        {
            set { this.count_Open = value; }
            get { return count_Open; }
        }

        public int Count_Lasso
        {
            set { this.count_Lasso = value; }
            get { return count_Lasso; }
        }

        public int LIMIT
        {
            set { }
            get { return limit; }
        }

        public int judge(Body body)
        {

            if (body.HandRightState == HandState.Closed)
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

        /// <summary>
        /// 変数リセット用関数
        /// 呼び出せばクラス内部の変数を0で初期化する
        /// </summary>
        public void Reset()
        {
            ans = 0; count_Closed = 0; count_Lasso = 0; count_Open = 0;
        }

    }
}
