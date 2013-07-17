using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Battle_Tanks.Sound
{
    /// <summary>
    /// Klasa statyczna komunikuj�ca si� z bass.dll poprzez DllIport.
    /// </summary>
	public static class BASS
	{
        /// <summary>Flaga kt�rej ustawienie spowoduje aut. wyczyszczenie pami�ci po zako�czeniu odtwarzania �r�d�a d�wi�ku.</summary>
        public const uint BASS_STREAM_AUTOFREE = 0x40000;
        /// <summary>Flaga dzi�ki kt�rej np. �cie�k� dost�pu mo�na poda� z uzyciem znakow spoza ASCII (np. PL znaki).</summary>
        public const uint BASS_UNICODE = 0x80000000;

        /// <summary>
        /// Inicjalizuje bass.dll
        /// </summary>
        /// <param name="device">Urzadzenie (podstawowe: -1)</param>
        /// <param name="freq">Czest. Pr�bkowania (Hz)</param>
        /// <param name="flags">Flagi</param>
        /// <param name="win">Wska�nik do obecnego okna</param>
        /// <returns>TRUE - jesli si� powiod�o, FALSE gdy wyst�pi� problem</returns>
        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BASS_Init(int device, int freq, int flags, int win, [MarshalAs(UnmanagedType.AsAny)] object clsid);

        /// <summary>
        /// Zwalnia pami�c zajmowan� przez bass.dll
        /// </summary>
        /// <returns>TRUE - jesli si� powiod�o, FALSE gdy wyst�pi� problem</returns>
        [DllImport("bass.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BASS_Free();

        /// <summary>
        /// Tworzy �r�d�o d�wi�ku dla danego pliku
        /// </summary>
        /// <param name="mem">Czy zr�d�o jest ju� wczytane do aplikacji</param>
        /// <param name="str">�cie�ka dost�pu/wska�nik do pocz�tku pliku w pami�ci aplikacji</param>
        /// <param name="offset">Od kt�rego momentu rozpocz�� odtwarzanie</param>
        /// <param name="length">D�ugo�� zr�d�a(dla plik�w �adowanych z pami�ci)</param>
        /// <param name="flags">Flagi</param>
        /// <returns>wska�nik/identyfikator do p�niejszych operacji na strumieniu d�wi�ku.</returns>
        [DllImport("bass.dll")]
        public static extern int BASS_StreamCreateFile(bool mem, [MarshalAs(UnmanagedType.LPWStr)] String str, UInt64 offset, UInt64 length, uint flags);

        /// <summary>
        /// Wyczysci strumie� d�wi�ku.
        /// </summary>
        /// <param name="handle">Strumie� d�wi�ku.</param>
        /// <returns>TRUE - jesli si� powiod�o, FALSE gdy wyst�pi� problem</returns>
        [DllImport("bass.dll")]
        public static extern bool BASS_StreamFree(int handle);

        /// <summary>
        /// Rozpocznie odtwarzanie d�wi�ku.
        /// </summary>
        /// <param name="handle">Strumie� d�wi�ku.</param>
        /// <param name="restart">przewin�c na pocz�tek?</param>
        /// <returns>TRUE - jesli si� powiod�o, FALSE gdy wyst�pi� problem</returns>
        [DllImport("bass.dll")]
        public static extern bool BASS_ChannelPlay(int handle, bool restart);

	}
}
