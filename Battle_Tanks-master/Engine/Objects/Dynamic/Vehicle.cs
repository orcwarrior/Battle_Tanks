using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Engine
{
	public abstract class Vehicle
	{
		protected int hp;
		protected int hpMax;
		protected Timer shieldTimer;
		private Weapon _usedWeapon;

		public int myWeaponDamage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public eVehicleControler controller
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		protected bool onGrid
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		protected Vehicle(Point pos, float speed, eDir dir, int hitPoints, Projectile weapon)
		{
			throw new NotImplementedException();
		}

		public void Damage(int damagePts)
		{
			throw new NotImplementedException();
		}

		public virtual void Kill()
		{
			throw new NotImplementedException();
		}

		protected void Shoot()
		{
			throw new NotImplementedException();
		}

		public void setWeapon(Weapon weap)
		{
			throw new NotImplementedException();
		}

		public override void Update()
		{
			throw new NotImplementedException();
		}

		public override void Render()
		{
			throw new NotImplementedException();
		}
	}
}
