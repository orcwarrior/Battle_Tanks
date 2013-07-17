using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Battle_Tanks.Objects
{
	public class Enemy : Vehicle
    {
        #region fields
        private Time.Timer _AIchangeDirTimer = new Time.Timer(Time.eUnits.MSEC, 3000, 1500, true, false);
        private Time.Timer _AIshootTimer = new Time.Timer(Time.eUnits.MSEC, 2500, 1000, true, false);
        #endregion

        #region methods
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="speed">Szybkoœæ</param>
        /// <param name="dir">Kierunek</param>
        /// <param name="hitPoints">Pkt. ¿ycia</param>
        public Enemy(Point pos, float speed, eDir dir, int hitPoints)
            : base(pos, speed, dir, hitPoints, null)
        {
            bBox = new Rectangle(new Point(pos.X + 2, pos.Y + 10), new Size(46, 53));
        }

        /// <summary>
        /// Metoda Update, przedewszystkim odpowiada za przetworzenie sztucznej inteligencji (<see cref="_doAI"/>)
        /// oraz próbe poruszenia siê w obecnym kierunku.
        /// </summary>
		public override void Update()
		{
            _doAI();

            tryMovement();
            staticObject collObj = usedEngine.checkCollisionStat(this);
            if (collObj != null)
            { // wykryto kolizje, przywróc ostatni¹ pozycje
                _AIchangeDirTimer.stop();
                undoMovement();
            }
		}
        /// <summary>
        /// Funckja w obecnej formie dzia³a identycznie do tej odziedziczonej z <see cref="Vehicle"/>
        /// </summary>
        public override void Render()
        {
            base.Render();
            //fonts.Default.Print("[" + hp + "/" + hpMax + "]\n[" + Pos.X + "," + Pos.Y + "]", new OpenTK.Vector2(Pos.X, Pos.Y));
        }

        internal void setVisuals(eTankType t)
        {
            switch (t)
            {
                default: visual = new Visuals.Sprite("TANK_ENEMY.PNG", Pos); break;
                /*z
                case eTankType.AGILE: spd = objSpeed.QUICK; break;
                case eTankType.NORMAL: spd = objSpeed.DEFAULT; break;
                case eTankType.IMPROVED: spd = objSpeed.IMPROVED; break;
                case eTankType.HEAVY: spd = objSpeed.SLOW; break;
                 * */
            }
            visual.rotationOrgin = new Point(25, 31);
        }

        private void _doAI()
        {
            if (!_AIchangeDirTimer.enabled)
            {
                Random rnd = new Random();
                direction = (eDir)rnd.Next((int)eDir.MAX);
                _AIchangeDirTimer.start();
            }
            if (!_AIshootTimer.enabled)
            {
                Shoot();
                _AIshootTimer.start();
            }
        }
        #endregion
    }
}
