using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Battle_Tanks.Time;
using QuickFont;

namespace Battle_Tanks.Objects
{
	public abstract class Vehicle : dynamicObject
    {
        #region statics
        static private OpenTK.Vector4 hpColorLow = new OpenTK.Vector4(0.9f, 0.2f, 0.2f, 0.5f);
        static private OpenTK.Vector4 hpColorHigh = new OpenTK.Vector4(0.2f, 0.9f, 0.2f, 0.5f);
        #endregion
        #region fields
        public bool hasShield { get; protected set; }

        protected int hpMax;
        protected Timer shieldTimer;
        protected Weapon _usedWeapon;
        private Visuals.Sprite shieldVisuals;
        #endregion
        #region properties
        public int myWeaponDamage { get { return _usedWeapon.damage; } }
        public bool destroyed { get { return hp <= 0; } }
        public int hp { get; protected set; }
        /// <summary> (TODO) Zwraca prawde jeœli obiekt jest obecnie wyrównany z siatk¹, inaczej fa³sz.</summary>
        #endregion

        #region methods
        protected Vehicle(Point pos, float speed, eDir dir, int hitPoints, Weapon weapon)
		: base(pos,speed,dir)
		{
            hp = hpMax = hitPoints;
            _usedWeapon = weapon;
            shieldVisuals = new Visuals.Sprite("TANK_SHIELD.png", new Point(0, 0));
            shieldVisuals.fps = 10;
            shieldVisuals.rotationOrgin = new Point(25, 31);
		}

		public void Damage(Weapon weap)
		{
            if (hasShield) { hasShield = false; return; }
            hp -= weap.damage;
            if(weap.owner is Player)
                ((Player)weap.owner).addPoints(10);
		}


		public virtual void Kill()
        {   // TODO: Visual FX
            hp = 0;
		}

		public void setWeapon(Weapon weap)
		{
            _usedWeapon = weap;
		}

		public override void Update()
		{
            base.Update();
		}
        public override void Render()
        {
            base.Render();
            if (hasShield)
            {
                shieldVisuals.setPos(visual.pos);
                //bugfix: positions need to be corrected on rotation
                shieldVisuals.rotation = visual.rotation;
                shieldVisuals.Render();
            }
            _renderHpBar();
        }

        protected virtual void Shoot()
        {
            _usedWeapon.Shoot(Pos);
        }

        private void _renderHpBar()
        {

            Visuals.Sprite box = new Visuals.Sprite("", Pos);
            OpenTK.Vector4 col = ((hpMax - hp) * hpColorLow + (hp * hpColorHigh)) / hpMax;
            box.move(new Point(-10, 5));
            //pierwsza zewnêtrzna ramka:
            box.width = 64;
            box.height = 8;
            box.color = new OpenTK.Graphics.Color4(col.X, col.Y, col.Z, col.W);//(0f, 0f, 0f, 0.9f);
            box.Render();

            //druga, wewn. ramka (czarna)
            box.width -= 2; box.height -= 2;
            box.move(new Point(1, 1));
            box.color = new OpenTK.Graphics.Color4(0f, 0f, 0f, col.W);//(0f, 0f, 0f, 0.9f);
            box.Render();

            //pasek:
            box.width -= 4; box.height -= 2;
            box.move(new Point(2, 1));
            box.width = (int)(box.width * ((double)hp / hpMax));
            box.color = new OpenTK.Graphics.Color4(col.X, col.Y, col.Z, col.W);//(0f, 0f, 0f, 0.9f);
            box.Render();

            fonts.hpBarFont.Options.Colour = new OpenTK.Graphics.Color4(col.X * 2f, col.Y * 2f, col.Z * 2f, 1f);
            fonts.hpBarFont.Print("[" + hp + "/" + hpMax + "]", new OpenTK.Vector2(Pos.X + 12, Pos.Y + 1));
        }
        #endregion
    }
}
