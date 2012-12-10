using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Engine
{
	public class staticObject
	{
		public staticObject(Point pos, eStaticObjType objTyp)
		{
			throw new NotImplementedException();
		}

		public staticObject(Point pos, eStaticObjType objTyp, eSpawnPointType spawnPtTyp)
		{
			throw new NotImplementedException();
		}

		public bool isColideable
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

		public bool isDestroyable
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

		public eStaticObjType objType
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

		public eSpawnPointType spawnPoint
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

		public virtual void Destroy()
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
