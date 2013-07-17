using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Klasa odpowiadaj¹ca za wystrzelone pociski, poruszaj¹ce siê ju¿ po mapie.
    /// Musz¹ one zawieraæ referencje do pojazdu z którego zostaly wystrzelone(<see cref="Vehicle"/>)
    /// oraz broni, której typu jest pocisk(<see cref="Weapon"/>).
    /// </summary>
	public class Projectile : dynamicObject
    {
        #region fields
        private Vehicle _firedBy;
        private Weapon _weapon;
        public bool toDispose { get; private set; }
        #endregion

        #region methods
        /// <summary>
        /// Konstruktor który tak¿e dodatkowo wywo³uje metode
        /// <see cref="Engine.addProjectile"/> dodaj¹c tworzony obiekt do listy
        /// pocisków engine, tak aby póŸniej by³ przetwarzany przez <see cref="Engine.OnUpdateFrame"/>
        /// oraz <see cref="Engine.OnRenderFrame"/>.
        /// Konstruktor przesuwa te¿ odpowiednio pocisk, tak aby wygl¹da³
        /// na wystrzelony prosto z lufy czo³gu.
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="speed">Predkoœæ pocisku</param>
        /// <param name="dir">Kierunek</param>
        /// <param name="weap">Broñ z której wystrzelono pocisk.</param>
        public Projectile(Point pos, float speed, eDir dir, Weapon weap)
			: base(pos,speed,eDir.U)
		{
            _weapon = weap;
            _firedBy = _weapon.owner;
            usedEngine.addProjectile(this);
            //wycentruj pocisk:
            Move(Engine.gridSize / 2-5, Engine.gridSize / 2-4);

            visual = new Visuals.Sprite("BULLET.PNG", pos);
            //bugfix: (rotacja kuli/ ustawienie orgin'a rotacji)
            direction = dir;
            visual.rotationOrgin = new Point(4, 8);
            _generateBBox();
		}

		public override void Update()
        {
            tryMovement();
            List<dynamicObject> dynColls = usedEngine.checkCollisionDyn(this);
            for (int i = 0; i < dynColls.Count; i++)
            {
                if (dynColls[i] is Projectile)
                {
                    toDispose = ((Projectile)dynColls[i]).toDispose = true;
                    return;//this do zniszczenia, przerwij update
                }
                else if (dynColls[i].GetType() != _firedBy.GetType())
                {
                    ((Vehicle)dynColls[i]).Damage(_weapon);
                    toDispose =  true;
                    return;
                }
                else if (dynColls[i] != _firedBy)
                {
                    toDispose = true;
                    return;
                }
            }

            staticObject collObj = usedEngine.checkCollisionStat(this);
            if (collObj != null)
            {
                // jezeli zniszczono obiekt statyczny, to zniszcz tez pocisk
                // (NANANA) - woda jest wyj¹tkiem!
                toDispose = (collObj.isColideable && collObj.objType!=eStaticObjType.WATER);
                collObj.Destroy(boundingBox);
            }
		}
        public override void Render()
        {
            base.Render();
        }
        /// <summary>
        /// Nadpisanie dziedziczonej metody wynika z innych rozmiarow bounding-box'a dla
        /// tej klasy.
        /// </summary>
        protected override void _generateBBox()
        {
            bBox = new Rectangle(Pos.X-8, Pos.Y-8, 16, 16);
        }
        #endregion
    }
}
