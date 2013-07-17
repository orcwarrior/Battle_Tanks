using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Time
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj¹cy za rodzaj pomiaru dla <see cref="Timer"/>
    /// </summary>
	public enum eUnits
	{
        /// <summary>Milisekundy</summary>
        MSEC,
        /// <summary>Klatki(dekrementacja przy kazdym <see cref="timeManager.Update"/>)</summary>
		Frames
	}
}
