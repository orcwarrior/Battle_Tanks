using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks
{
    /// <summary>
    /// Typ wyliczeniowy odpowiadaj¹cy za obecny stan gry.
    /// </summary>
	public enum eGameState
	{
        /// <summary>Gra znajduje sie w stanie menu.(pauza)</summary>
        MENU,
        /// <summary>Gra znajduje sie w stanie gry.</summary>
        GAME,
        /// <summary>Gra znajduje sie w stanie niezdefiniowanym.(???)</summary>
		UNDEF
	}
}
