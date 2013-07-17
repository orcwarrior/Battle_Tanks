using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy, ale flagowy (wi�c mog� wyst�pi�
    /// permutacje wszystkich warto�ci) przechowuj�cy informacje co do �wiartek
    /// obiektu <see cref="staticObjectGroup"/> kt�re zosta�y utworzone dla tego obiektu.
    /// </summary>
    [Flags]
	public enum eSOBQuarters
	{
        /// <summary>g�rna lewa cz�� obiektu </summary>
		UPPER_LEFT = 1,
        /// <summary>g�rna prawa cz�� obiektu </summary>
		UPPER_RIGHT = 2,
        /// <summary>dolna prawa cz�� obiektu </summary>
        LOWER_RIGHT = 4,
        /// <summary>dolna lewa cz�� obiektu </summary>
		LOWER_LEFT = 8 
	}
}
