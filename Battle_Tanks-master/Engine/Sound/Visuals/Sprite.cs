using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Engine
{
	public class Sprite
	{
		public bool destroyWhenAniEnd;
		public static readonly int spriteTexSize = 32;
		private int _currentFrame;
		private int _frameTexWidth;
		private int _renderHeight;
		private int _renderWidth;
		private Texture _tex;

		private int _framesCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public int fps
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

		public int posX
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

		public int posY
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

		public Sprite(string texName)
		{
			throw new NotImplementedException();
		}

		public void Render()
		{
			throw new NotImplementedException();
		}
	}
}
