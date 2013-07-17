using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// OBSOLETE (Partialy)
    /// Przeciwnicy spawnuja sie wg. listy pkt. przechowywanej w Engine
    /// obiekty klasy player maj¹ pozycje zapisan¹ wewn pola klasy.
    /// </summary>
	public enum eSpawnPointType
	{
		NONE,
        /// <summary> gracz 1 </summary>
		PLAYER_1,
        /// <summary> gracz 2 </summary>
		PLAYER_2,
        /// <summary> przeciwnicy </summary>
		ENEMY
	}
}
