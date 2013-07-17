using System;
using System.Collections.Generic;
using System.Text;
using QuickFont;

namespace Battle_Tanks.Menu
{
	public class menuManager
    {
        #region statics
        /// <summary>Podstawowa czcionka napisów menu.</summary>
        static public QFont menuFont;
        /// <summary>Podstawowa czcionka napisów menu.(Gdy zaznaczona)</summary>
        static public QFont menuFontActive;
        /// <summary>Podstawowa czcionka ma³ych napisów menu.(Komentarze, opisy)</summary>
        static public QFont menuFontSmall;
        /// <summary>Alternatywna czcionka ma³ych napisów menu.(Komentarze, opisy)</summary>
        static public QFont menuFontAlt;
        #endregion

        /// <summary>Zwraca obecne menu gry.</summary>
        public Menu currentMenu { get; private set; }

		public event EventHandler keyUp;


        /// <summary>Odpowiada za process renderowania ca³oœci menu.</summary>
		public void Render()
		{
			throw new NotImplementedException();
		}
	}
}
