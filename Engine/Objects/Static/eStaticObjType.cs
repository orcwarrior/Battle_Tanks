using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy opisuj¹cy rodzaj <see cref="staticObject"/>
    /// dziêki któremu zidentyfikowanie wszystkich obiektów statycznych staje siê
    /// mo¿liwe.
    /// </summary>
	public enum eStaticObjType
	{
        /// <summary> obiekt nie zdefiniowany (pusty) </summary>
		UNDEF,
        /// <summary> trawa </summary>
		BUSH,
        /// <summary> woda </summary>
		WATER,
        /// <summary> ceg³a </summary>
		BRICK,
        /// <summary> twarda ceg³a (nie mo¿na jej zniszczyæ) </summary>
		BRICK_HARD,
        /// <summary> baza (na samym dole po œrodku) </summary>
		BASE,
        /// <summary> obiekty dodaj¹ce bonus, u³atwiaj¹ce gre </summary>
		POWER_UP,
        /// <summary>  </summary>
        MAP_BORDER
	}
}
