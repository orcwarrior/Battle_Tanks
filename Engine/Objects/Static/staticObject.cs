using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace Battle_Tanks.Objects
{
	public class staticObject : virtualObject
    {
        #region fields
        /// <summary>Dany obiekt statyczny jest kolizyjny</summary>
        public bool isColideable { get; private set; }
        /// <summary>Dany obiekt statyczny jest zniszczalny(przy kolizji)</summary>
        public bool isDestroyable { get; private set; }
        /// <summary>Dany obiekt statyczny jest ju¿ zniszczony.</summary>
        public bool isDestroyed { get; private set; }
        /// <summary>Typ danego obiektu statycznego.</summary>
        public eStaticObjType objType { get; private set; }
        /// <summary>Obiekt statyczny jest spawnpointem.</summary>
		public eSpawnPointType spawnPoint { get; private set; }
        #endregion

        #region methods
        /// <summary>
        /// Konstruktor który odrazu ustawia odpowiedni dla
        /// parametru objTyp <see cref="Visuals.Sprite"/> oraz 
        /// flagi (<see cref="isColideable"/>/<see cref="isDestroyable"/>).
        /// </summary>
        /// <param name="pos">Pozycja startowa.</param>
        /// <param name="objTyp">Typ obiektu.</param>
        public staticObject(Point pos, eStaticObjType objTyp)
			: base()
		{
            Pos = pos;
			objType = objTyp;
			_setObjVisual();
            _setObjFlags();
		}
		public staticObject(Point pos, eStaticObjType objTyp, eSpawnPointType spawnPtTyp)
			:this(pos,objTyp)
		{
			spawnPoint = spawnPtTyp;
		}
		
		public virtual bool Destroy(Rectangle destroyerBBox)
		{
            if (isColideable && isDestroyable)
            {
                _Destroy();
                return true;
            }
            return false; // nic nie zniszczono
		}
        public virtual bool intersectWith(Rectangle rectangle)
        {
            return boundingBox.IntersectsWith(rectangle);
        }
        /// <summary>
        /// Wywo³uje <see cref="Visuals.Sprite.Render"/>.
        /// </summary>
        public override void Render()
		{
			if(visual!=null)
			visual.Render();
		}
        /// <summary>
        /// Metoda nadpisana dla u³atwienia debugowania.
        /// </summary>
        /// <returns>Format: "SOB - $Typ_Obiektu - $spawnPoint ($PozSiatkiX,$PozSiatkiY)"</returns>
		public override string ToString()
		{
			string s = "SOB - ";
			if (objType != eStaticObjType.UNDEF)
				s += objType.ToString();
			else if (spawnPoint != eSpawnPointType.NONE)
				s += spawnPoint.ToString();
			return s + "(" + gridPos.X + "," + gridPos.Y + ")";
		}
        /// <summary>
        /// Bounding-Box jest generowany podstawowo identycznie jak wymiary i pozycja <see cref="virtualObject.visual"/>,
        /// jeœli jednak takowy nie istnieje to Bounding-Box bêdzie zaczynal siê w <see cref="virtualObject.Pos"/> i posiada³ wymiary jak
        /// <see cref="Engine.gridSize"/>.
        /// </summary>
        protected override void _generateBBox()
        {
            if (visual != null)
                bBox = new Rectangle(visual.pos.X, visual.pos.Y, Engine.gridSize, Engine.gridSize);
            else
                bBox = new Rectangle(Pos.X, Pos.Y, Engine.gridSize, Engine.gridSize);
        }
        /// <summary>
        /// Niszczy obiekt.
        /// <seealso cref="isColideable"/>
        /// <seealso cref="isDestroyed"/>
        /// </summary>
        protected void _Destroy()
        {
            visual = null;
            isColideable = false;
            isDestroyed = true;
        }

		/// <summary>
		/// Ustawia odpowiedniego Sprite'a dla obiektu zaleznie od jego typu
        /// <see cref="eStaticObjType"/>
		/// </summary>
		private void _setObjVisual()
		{
			switch (objType)
			{
				case eStaticObjType.WATER: visual = new Visuals.Sprite("WATER.png", Pos); break;
				case eStaticObjType.BUSH: visual = new Visuals.Sprite("BUSH.png", Pos); break;
            }
            if(visual!=null)
            visual.setPos(Pos);
            _generateBBox();
		}
        private void _setObjFlags()
        {
            isColideable = false; isDestroyable = false; //<- default
            switch (objType)
            {
                case eStaticObjType.WATER:      isColideable = true; isDestroyable = false; break;
                case eStaticObjType.BUSH:       isColideable = false;isDestroyable = false; break;
                case eStaticObjType.BRICK:      isColideable = true; isDestroyable = true;  break;
                case eStaticObjType.BRICK_HARD: isColideable = true; isDestroyable = false; break;
                case eStaticObjType.MAP_BORDER: isColideable = true; isDestroyable = false; break;
            }

        }
        #endregion
    }
}
