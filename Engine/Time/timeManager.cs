using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Battle_Tanks.Time
{
    /// <summary>
    /// Klasa obs³uguj¹ca wszystkie obiekty <see cref="Timer"/> utworzone i obecnie aktywne.
    /// </summary>
	public class timeManager
    {
        #region fields
        private Stopwatch _msecMeasure;
		private List<Timer> _fpsTimers;
		private List<Timer> _msecTimers;
        #endregion

        #region methods
        /// <summary>
        /// Konstruktor, wywo³uje
        /// <see cref="Timer.setTimeManager"/> tak aby ka¿dy
        /// tworzony <see cref="Timer"/> móg³ automatycznie dodaæ siê
        /// do list <see cref="_fpsTimers"/>/<see cref="_msecTimers"/>.
        /// </summary>
        public timeManager()
        {
            Timer.setTimeManager(this);
            _msecTimers = new List<Timer>();
            _fpsTimers = new List<Timer>();
            _msecMeasure = new Stopwatch();
        }
        /// <summary>
        /// Dodaje Timer'a do odpowiedniej dla niego listy
        /// przechowywana przez klasê, wywo³ywane przez
        /// <see cref="Timer.start"/>.
        /// </summary>
        /// <param name="timer">Obiekt Timer</param>
		public void addTimer(Timer timer)
		{
            if (timer.type == eUnits.MSEC)
                _msecTimers.Add(timer);
            else if (timer.type == eUnits.Frames)
                _fpsTimers.Add(timer);
		}

        /// <summary>
        /// Usuwa Timer'a do odpowiedniej dla niego listy
        /// przechowywana przez klasê, wywo³ywane przez
        /// <see cref="Timer.stop"/>.
        /// </summary>
        /// <param name="timer">Obiekt Timer</param>
		public bool removeTimer(Timer timer)
        {
            if (timer.type == eUnits.MSEC)
                return _msecTimers.Remove(timer);
            else if (timer.type == eUnits.Frames)
                return _fpsTimers.Remove(timer);
            return false;
		}
        /// <summary>
        /// Update odejmuje od <see cref="Timer"/>'ów zgromadzonych na listach
        /// odpowiednie wartoœci czasu/klatek które up³yne³y od ostatenigo wywo³ania.
        /// </summary>
		public void Update()
        {
            _msecMeasure.Stop();
            long msec_elapsed = _msecMeasure.ElapsedMilliseconds;

            // update lists:
            for (int i = 0; i < _msecTimers.Count; i++)
            {
                    _msecTimers[i].Update(msec_elapsed);
            }
            for (int i = 0; i < _fpsTimers.Count; i++)
            {
                    _fpsTimers[i].Update(1); // 1 frame passed
            }

            _msecMeasure.Reset();
            _msecMeasure.Start();
        }
    }
    #endregion
}
