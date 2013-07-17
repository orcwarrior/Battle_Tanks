using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Battle_Tanks.Objects
{
	public class Weapon
    {
        #region fields
        public int damage;
		public float speed;
        public int maxBullets;
        public bool canDestroyStrongBrick { get { return false; } }
        public bool canShoot { get { return true; } }
        public Vehicle owner { get; private set; }

        private List<Projectile> _bullets;
        #endregion

        /// <summary>
        /// wystrzelenie pocisku
        /// </summary>
        /// <param name="ownerPos">obecna pozycja Pojazdu kt�ry utworzy�(wystrzelil) pocisk</param>
        public void Shoot(Point ownerPos)
		{
            Projectile p = new Projectile(ownerPos, speed, owner.direction, this);
            _bullets.Add(p);            
		}
        /// <summary>
        /// tworzenie broni
        /// </summary>
        /// <param name="ownr"> pojazd kt�ry posiada t� bro�</param>
		public Weapon(Vehicle ownr)
		{
            owner = ownr;
            damage = 1;
            maxBullets = 1;
            speed = objSpeed.BULLET_BASE;
            _bullets = new List<Projectile>();
		}

	}
}
