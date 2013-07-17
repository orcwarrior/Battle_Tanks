using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Sound
{
    /// <summary>
    /// Klasa odpowiadaj�ca za odtwarzanie d�wi�ki i wszelkie inne
    /// operacje zwiazane z d�wiekiem w aplikacji.
    /// </summary>
	public class soundManager
	{
        private static string soundsPath = "../sounds/";
        /// <summary>
        /// Konstruktor, wywo�uje <see cref="BASS.BASS_Init"/>
        /// </summary>
		public soundManager()
		{
            BASS.BASS_Init(-1, 44100, 0, 0, (object)null);
		}
        /// <summary>
        /// Tworzy i rozpoczyna odtwarzanie d�wi�ku.
        /// Obs�uguje formaty: OGG, WAV, MP3.
        /// </summary>
        /// <param name="filename">Nazwa pliku d�wi�kowego</param>
		public void playStream(string filename)
		{
            int stream = BASS.BASS_StreamCreateFile(false, soundsPath+filename, 0, 0, BASS.BASS_UNICODE | BASS.BASS_STREAM_AUTOFREE);
            BASS.BASS_ChannelPlay(stream, false);
		}
	}
}
