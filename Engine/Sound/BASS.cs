using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Battle_Tanks.Sound
{
    /// <summary>
    /// Klasa statyczna komunikuj¹ca siê z bass.dll poprzez DllIport.
    /// </summary>
	public static class BASS
	{
        /// <summary>Flaga której ustawienie spowoduje aut. wyczyszczenie pamiêci po zakoñczeniu odtwarzania Ÿród³a dŸwiêku.</summary>
        public const uint BASS_STREAM_AUTOFREE = 0x40000;
        /// <summary>Flaga dziêki której np. œcie¿kê dostêpu mo¿na podaæ z uzyciem znakow spoza ASCII (np. PL znaki).</summary>
        public const uint BASS_UNICODE = 0x80000000;

        /// <summary>
        /// Inicjalizuje bass.dll
        /// </summary>
        /// <param name="device">Urzadzenie (podstawowe: -1)</param>
        /// <param name="freq">Czest. Próbkowania (Hz)</param>
        /// <param name="flags">Flagi</param>
        /// <param name="win">WskaŸnik do obecnego okna</param>
        /// <returns>TRUE - jesli siê powiod³o, FALSE gdy wyst¹pi³ problem</returns>
        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BASS_Init(int device, int freq, int flags, int win, [MarshalAs(UnmanagedType.AsAny)] object clsid);

        /// <summary>
        /// Zwalnia pamiêc zajmowan¹ przez bass.dll
        /// </summary>
        /// <returns>TRUE - jesli siê powiod³o, FALSE gdy wyst¹pi³ problem</returns>
        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BASS_Free();

        /// <summary>
        /// Tworzy Ÿród³o dŸwiêku dla danego pliku
        /// </summary>
        /// <param name="mem">Czy zród³o jest ju¿ wczytane do aplikacji</param>
        /// <param name="str">Œcie¿ka dostêpu/wska¿nik do pocz¹tku pliku w pamiêci aplikacji</param>
        /// <param name="offset">Od którego momentu rozpocz¹æ odtwarzanie</param>
        /// <param name="length">D³ugoœæ zród³a(dla plików ³adowanych z pamiêci)</param>
        /// <param name="flags">Flagi</param>
        /// <returns>wskaŸnik/identyfikator do póŸniejszych operacji na strumieniu dŸwiêku.</returns>
        [DllImport("bass.dll")]
        public static extern int BASS_StreamCreateFile(bool mem, [MarshalAs(UnmanagedType.LPWStr)] String str, UInt64 offset, UInt64 length, uint flags);

        /// <summary>
        /// Wyczysci strumieñ dŸwiêku.
        /// </summary>
        /// <param name="handle">Strumieñ dŸwiêku.</param>
        /// <returns>TRUE - jesli siê powiod³o, FALSE gdy wyst¹pi³ problem</returns>
        [DllImport("bass.dll")]
        public static extern bool BASS_StreamFree(int handle);

        /// <summary>
        /// Rozpocznie odtwarzanie dŸwiêku.
        /// </summary>
        /// <param name="handle">Strumieñ dŸwiêku.</param>
        /// <param name="restart">przewin¹c na pocz¹tek?</param>
        /// <returns>TRUE - jesli siê powiod³o, FALSE gdy wyst¹pi³ problem</returns>
        [DllImport("bass.dll")]
        public static extern bool BASS_ChannelPlay(int handle, bool restart);

	}
}
