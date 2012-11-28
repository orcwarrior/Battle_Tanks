using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Engine
{
	public class Timer
	{
		private bool _autostartOnRecreate;
		private Int64 _initTime;
		private Int64 _initTimeVar;
		private int _iterator;
		private bool _recreateOnDestroy;
		private static Timers_Manager _timeMgr;

		public float partDone
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public float partDoneReverse
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Int64 currentTime
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

		public bool enabled
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

		public Int64 totalTime
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

		public eUnits type
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

		public Timer(eUnits t, int timeAvg)
		{
			throw new NotImplementedException();
		}

		public Timer(eUnits t, Int64 timeAvg)
		{
			throw new NotImplementedException();
		}

		public Timer(eUnits t, int timeAvg, int timeVar)
		{
			throw new NotImplementedException();
		}

		public Timer(eUnits t, int timeAvg, int timeVar, bool recreate, bool recAutostart)
		{
			throw new NotImplementedException();
		}

		public Timer(eUnits t, Int64 timeAvg, Int64 timeVar)
		{
			throw new NotImplementedException();
		}

		public void start()
		{
			throw new NotImplementedException();
		}

		public void restart()
		{
			throw new NotImplementedException();
		}

		public bool stop()
		{
			throw new NotImplementedException();
		}

		public void Update(long time_passed)
		{
			throw new NotImplementedException();
		}

		private void _setIterator()
		{
			throw new NotImplementedException();
		}
	}
}
