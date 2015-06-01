using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace WpfApplication1.CLass
{
    class SE
    {
        const string SE1 = @"Music\じゃんけん.wav";

        public void playSE(string se)
        {
            SoundPlayer wavePlayer = new SoundPlayer(se);
            wavePlayer.Play();
        }

    }
}
