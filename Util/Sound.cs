using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StockCheckerBot.Util
{
    public static class Sound
    {
        public static void PlaySound(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return;
            }
            if (path.StartsWith("!"))
            {
                PlaySystemSound(path);
            }
            else
            {
                PlayCustomSound(path);
            }
        }

        private static void PlaySystemSound(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                switch (path)
                {
                    default:
                        SystemSounds.Asterisk.Play();
                        break;

                    case "!Hand":
                        SystemSounds.Hand.Play();
                        break;
                }
            }
        }

        private static void PlayCustomSound(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                new SoundPlayer(path).Play();
            }
        }
    }
}
