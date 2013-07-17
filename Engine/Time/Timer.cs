using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Time
{
    /// <summary>
    /// Klasa kt�rej obiekty maj� za zadanie mierzy� czas w dost�pnych jednostkach <see cref="eUnits"/>
    /// </summary>
	public class Timer
	{
        static private timeManager _timeMgr;

        #region fields 
        private bool _autostartOnRecreate;
		private Int64 _initTime;
		private Int64 _initTimeVar;
		private int _iterator;
		private bool _recreateOnDestroy;
		private int iterator;
        /// <summary>Obecny czas timera wi�c <see cref="totalTime"/>-$Czas_Kt�ry_Up�yn��, lub poprostu czas kt�ry up�yn�� gdy Timer nie odlicza czasu, a jedynie go mierzy.</summary>
        public Int64 currentTime { get; private set; }
        /// <summary>Czas ca�kowity kt�ry jest odmierzany. </summary>
        public Int64 totalTime { get; private set; }
        /// <summary>Identyfikuje czy Timer jest w��czony(odmierza czas).</summary>
        public bool enabled { get; private set; }
        /// <summary>Jednostki w kt�rych odmierza czasomierz.</summary>
		public eUnits type { get; private set; }
        #endregion

        /// <summary>Warto�� w przedziale (0,1) wskazuj�ca jaka cz�� odmierzanego czasu ju� up�yne�a.</summary>
        public float partDone { get { return (float)currentTime / _initTime; } }
        /// <summary>Warto�� odwrotna do <see cref="partDone"/> przydatna w wypadku niekt�rych oblicze�</summary>
        public float partDoneReverse { get { return 1f - partDone; } }


		#region constructors
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Typ jednostek</param>
        /// <param name="timeAvg">Czas Do odmierzenia</param>
		public Timer(eUnits t, int timeAvg)
			: this(t, (Int64)timeAvg)
		{	}

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Typ jednostek</param>
        /// <param name="timeAvg">Czas Do odmierzenia</param>
		public Timer(eUnits t, Int64 timeAvg)
		{
			_initTime = totalTime = timeAvg;
			type = t;
			currentTime = totalTime;
			_setIterator();
		}

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Typ jednostek</param>
        /// <param name="timeAvg">Czas Do odmierzenia</param>
        /// <param name="timeVar">Max. warto�� losowego odchylenia odmierzanej warto�ci od parametru "timeAvg".</param>
		public Timer(eUnits t, int timeAvg, int timeVar)
			:this(t,(Int64)timeAvg,(Int64)timeVar)
		{	}
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Typ jednostek</param>
        /// <param name="timeAvg">Czas Do odmierzenia</param>
        /// <param name="timeVar">Max. warto�� losowego odchylenia odmierzanej warto�ci od parametru "timeAvg".</param>
		public Timer(eUnits t, Int64 timeAvg, Int64 timeVar)
			: this(t,timeAvg)
		{
			Int64 rnd = new Random().Next((int)timeVar * 2) - timeVar;
			_initTimeVar = timeVar;
			totalTime = _initTime + rnd;
			currentTime = totalTime;
		}

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Typ jednostek.</param>
        /// <param name="timeAvg">Czas Do odmierzenia.</param>
        /// <param name="timeVar">Max. warto�� losowego odchylenia odmierzanej warto�ci od parametru "timeAvg".</param>
        /// <param name="recreate">je�li TRUE to po odmierzeniu wskazanego czasu, Timer stworzy si� od nowa i ustawi czas na identyczna warto�c(+/- odchylenie).</param>
        /// <param name="recAutostart">je�li TRUE to po odtworzeniu Timer odrazu wystartuje.</param>
		public Timer(eUnits t, int timeAvg, int timeVar, bool recreate, bool recAutostart)
			: this(t,timeAvg,timeVar)
		{
			_recreateOnDestroy = recreate;
			_autostartOnRecreate = recAutostart;
		}
		#endregion

        #region methods
        /// <summary>
        /// Rozpoczyna odmierzanie czasu, ustawia <see cref="enabled"/> na TRUE.
        /// i dodaje Timer do <see cref="timeManager"/>'a(Automatyczne aktualizacje).
        /// </summary>
        public void start()
		{
			if (enabled == false)
			{
				enabled = true;
				if (type == eUnits.Frames)
					_timeMgr.addTimer(this);
				else if (type == eUnits.MSEC)
                    _timeMgr.addTimer(this);
			}
		}

        /// <summary>
        /// Ustawia
        /// <see cref="currentTime"/> na <see cref="totalTime"/> poczym wywo�uj�
        /// <see cref="start"/>.
        /// </summary>
		public void restart()
		{
			currentTime = totalTime;
			start();
		}

        /// <summary>
        /// Zatrzymuje odmierzanie czasu, ustawia <see cref="enabled"/> na FALSE.
        /// i usuwa Timer do <see cref="timeManager"/>'a(Wy��cza automatyczne aktualizacje).
        /// </summary>
		public bool stop()
		{
            enabled = false;
			if (type == eUnits.Frames)
                return _timeMgr.removeTimer(this);
			else if (type == eUnits.MSEC)
                return _timeMgr.removeTimer(this);
			return false;
		}

        /// <summary>
        /// Aktualizacja wywo�ywana z <see cref="timeManager"/>'a kt�ra odejmuje odpowiednia warto��
        /// od pola <see cref="currentTime"/> i je�li mierzony czas ju� up�yn�� wywo�uje <see cref="stop"/>.
        /// </summary>
		public void Update(long time_passed)
		{
			currentTime += iterator * time_passed;

			// time was runing from certain time eg. 1000ms
			// till reach of 0ms
			if (currentTime <= 0 && iterator < 0)
			{
				if (_recreateOnDestroy)
				{
					long rnd = new Random().Next((int)_initTimeVar * 2) - _initTimeVar;
					currentTime = totalTime = _initTime + rnd;
					if (!_autostartOnRecreate)
					{
						this.stop();
					}
				}
				else // remove timer:
				{
					this.stop();
				};
			};
		}
        /// <summary>
        /// Ustawia <see cref="_timeMgr"/> aby mo�liwe by�o dodawanie/usuwanie
        /// Timer'�w z jego list aktualizacji.
        /// </summary>
        /// <param name="tm"></param>
        public static void setTimeManager(timeManager tm)
        {
            _timeMgr = tm;
        }

        private void _setIterator()
        {
            if (totalTime > 0) iterator = -1;
            else iterator = 1;
        }
        #endregion
    }
}