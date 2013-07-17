using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj�cy za przechowywanie akcji
    /// kt�re mog� by� wywo�ane przez nacisni�cie odpowiednich klawiszy.
    /// <see cref="Battle_Tanks.Controls"/>
    /// </summary>
	public enum eKey
	{

        /// <summary>Poruszenie czo�gu w g�r�</summary>
        UP,
        /// <summary>Poruszenie czo�gu w prawo</summary>
        RIGHT,
        /// <summary>Poruszenie czo�gu w d�</summary>
        DOWN,
        /// <summary>Poruszenie czo�gu w lewo</summary>
        LEFT,
        /// <summary>Wystrza� pocisku z czo�gu</summary>
		SHOOT
	}
}
