using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Battle_Tanks.Time;

namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Klasa odpowiadaj¹ca za power-up'y pojawiaj¹ce siê na mapie
    /// s¹ one obiektami statycznymi, to jednak wyró¿niaj¹ siê z typowych
    /// obiektów statycznych tym ¿e ich <see cref="Visuals.Sprite"/> migocze w odstêpach czasu.
    /// Oraz ze przy kolizji z którymkolwiek z graczy znikn¹, wywo³uj¹c odpowiednia metode zale¿nie od
    /// <see cref="powerUpType"/>.
    /// </summary>
	public class powerUpObject : staticObject
    {
        static private int powerUp_turnOnTime =  500;
        static private int powerUp_turnOffTime = 250;

        /// <summary>Typ power-up'a.</summary>
        public ePowerUpType powerUpType { get; private set; }
        /// <summary>Wartoœæ TRUE sygnalizuje silnikowi ¿e obiekt ma zostaæ usuniêty.</summary>
        public bool toDispose { get; set; } 
		Timer blinkTimer;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="pos">Pozycja startowa</param>
        /// <param name="powTyp">Typ Power-UP'a</param>
		public powerUpObject(Point pos, ePowerUpType powTyp)
			: base(pos,eStaticObjType.POWER_UP)
		{
            blinkTimer = new Timer(eUnits.MSEC, powerUp_turnOnTime, 0, false, false);
            powerUpType = powTyp;
                switch(powTyp)
                {
                    case ePowerUpType.BARREL:   visual = new Visuals.Sprite("PU_BARREL.png", pos); break;
                    case ePowerUpType.TIME_STOP: visual = new Visuals.Sprite("PU_TIMESTOP.png", pos); break;
                    case ePowerUpType.SHIELD:   visual = new Visuals.Sprite("PU_SHIELD.png", pos); break;
                    case ePowerUpType.STAR:     visual = new Visuals.Sprite("PU_STAR.png", pos); break;
                    case ePowerUpType.ROCKETS:  visual = new Visuals.Sprite("PU_ROCKETS.png",pos); break;
                    case ePowerUpType.LIVE:     visual = new Visuals.Sprite("PU_LIVE.png", pos); break;
                    default:                    visual = new Visuals.Sprite("PU_TIMESTOP.png", pos); break;
                }
        }
        /// <summary>
        /// Metoda Update uwzglêdnia tutaj zamiane stanu Sprite'a z W³¹czony/Wy³¹czony
        /// uruchamiaj¹c <see cref="Time.Timer"/>'a z odpowiednim parametrem d³ugoœci czasu.
        /// </summary>
		public override void Update()
        {
            base.Update();
            if (blinkTimer.enabled == false)
            {
                if (Math.Abs(blinkTimer.totalTime - powerUp_turnOnTime) < 1)
                    blinkTimer = new Timer(eUnits.MSEC, powerUp_turnOffTime, 0, false, false);
                else
                    blinkTimer = new Timer(eUnits.MSEC, powerUp_turnOnTime, 0, false, false);
                blinkTimer.start();
            }
		}
        /// <summary>
        /// Render uwzglêdnia tutaj dodatkowo migotanie Sprite'a.
        /// </summary>
		public override void Render()
		{
            if (Math.Abs(blinkTimer.totalTime - powerUp_turnOnTime) < 1)
                base.Render();
		}

    }
}
