using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj¹cy za kierunek poruszania siê
    /// <see cref="dynamicObject"/>.
    /// </summary>
	public enum eDir
	{
        /// <summary>Gracz porusz siê w górê</summary>
		U=0,
        /// <summary>Gracz porusz siê w prawo</summary>
		R=1,
        /// <summary>Gracz porusz siê w gó³</summary>
		D=2,
        /// <summary>Gracz porusz siê w lewo</summary>
		L=3,
        /// <summary>Iloœæ w³aœciwych wartosci tej enumeracji, przydatne do niektórych obliczeñ.</summary>
        MAX=4
	}
}
