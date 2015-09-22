using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace LemonParticlesSystem.Sound
{
    public class SoundSys
    {
        SoundEffect sound;
        SoundEffect sound2;
        SoundEffectInstance Sound1;
        SoundEffectInstance Sound2;
        float volume;

        public string path1 { get; set; }
        public string path2 { get; set; }
        public float Volume
        {
            get { return volume; }
            set { volume = Math.Max(0, value); }
        }

        public void LoadContent(ContentManager content)
        {
            byte[] buffer = new byte[20];
            sound = new SoundEffect(buffer, 44100, AudioChannels.Stereo);
            sound.Name = "1";
            if (path1 != null && path1 != string.Empty)
            {
                sound = content.Load<SoundEffect>(path1);
                // sound2 = content.Load<SoundEffect>(path2);
                Sound1 = sound.CreateInstance();
                Sound1.Volume = Volume;
                Sound1.Pan = 0.0f;
                MediaPlayer.Volume = 0.2f;
            }
           // Sound2 = sound2.CreateInstance();
        }

        public void Update(GameTime gameTime)
        {
            if (path1 != null)
                if (Sound1.State != SoundState.Playing)
                    Sound1.Play();
            
        }

        public void Stop()
        {
            if (path1 != null)
                Sound1.Stop();
        }
    }
}
