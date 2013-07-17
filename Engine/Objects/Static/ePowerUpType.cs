using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj�cy za opisanie wszystkich typ�w power-up'�w.
    /// Wykorzystywane przedewszystkim przy okre�laniu typu <see cref="powerUpObject"/>.
    /// </summary>
    public enum ePowerUpType
    {
        /// <summary>power up - tarcza </summary>
        SHIELD=0,
        /// <summary>power up - dodatkowe �ycie </summary>
        LIVE=1,
        /// <summary>power up - ulepszenie amunicji </summary>
        STAR=2,
        /// <summary>power up - niszczy wszystkie wrogie czo�gi znajduj�ce si� na planszy </summary>
        ROCKETS=3,

        BARREL=4,
        /// <summary>power up - zatrzymuje wszytkie wrogie czo�gi na pewien czas </summary>
        TIME_STOP=5,
        MAX=6
    }
}
