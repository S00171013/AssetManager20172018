using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace AudioPlayer
{
    static public class AudioManager
    {
        static public Dictionary<string, SoundEffect> SoundEffects 
            = new Dictionary<string, SoundEffect>();


        public static void Play(ref SoundEffectInstance _playInstance, string sound)
        {
            if (_playInstance == null) 
            {
                _playInstance = AudioManager.SoundEffects[sound].CreateInstance();
                _playInstance.Play();
            }
            else if (_playInstance != null)
            {
                if (_playInstance.State == SoundState.Stopped)
                {
                    _playInstance.Dispose();
                    _playInstance = null;
                }
            }
        }
    }
}
