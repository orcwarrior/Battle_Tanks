using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy opisuj�cy rodzaj <see cref="staticObject"/>
    /// dzi�ki kt�remu zidentyfikowanie wszystkich obiekt�w statycznych staje si�
    /// mo�liwe.
    /// </summary>
	public enum eStaticObjType
	{
        /// <summary> obiekt nie zdefiniowany (pusty) </summary>
		UNDEF,
        /// <summary> trawa </summary>
		BUSH,
        /// <summary> woda </summary>
		WATER,
        /// <summary> ceg�a </summary>
		BRICK,
        /// <summary> twarda ceg�a (nie mo�na jej zniszczy�) </summary>
		BRICK_HARD,
        /// <summary> baza (na samym dole po �rodku) </summary>
		BASE,
        /// <summary> obiekty dodaj�ce bonus, u�atwiaj�ce gre </summary>
		POWER_UP,
        /// <summary>  </summary>
        MAP_BORDER
	}
}
