using System;
using System.Collections.Generic;
using System.Text;

namespace Battle_Tanks.Time
{
    /// <summary>
    /// Klasa której obiekty maj¹ za zadanie mierzyæ czas w dostêpnych jednostkach <see cref="eUnits"/>
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
        /// <summary>Obecny czas timera wiêc <see cref="totalTime"/>-$Czas_Który_Up³yn¹³, lub poprostu czas który up³yn¹³ gdy Timer nie odlicza czasu, a jedynie go mierzy.</summary>
        public Int64 currentTime { get; private set; }
        /// <summary>Czas ca³kowity który jest odmierzany. </summary>
        public Int64 totalTime { get; private set; }
        /// <summary>Identyfikuje czy Timer jest w³¹czony(odmierza czas).</summary>
        public bool enabled { get; private set; }
        /// <summary>Jednostki w których odmierza czasomierz.</summary>
		public eUnits type { get; private set; }
        #endregion

        /// <summary>Wartoœæ w przedziale (0,1) wskazuj¹ca jaka czêœæ odmierzanego czasu ju¿ up³yne³a.</summary>
        public float partDone { get { return (float)currentTime / _initTime; } }
        /// <summary>Wartoœæ odwrotna do <see cref="partDone"/> przydatna w wypadku niektórych obliczeñ</summary>
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
        /// <param name="timeVar">Max. wartoœæ losowego odchylenia odmierzanej wartoœci od parametru "timeAvg".</param>
		public Timer(eUnits t, int timeAvg, int timeVar)
			:this(t,(Int64)timeAvg,(Int64)timeVar)
		{	}
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="t">Typ jednostek</param>
        /// <param name="timeAvg">Czas Do odmierzenia</param>
        /// <param name="timeVar">Max. wartoœæ losowego odchylenia odmierzanej wartoœci od parametru "timeAvg".</param>
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
        /// <param name="timeVar">Max. wartoœæ losowego odchylenia odmierzanej wartoœci od parametru "timeAvg".</param>
        /// <param name="recreate">jeœli TRUE to po odmierzeniu wskazanego czasu, Timer stworzy siê od nowa i ustawi czas na identyczna wartoœc(+/- odchylenie).</param>
        /// <param name="recAutostart">jeœli TRUE to po odtworzeniu Timer odrazu wystartuje.</param>
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
        /// <see cref="currentTime"/> na <see cref="totalTime"/> poczym wywo³ujê
        /// <see cref="start"/>.
        /// </summary>
		public void restart()
		{
			currentTime = totalTime;
			start();
		}

        /// <summary>
        /// Zatrzymuje odmierzanie czasu, ustawia <see cref="enabled"/> na FALSE.
        /// i usuwa Timer do <see cref="timeManager"/>'a(Wy³¹cza automatyczne aktualizacje).
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
        /// Aktualizacja wywo³ywana z <see cref="timeManager"/>'a która odejmuje odpowiednia wartoœæ
        /// od pola <see cref="currentTime"/> i jeœli mierzony czas ju¿ up³yn¹³ wywo³uje <see cref="stop"/>.
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
        /// Ustawia <see cref="_timeMgr"/> aby mo¿liwe by³o dodawanie/usuwanie
        /// Timer'ów z jego list aktualizacji.
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