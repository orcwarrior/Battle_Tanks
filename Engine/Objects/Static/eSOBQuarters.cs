using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy, ale flagowy (wiêc mog¹ wyst¹piæ
    /// permutacje wszystkich wartoœci) przechowuj¹cy informacje co do æwiartek
    /// obiektu <see cref="staticObjectGroup"/> które zosta³y utworzone dla tego obiektu.
    /// </summary>
    [Flags]
	public enum eSOBQuarters
	{
        /// <summary>górna lewa czêœæ obiektu </summary>
		UPPER_LEFT = 1,
        /// <summary>górna prawa czêœæ obiektu </summary>
		UPPER_RIGHT = 2,
        /// <summary>dolna prawa czêœæ obiektu </summary>
        LOWER_RIGHT = 4,
        /// <summary>dolna lewa czêœæ obiektu </summary>
		LOWER_LEFT = 8 
	}
}
