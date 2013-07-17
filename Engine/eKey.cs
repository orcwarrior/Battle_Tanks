using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj¹cy za przechowywanie akcji
    /// które mog¹ byæ wywo³ane przez nacisniêcie odpowiednich klawiszy.
    /// <see cref="Battle_Tanks.Controls"/>
    /// </summary>
	public enum eKey
	{

        /// <summary>Poruszenie czo³gu w górê</summary>
        UP,
        /// <summary>Poruszenie czo³gu w prawo</summary>
        RIGHT,
        /// <summary>Poruszenie czo³gu w dó³</summary>
        DOWN,
        /// <summary>Poruszenie czo³gu w lewo</summary>
        LEFT,
        /// <summary>Wystrza³ pocisku z czo³gu</summary>
		SHOOT
	}
}
