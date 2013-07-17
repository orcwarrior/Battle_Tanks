using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Fabryka obiekt�w statycznych (te� statyczna).
    /// Tworzy obiekty typu <see cref="staticObject"/> wraz z obiektami
    /// dziedzicz�cymi <seealso cref="powerUpObject"/> <seealso cref="staticObjectGroup"/>.
    /// Ma na celu wy��czy� z uzycia bezpo�rednie konstruktory tych klas, tak aby u�atwi�
    /// decyzje kt�rej klasy u�y� by utworzy� oczekiwany przez nas obiekt.
    /// </summary>
	public static class staticObjectFactory
	{
        /// <summary>
        /// Utworzy obiekt.
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="objTyp">Typ obiektu.</param>
        /// <returns>Utworzony obiekt <see cref="staticObject"/>.</returns>
		public static staticObject Create(Point pos, eStaticObjType objTyp)
		{
            return new staticObject(pos, objTyp);
		}

        /// <summary>
        /// Utworzy obiekt.
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="spawnPtTyp">Typ spawn-point'a.</param>
        /// <returns>Utworzony obiekt <see cref="staticObject"/>.</returns>
		public static staticObject Create(Point pos,eSpawnPointType spawnPtTyp)
        {
            return new staticObject(pos, eStaticObjType.UNDEF ,spawnPtTyp);
		}

        /// <summary>
        /// Utworzy obiekt.
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="powUpTyp">Typ power-up'a.</param>
        /// <returns>Utworzony obiekt <see cref="powerUpObject"/>.</returns>
        public static powerUpObject Create(Point pos, ePowerUpType powUpTyp)
		{
            return new powerUpObject(pos, powUpTyp);
		}

        /// <summary>
        /// Utworzy obiekt.
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="objType">Typ obiektu statycznego.</param>
        /// <param name="quarters">�wiartki, kt�re maj� zosta� utworzone</param>
        /// <returns>Utworzony obiekt <see cref="staticObjectGroup"/>.</returns>
		public static staticObject Create(Point pos, eStaticObjType objType, eSOBQuarters quarters)
		{
            return new staticObjectGroup(pos, objType, quarters);
		}
	}
}
