using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFont;
using System.Drawing;

namespace Battle_Tanks
{
    static class fonts
    {
        static public QFont Default = new QFont("../fonts/segoeui.ttf", 11f, FontStyle.Regular);
        /// <summary> Czcionka tylko do użytku wewnętrznego, jej kolor ulega zmianom z poziomu kodu!!! </summary>
        static public QFont hpBarFont = new QFont("../fonts/segoeuib.ttf", 7f, FontStyle.Bold);
    }
}
