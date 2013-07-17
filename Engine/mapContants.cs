using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Battle_Tanks.Objects;
namespace Battle_Tanks
{
    /// <summary>
    /// Sta³a klasa zawieraj¹ca wartoœci kolorów(<see cref="Color"/>) czytanych z bitmapy
    /// na bazie których generowane bêda obiekty <see cref="Objects.staticObject"/> mapy, czêœæ z kolorów
    /// zapisanych jest jako tablica gdy¿ bêda generowaly wszystkie mo¿liwe uk³ady <see cref="Objects.staticObjectGroup"/>
    /// </summary>
	public static class mapContants
	{
        /// <summary> Ile mo¿lwoœci cegie³ mo¿na wygenerowaæ.</summary>
        public const int BRICK_MAX = 15;
        /// <summary> <see cref="Color"/> Krzak.</summary>
        static public readonly Color BUSH = Color.FromArgb(255, 0, 255, 0);//green
        /// <summary> <see cref="Color"/> Woda.</summary>
        static public readonly Color WATER = Color.FromArgb(255, 0, 0, 255);//blue
        /// <summary> <see cref="Color"/> Bazy gracza.</summary>
        static public readonly Color BASE = Color.FromArgb(255, 255, 128, 0);//orange
        /// <summary> <see cref="Color"/> Przeciwnik(Spawn Point).</summary>
        static public readonly Color ENEMY = Color.FromArgb(255, 255, 255, 0);//yellow
        /// <summary> <see cref="Color"/> Gracz Pierwszy(Spawn Point).</summary>
        static public readonly Color PLAYER1 = Color.FromArgb(255, 0, 128, 255);//teal
        /// <summary> <see cref="Color"/> Gracz Drugi(Spawn Point).</summary>
        static public readonly Color PLAYER2 = Color.FromArgb(255, 0, 255, 128);//......
        /// <summary> <see cref="Color"/> Tablica zawierajaca zwykle (czerwone) ceg³y.</summary>
        static public readonly Color[] BRICK = new Color[BRICK_MAX]{
            Color.FromArgb(255, 255, 0, 0), // 1000  [0]      1+0+0+0=1
            Color.FromArgb(255, 239, 0, 0), // 0100  [1]      0+2+0+0=2     
            Color.FromArgb(255, 223, 0, 0), // 1100  [2]      1+2+0+0=3     1 | 2
            Color.FromArgb(255, 207, 0, 0), // 0010  [3]      0+0+4+0=4    ---|---
            Color.FromArgb(255, 191, 0, 0), // 1010  [4]      1+0+4+0=5     4 | 3
            Color.FromArgb(255, 175, 0, 0), // 0110  [5]      0+2+4+0=6     
            Color.FromArgb(255, 159, 0, 0), // 1110  [6]      1+2+4+0=7    
            Color.FromArgb(255, 143, 0, 0), // 0001  [7]      0+0+0+8=8     
            Color.FromArgb(255, 127, 0, 0), // 1001  [8]      1+0+0+8=9      
            Color.FromArgb(255, 95, 0, 0),  // 0101  [9]      0+2+0+8=10     
            Color.FromArgb(255, 79, 0, 0),  // 1101  [10]     1+2+0+8=11     
            Color.FromArgb(255, 63, 0, 0),  // 0011  [11]     0+0+4+8=12
            Color.FromArgb(255, 47, 0, 0),  // 1011  [12]     1+0+4+8=13
            Color.FromArgb(255, 31, 0, 0),  // 0111  [13]     0+2+4+8=14
            Color.FromArgb(255, 15, 0, 0)   // 1111  [14]     1+2+4+8=15
        };
        /// <summary> <see cref="Color"/> Tablica zawierajaca mocne (szare) ceg³y.</summary>
        static public readonly Color[] BRICK_HARD = new Color[BRICK_MAX]
            {
            Color.FromArgb(255, 255,255,255), // 1000  [0]      1+0+0+0=1      1 | 2
            Color.FromArgb(255, 239,239,239), // 0100  [1]      0+2+0+0=2     ---|---
            Color.FromArgb(255, 223,223,223), // 1100  [2]      1+2+0+0=3      4 | 3
            Color.FromArgb(255, 207,207,207), // 0010  [3]      0+0+4+0=4 
            Color.FromArgb(255, 191,191,191), // 1010  [4]      1+0+4+0=5 
            Color.FromArgb(255, 175,175,175), // 0110  [5]      0+2+4+0=6 
            Color.FromArgb(255, 159,159,159), // 1110  [6]      1+2+4+0=7 
            Color.FromArgb(255, 143,143,143), // 0001  [7]      0+0+0+8=8 
            Color.FromArgb(255, 127,127,127), // 1001  [8]      1+0+0+8=9 
            Color.FromArgb(255, 95, 95, 95),  // 0101  [9]      0+2+0+8=10
            Color.FromArgb(255, 79, 79, 79),  // 1101  [10]     1+2+0+8=11
            Color.FromArgb(255, 63, 63, 63),  // 0011  [11]     0+0+4+8=12
            Color.FromArgb(255, 47, 47, 47),  // 1011  [12]     1+0+4+8=13
            Color.FromArgb(255, 31, 31, 31),  // 0111  [13]     0+2+4+8=14
            Color.FromArgb(255, 15, 15, 15)   // 1111  [14]     1+2+4+8=15
            };
        /// <summary> <see cref="Color"/> Nic(???) - Czerñ, dla przyspieszenia generowania mapy (jesli wykryto 'NOTHING' <see cref="Engine._createObjectFromMapPixel"/> pominie pozosta³e testy kolorów).</summary>
        static public readonly Color NOTHING = Color.FromArgb(255, 0, 0, 0);//czarny, dla optymalizacji            
        /// <summary>
        /// Funkcja przekszta³caj¹ca inta do wyliczeniowego
        /// typu eSobQuarters wymaganego do konstrukcji obiektów
        /// typu staticObjectGroup.
        /// </summary>
        /// <param name="i">zmienna</param>
        /// <returns>Zwraca odpowiednia wartoœæ w typie wyliczeniowym.</returns>
        static public eSOBQuarters intToSobQuarters(int i)
        {
			// bugfix: tablica indeksowana od 0, zeby przeklad na flagi byl faktycznie jak w komentarzach
			// trzeba zwiekszyc i o 1
			i++;
            eSOBQuarters flags = new eSOBQuarters();
            if (i >= 8)
            {
                i -= 8;
                flags = eSOBQuarters.LOWER_LEFT;
            }
            if (i >= 4)
            {
                i -= 4;
                flags = flags | eSOBQuarters.LOWER_RIGHT;
            }
            if (i >= 2)
            {
                i -= 2;
                flags = flags | eSOBQuarters.UPPER_RIGHT;
            }
            if (i >= 1)
            {
                flags = flags | eSOBQuarters.UPPER_LEFT;
            }
            return flags;
        }
		
	}
}
