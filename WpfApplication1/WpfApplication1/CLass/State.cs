using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.CLass

/// <summary>
/// ゲームの状態を記録して置く為のクラス
/// どうせだから、ゲッタとセッタ作って、呼び出しとかしてやる？
/// </summary>
{
    class State
    {
        private Boolean Winner;
        Boolean count = false;//一周したかしてないか,処理開始毎にfalseに、何かしらのアクションを取ったらtrueにする事

        public void setWinner(Boolean Winner){this.Winner = Winner;}
        public Boolean getWinner() { return Winner; }
        public void setCount(Boolean count) { this.count = count; }
        public Boolean getCount() { return count; }

    }
}
