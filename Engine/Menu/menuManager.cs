using System;
using System.Collections.Generic;
using System.Text;
using QuickFont;

namespace Battle_Tanks.Menu
{
	public class menuManager
    {
        #region statics
        /// <summary>Podstawowa czcionka napis�w menu.</summary>
        static public QFont menuFont;
        /// <summary>Podstawowa czcionka napis�w menu.(Gdy zaznaczona)</summary>
        static public QFont menuFontActive;
        /// <summary>Podstawowa czcionka ma�ych napis�w menu.(Komentarze, opisy)</summary>
        static public QFont menuFontSmall;
        /// <summary>Alternatywna czcionka ma�ych napis�w menu.(Komentarze, opisy)</summary>
        static public QFont menuFontAlt;
        #endregion

        /// <summary>Zwraca obecne menu gry.</summary>
        public Menu currentMenu { get; private set; }

		public event EventHandler keyUp;


        /// <summary>Odpowiada za process renderowania ca�o�ci menu.</summary>
		public void Render()
		{
			throw new NotImplementedException();
		}
	}
}
