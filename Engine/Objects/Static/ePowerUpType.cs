using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj¹cy za opisanie wszystkich typów power-up'ów.
    /// Wykorzystywane przedewszystkim przy okreœlaniu typu <see cref="powerUpObject"/>.
    /// </summary>
    public enum ePowerUpType
    {
        /// <summary>power up - tarcza </summary>
        SHIELD=0,
        /// <summary>power up - dodatkowe ¿ycie </summary>
        LIVE=1,
        /// <summary>power up - ulepszenie amunicji </summary>
        STAR=2,
        /// <summary>power up - niszczy wszystkie wrogie czo³gi znajduj¹ce siê na planszy </summary>
        ROCKETS=3,

        BARREL=4,
        /// <summary>power up - zatrzymuje wszytkie wrogie czo³gi na pewien czas </summary>
        TIME_STOP=5,
        MAX=6
    }
}
