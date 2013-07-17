using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
namespace Battle_Tanks.Objects
{
    /// <summary>
    /// Klasa odpowiadaj¹ca za pojazdy sterowane przez graczy.
    /// Zyskuje przedewszystkim mo¿liwoœæ obs³ugi naciœniêc klawiszy
    /// oraz sprawdza stan klawiszy w celu wykonywania akcji dostêpnych w grze dla gracza.
    /// 
    /// </summary>
	public class Player : Vehicle
    {
        #region fields
        /// <summary> Pozycja w ktorej po zniszczeniu odrodzi siê gracz </summary>
        private Point _spawnPos;
		private int _playerID;
		private int _oldHp;
        private Controls _myControls;
        private bool isMoving;

        /// <summary>Pkt. uzbierane przez gracza na tym poziomie.</summary>
        public int points { get; private set; }
        /// <summary>Pkt. uzbierane przez gracza w ca³ej rozgrywce.</summary>
        public int totalPoints { get; private set; }
        /// <summary>Iloœæ ¿yæ gracza.</summary>
        public int lives { get; private set; }
        #endregion

        #region methods
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="ID">ID Gracza.</param>
        /// <param name="pos">Pozycja startowa.</param>
        /// <param name="spd">Szybkoœæ.</param>
        /// <param name="dir">Kierunek ruchu.</param>
        /// <param name="hitPoints">Pkt. ¯ycia</param>
        /// <param name="myControls">Sterowanie/Klawiszologia dla gracza.</param>
        public Player(int ID, Point pos, float spd, eDir dir, int hitPoints, Controls myControls)
            : base(pos, spd, dir, hitPoints, null)
        {
            _playerID = ID;
            _spawnPos = pos;
            _myControls = myControls;
            bBox = new Rectangle(new Point(pos.X + 2, pos.Y + 10), new Size(46, 53));
            if (ID == 1) visual = new Visuals.Sprite("TANK_P1.PNG", pos);
            if (ID == 2) visual = new Visuals.Sprite("TANK_P2.PNG", pos);
            visual.rotationOrgin = new Point(25, 31);
            setWeapon(new Weapon(this));
        }

        public override void Update()
		{
            _checkPressedKeys();
            if (!isMoving) return; // brak ruchu, brak kolizji

            tryMovement();
            staticObject collObj = usedEngine.checkCollisionStat(this);
            if (collObj != null)
            { // wykryto kolizje, przywróc ostatni¹ pozycje / zbierz power upa

                if (collObj.objType == eStaticObjType.POWER_UP)
                    _takePowerUp((powerUpObject)collObj);
                else undoMovement();
            }
		}
        public override void Render()
        {
            base.Render();
        }

        /// <summary>
        /// Wywo³ywane przez zdarzenie Battle_Tanks.Engine.Keyboard.KeyDown
        /// naciœniêcie klawisza mo¿e wywo³ywaæ odpowiednie akcje dla graczy.
        /// (Obecnie nacisniêcie <see cref="Battle_Tanks.Controls.keySHOOT"/> wywoluje metode
        /// <see cref="Shoot"/>).
        /// </summary>
        /// <param name="sender">Wysy³aj¹cy zdarzenie</param>
        /// <param name="arg">Argumenty zdarzeñ(Nacisniêty klawisz)</param>
        public void KeyEvent(object sender, OpenTK.Input.KeyboardKeyEventArgs arg)
		{
           // if      (arg.Key == _myControls.keyUP)      direction = eDir.U;
           // else if (arg.Key == _myControls.keyLEFT)    direction = eDir.L;
           // else if (arg.Key == _myControls.keyRIGHT)   direction = eDir.R;
           // else if ( arg.Key == _myControls.keyDOWN)    direction = eDir.D;
           // else ...
            if (arg.Key == _myControls.keySHOOT)   Shoot();    
		}        

		public override void Kill()
		{
			throw new NotImplementedException();
		}      

		public void loadScore(Savegame sav)
		{
			throw new NotImplementedException();
		}
        /// <summary>
        /// Nadpisanie funkcji pomocniczne, w celach debugowania.
        /// </summary>
        /// <returns>Zwraca ci¹g: "P$GRACZ ($PozSiatkiX,$PozSiatkiY)"</returns>
        public override string ToString()
        {
            return "P" + _playerID + "(" + gridPos.X + "," + gridPos.Y + ")";
        }

        internal void Reinit(int p, int p_2)
        {
            MoveAbs(p, p_2);
            hp = hpMax;
        }
        /// <summary>
        /// Wywo³anie metody powoduje wywo³anie metody <see cref="Vehicle.Shoot"/> oraz odtworzenie
        /// odpowiedniego dŸwiêku towarzysz¹cego wystrza³owi gracza.
        /// </summary>
        protected override void Shoot()
        {
            base.Shoot();
            usedEngine.playSound("SHOOT"+new Random().Next(2)+".WAV");
        }
        /// <summary>
        /// Dodaje pkt. do sumy pkt. gracza.
        /// </summary>
        /// <param name="p">Wartoœæ do dodania do sumy pkt.</param>
        public void addPoints(int p)
        {
            points += p;
        }

        private void _takePowerUp(powerUpObject pup)
        {
            switch(pup.powerUpType)
            {
                case ePowerUpType.LIVE: lives++; break;
                case ePowerUpType.BARREL: _updateWeapon_GunBarrel(); break;
                case ePowerUpType.SHIELD: hasShield = true; break;
                case ePowerUpType.STAR: _updateWeapon_Star(); break;
                case ePowerUpType.ROCKETS: usedEngine.destroyAllEnemies(this); break;
                case ePowerUpType.TIME_STOP: usedEngine.holdEnemies(); break;
            }
            pup.toDispose = true;
        }

        private void _updateWeapon_Star()
        {
            throw new NotImplementedException();
        }

        private void _updateWeapon_GunBarrel()
        {
            throw new NotImplementedException();
        }

        private void _checkPressedKeys()
        {
            isMoving = true;
            if (usedEngine.keyboardState[_myControls.keyUP]) direction = eDir.U;
            else if (usedEngine.keyboardState[_myControls.keyLEFT]) direction = eDir.L;
            else if (usedEngine.keyboardState[_myControls.keyRIGHT]) direction = eDir.R;
            else if (usedEngine.keyboardState[_myControls.keyDOWN]) direction = eDir.D;
            else /*  Gracz nie porusza siê w zadnym kierunku   */    isMoving = false;
        }
        #endregion

    }
}
