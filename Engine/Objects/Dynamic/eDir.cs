using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj�cy za kierunek poruszania si�
    /// <see cref="dynamicObject"/>.
    /// </summary>
	public enum eDir
	{
        /// <summary>Gracz porusz si� w g�r�</summary>
		U=0,
        /// <summary>Gracz porusz si� w prawo</summary>
		R=1,
        /// <summary>Gracz porusz si� w g�</summary>
		D=2,
        /// <summary>Gracz porusz si� w lewo</summary>
		L=3,
        /// <summary>Ilo�� w�a�ciwych wartosci tej enumeracji, przydatne do niekt�rych oblicze�.</summary>
        MAX=4
	}
}
