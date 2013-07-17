using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Input;
namespace Battle_Tanks
{
    /// <summary>
    /// Obiekty klasy Controls zawieraj¹ pe³ne przypisanie klawiszy
    /// steruj¹cych dla gracza poprzez atrybuty typu Key (OpenTK.Input)
    /// </summary>
	public class Controls
    {
        #region fields
        /// <summary>Klawisz Poruszania siê w górê</summary>
        public Key keyUP { get; private set; }
        /// <summary>Klawisz Poruszania siê w prawo</summary>
        public Key keyRIGHT { get; private set; }
        /// <summary>Klawisz Poruszania siê w dó³</summary>
        public Key keyDOWN { get; private set; }
        /// <summary>Klawisz Poruszania siê w lewo</summary>
        public Key keyLEFT { get; private set; }
        /// <summary>Klawisz Strza³u</summary>
        public Key keySHOOT { get; private set; }
        #endregion

        #region methods
        /// <summary>
        /// Podstawowy konstruktor klasy, ktory zaleznie od ID gracza tworzy
        /// zdefiniowan¹ dal niego podstawowo klawiszologie. 
        /// * PlayerID == 1 => Gracz pierwszy
        /// * PlayerID == 2 => Gracz drugi
        /// </summary>
        /// <param name="playerID">ID Gracza</param>
        public Controls(int playerID)
		{
            if (playerID == 1)
            {
                keyUP     =  Key.Up;
                keyRIGHT  =  Key.Right;
                keyDOWN   =  Key.Down;
                keyLEFT   =  Key.Left;
                keySHOOT  =  Key.RControl;
            }
            else if(playerID == 2)
            {
                keyUP     =  Key.W;
                keyLEFT   =  Key.A;
                keyDOWN   =  Key.S;
                keyRIGHT  =  Key.D;
                keySHOOT  =  Key.LControl;
            }
		}
        /// <summary>
        /// Ustawia dla klawisza odpowiednia wartoœæ
        /// na podstawie podanej jako argument wartosci
        /// typu wyliczeniowego eKey <see cref="Battle_Tanks.eKey"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
		public void bindKey(eKey key, Key value)
		{
            switch (key)
            {
                case eKey.DOWN:     keyDOWN = value; break;
                case eKey.LEFT:     keyLEFT = value; break;
                case eKey.RIGHT:    keyRIGHT= value; break;
                case eKey.SHOOT:    keySHOOT= value; break;
                case eKey.UP:       keyUP   = value; break;
            }
        }
        #endregion
    }
}
