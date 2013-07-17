using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Sound
{
    /// <summary>
    /// Klasa odpowiadajπca za odtwarzanie düwiÍki i wszelkie inne
    /// operacje zwiazane z düwiekiem w aplikacji.
    /// </summary>
	public class soundManager
	{
        private static string soundsPath = "../sounds/";
        /// <summary>
        /// Konstruktor, wywo≥uje <see cref="BASS.BASS_Init"/>
        /// </summary>
		public soundManager()
		{
            BASS.BASS_Init(-1, 44100, 0, 0, (object)null);
		}
        /// <summary>
        /// Tworzy i rozpoczyna odtwarzanie düwiÍku.
        /// Obs≥uguje formaty: OGG, WAV, MP3.
        /// </summary>
        /// <param name="filename">Nazwa pliku düwiÍkowego</param>
		public void playStream(string filename)
		{
            int stream = BASS.BASS_StreamCreateFile(false, soundsPath+filename, 0, 0, BASS.BASS_UNICODE | BASS.BASS_STREAM_AUTOFREE);
            BASS.BASS_ChannelPlay(stream, false);
		}
	}
}
