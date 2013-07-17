using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Klasa abstrakcyjna obiektu dynamicznego, implenetuje podstawy 
    /// poruszania si�<see cref="Move"/>, kierunku poruszania si�<see cref="eDir"/>
    /// oraz odpowiednich zachowa� dotycz�cych wykrywanie kolizji(<see cref="tryMovement"/>/<see cref="undoMovement"/>)
    /// </summary>
	public abstract class dynamicObject : virtualObject
    {
        #region fields
        /// <summary> w pikselach na update, najlepiej aby n wielokrotnosc byla bliska 1.0 dla dokladnosci</summary>
		protected float speed;
        /// <summary>Pozycja zapisana przed ostatnim wywolaniem metody tryMovement() </summary>
        private Point premovementPos;
        private float pxPartDone;
        private eDir _dir;
        #endregion

        #region properties
        public bool onGridX { get { return (Pos.X % Engine.gridSize == 0); } }
        public bool onGridY { get { return (Pos.Y % Engine.gridSize == 0); } }
        public bool onGrid { get { return onGridX && onGridY; } }
        #endregion

        #region methods
        /// <summary>
        /// Kierunek w kt�rym porusza si� dany obiekt dynamiczny, ustawienie warto�ci obr�ci <see cref="virtualObject.visual"/> w spos�b zgodny z nowym kierunkiem poruszania sie.
        /// </summary>
        public eDir direction
		{
			get{return _dir;}
			set
			{
                if (value == _dir) return;
                //obrot obrazka:
                if (visual != null)
                {
                    if ((int)_dir % 2 == (int)value % 2) visual.rotate(180);
                    else if (((int)_dir + 1) % 4 == (int)value) visual.rotate(90);
                    else if (((int)_dir + 3) % 4 == (int)value) visual.rotate(270);
                }
                _dir = value;                
			}
		}

		public void Move(int x, int y)
		{            
            Pos.Offset(x,y);
            if (visual != null) 
            visual.setPos(Pos);
            bBox.Location = Pos;
		}

		public void MoveAbs(int x, int y)
		{
            MoveAbs(new Point(x, y));
		}

		public void Move(Point p)
		{
			throw new NotImplementedException();
		}

		public void MoveAbs(Point p)
        {
            Pos = p;
            if (visual != null) 
            visual.setPos(Pos);
            bBox.Location = Pos;
		}

        /// <summary>
        /// Aktualizacja dla obiektu dynamicznego to tylko przesuni�cie
        /// Testy kolizyjne odbywaja si� w podklasach.
        /// </summary>
		public override void Update()
		{
            base.Update();
		}
        /// <summary>
        /// Kontruktor klasy dynamicObject.
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="spd">Pr�dko�� poruszania si�</param>
        /// <param name="dir">Kierunek ruchu.</param>
		protected dynamicObject(Point pos, float spd, eDir dir)
		{
            Pos = pos;
            speed = spd;
            direction = dir;
		}
        /// <summary>
        /// Przesuwa obiekt odpowiednio dla predko�ci ruchu obiektu(<see cref="speed"/>).
        /// Zapamietuj�c obecn� pozycj� <see cref="premovementPos"/>, tak aby mo�na by�o
        /// �atwo do niej wr�ci� w wypadku wykrycia kolizji przez wywo�anie <see cref="undoMovement"/>
        /// </summary>
        protected void tryMovement()
        {            
            premovementPos = Pos;
            //float eps = 0.2f;
            int move=0;
            pxPartDone+=speed;
            if (/*pxPartDone % 1.0 <= eps &&*/ pxPartDone >= 1f) { move = (int)pxPartDone; pxPartDone -= move; }
            else return;
            switch (direction)
            {
                case eDir.U: Move(0, -move); break;
                case eDir.R: Move(move, 0);  break;
                case eDir.L: Move(-move, 0); break;
                case eDir.D: Move(0, move);  break;
            }
        }
        protected void undoMovement()
        {
            MoveAbs(premovementPos);
        }
        #endregion
    }
}
