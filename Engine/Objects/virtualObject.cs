using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Battle_Tanks.Visuals;

namespace Battle_Tanks.Objects
{
	public abstract class virtualObject
	{
        static protected Engine usedEngine;
        #region fileds
        protected Sprite visual;
        protected Rectangle bBox;
        protected Point Pos;
        #endregion

        #region metods
        /// bugfix: jezeli wartosc jest mniejsza niz 0 zwracaj zawsze -1 !!!
        public Point gridPos
        { get { return new Point(Pos.X < 0? -1 : Pos.X / Engine.gridSize, Pos.Y < 0? -1 : Pos.Y / Engine.gridSize); } }
        public Rectangle boundingBox { get { if (bBox.IsEmpty) _generateBBox(); return bBox; } }

        static public void setEngine(Engine e)
        {
            usedEngine = e;
        }
		public virtual void Update()
		{
		}

		public virtual void Render()
		{
            if(visual!=null)
            visual.Render();
		}

        /// <summary>
        /// Wygeneruje prostok¹t kolizji automatycznie, bazujac na pozycji i wymiarach sprite'a
        /// </summary>
        protected virtual void _generateBBox()
        {
            if (visual != null)
                bBox = new Rectangle(visual.pos.X,visual.pos.Y, visual.width, visual.height);
        }
        #endregion
    }
}
