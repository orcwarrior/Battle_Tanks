using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Battle_Tanks.Menu;
using Battle_Tanks.Objects;
using Battle_Tanks.Sound;
using Battle_Tanks.Visuals;
using Battle_Tanks.Time;
using System.Drawing;

namespace Battle_Tanks
{
    /// <summary>
    /// Klasa Engine jest korzeniem ca³oœci aplikacji, przechowuje odpowiednie
    /// referencje umo¿liwiaj¹c bezpoœredni lub poœredni dostêp do wszelkich obiektów
    /// wygenerowanych przez grê.
    /// Najbardziej istotne s¹ dla niej metody <see cref="OnUpdateFrame"/> oraz
    /// <see cref="OnRenderFrame"/> realizuj¹ce praktycznie ca³¹ logikê gry.
    /// </summary>
	public class Engine : OpenTK.GameWindow
    {
        /// <summary>Plik log'a w którym zapisuj¹ siê informacjê przydatnê w procesie debugowania.</summary>
        static public logStream log = new logStream("log.txt");
        static private string _mapsPath = "../maps/";

        #region fields
        /// <summary> Wielkosc siatki gry czyli wielkoœæ jednego obiektu gry </summary>
        public static int gridSize { get; private set; }
        /// <summary>Obecny poziom (mapa) gry.</summary>
        public int currentLevel { get; private set; }

        /// <summary>
        /// Punkty w których odradzaj¹ siê przeciwnicy na obecnej mapy, ta lista jest wype³niania
        /// przy ka¿dym ³adowaniu mapy - powiedzmy ze u nas spawn-pointy sa zaznaczane 
        /// na mapie i nie musz¹ byæ to zawsze te same punkty.
        /// </summary>
        public List<staticObject> enemySpawnPoints { get; private set; }

        /// <summary>
        /// Wartoœæ oznaczaj¹ca maksymaln¹ iloœæ przeciwników na mapie na raz, liczba bedzie ustawiana na inn¹
        /// zale¿nie od tego czy wybrano tryb dla 1 czy dla wielu graczy, tak¿e kolejne poziomy mogly by zwiekszaæ iloœæ
        /// przeciwnikow na mapie (POMYS£).
        /// </summary>
        public int maxEnemiesOnMap { get; private set; }

        /// <summary>
        /// Tablica z 2 obiektami klasy Controls kazda dla 1 z graczy
        /// 0 - 1 gracz
        /// 1 - 2 gracz
        /// </summary>
        public Controls[] playerControls { get; private set; }

        /// <summary>Obecny stan gry (MENU/GAME) </summary>
        public eGameState state { get; private set; }

		private List<Enemy> _enemiesOnMap;
		private List<Enemy> _enemiesToAdd;
		private Player _player1;
		private Player _player2;
        private List<powerUpObject> _powerUps; private Timer _powerUpsTimer;
		private List<Projectile> _projectiles;
		private statistics _stats;
		private menuManager _menuMgr;
		private soundManager _sndMgr;
		private timeManager _timeMgr;
		private staticObject[,] _staticObjects;
        private int enemiesCount;
        /// <summary> Ostatnio zaladowana bitmapa z której wygenerowano </summary>
        private Bitmap _loadedMap;
        private int _lastUsedSpawnPoint;
        #endregion

        #region keyboardDev class
        /// <summary>
        /// Klasa umo¿liwaj¹ca ³atwy dostêp (odczyt) stanu
        /// klawiatury przez obiekt klasy Engine, jak i klasy które
        /// tylko posi¹daj¹ca referencje do obiektu klasy Engine.
        /// </summary>
        public class keyboardDev
        {
            private Engine engine;
            
            internal keyboardDev(Engine e)
            {engine = e;}

            /// <summary>
            /// Indekser dziêki któremu pobraæ obecny
            /// stan ka¿dego z klawiszy klawiatury.
            /// </summary>
            /// <param name="k">Wybrany klawisz</param>
            /// <returns>Zwraca prawdê jeœli klawisz jest naciœniêty, fa³sz jeœli jest inaczej.</returns>
            public bool this[OpenTK.Input.Key k]
            { get { return engine.Keyboard[k]; } }
        }
        /// <summary>
        /// Obiekt <see cref="Battle_Tanks.Engine.keyboardDev"/> dziêki któremu mo¿emy w ³atwy sposób sprawdziæ stan klawiszy.
        /// </summary>
        public keyboardDev keyboardState;
        #endregion
     

        /// <summary>Inicjuje silnik gry </summary>
        public Engine()
            : base(1280, 800)
        {
            // NOTE: Pomimo tego ze wielkoœæ siatki jest ustawiona na 58x58 pikseli
            // tekstury musz¹ byæ tworzone w rozmiarze 64x64 jako ze jest to wielokrotoœæ
            // liczby 2 i dla kart graficznych pracowanie na teksturach w teksturach o rozdzielczoscach
            // które nie s¹ wielokrotnoœci¹ 2jki to ogromne koszty obliczeñ!
            gridSize = 58;
            currentLevel = 0;
            _lastUsedSpawnPoint = 0;
            maxEnemiesOnMap = (_player2 != null) ? 5 : 4;
            enemiesCount = 20;
            virtualObject.setEngine(this);
            keyboardState = new keyboardDev(this);
            _sndMgr = new soundManager();
            _timeMgr = new timeManager();
            enemySpawnPoints = new List<staticObject>();
            _powerUps = new List<powerUpObject>();
            _projectiles = new List<Projectile>();
            _enemiesToAdd = new List<Enemy>();
            _enemiesOnMap = new List<Enemy>();
            playerControls = new Controls[2];
            playerControls[0] = new Controls(1);
            playerControls[1] = new Controls(2);
            //WindowState = OpenTK.WindowState.Fullscreen;
            log.WriteLine("[LOG START]");
        }

		/// <summary> Metoda dodaj¹ca pocisk do listy _Projectiles </summary>
		public void addProjectile(Projectile proj)
		{
            _projectiles.Add(proj);
		}

		/// <summary>
		/// Sprawdza kolizje dynamiczn¹ obiektu z innymi obiektami (dynamicznymi)
		/// </summary>
		/// <param name="obj">Obiekt którego kolizja ma zostaæ sprawdzona</param>
		/// <returns>Lista obiektów z ktorymi koliduje, null jezeli takowych brak</returns>
		public List<dynamicObject> checkCollisionDyn(dynamicObject obj)
		{
            List<dynamicObject> dynCollObjs = new List<dynamicObject>();
            
            // najpierw sprawdŸ kolizje z pociskami:
            for (int i = 0; i < _projectiles.Count; i++)
            {
                if (obj != _projectiles[i] && obj.boundingBox.IntersectsWith(_projectiles[i].boundingBox))
                    dynCollObjs.Add(_projectiles[i]);
            }
            // czolgi graczy
            if (obj.boundingBox.IntersectsWith(_player1.boundingBox)) dynCollObjs.Add(_player1);
            if (obj.boundingBox.IntersectsWith(_player2.boundingBox)) dynCollObjs.Add(_player2);

            for (int i = 0; i < _enemiesOnMap.Count; i++)
            {
                if (obj != _enemiesOnMap[i] && obj.boundingBox.IntersectsWith(_enemiesOnMap[i].boundingBox))
                    dynCollObjs.Add(_enemiesOnMap[i]);
            }

            return dynCollObjs;
		}
		/// <summary>
		/// Sprawdza kolizje statyczna obiektu z innymi obiektami (statycznymi)
		/// Obiekt dynamiczny mo¿e kolidowaæ tylko z jednym polem statycznym na raz.
        /// * Dodano sprawdzanie kolizji z list¹ power-upów na mapie, jako ¿e s¹
        ///   one obiektami klasy pochodnej dla staticObject.
        ///   ICH TEST PRZEPROWADZANY JEST W PIERWSZEJ KOLEJNOSCI
		/// </summary>
		/// <param name="obj">Obiekt którego kolizja ma zostaæ sprawdzona</param>
		/// <returns>Obiekt statyczny z którym koliduje dany obiekt dynamiczny</returns>
		public staticObject checkCollisionStat(dynamicObject obj)
		{
            List<Point> myGridPositions = new List<Point>();
            Point basePt = obj.gridPos;
            myGridPositions.Add(basePt);

            //sprawdzenie kolizji z power-up'ami:
            if(obj is Vehicle) // KOLIZJA Z POWER-UP'ami tylko dla pojazdów!
            for (int i = 0; i < _powerUps.Count; i++)
            {
                if (obj.boundingBox.IntersectsWith(_powerUps[i].boundingBox))
                    return _powerUps[i];
            }

            //Sprawdzenie czy obiekt nie wychodzi poza obszar mapy:
            if(!new Rectangle(0,0,_loadedMap.Width*gridSize,_loadedMap.Height*gridSize).Contains(obj.boundingBox))
                return new staticObject(new Point(-1, -1), eStaticObjType.MAP_BORDER);

            bool onXGrid = obj.onGridX;
            bool onYGrid = obj.onGridY;
            if (onXGrid == false) myGridPositions.Add(new Point(basePt.X + 1, basePt.Y));
            if (onYGrid == false)
            {
                myGridPositions.Add(new Point(basePt.X, basePt.Y + 1));
                if (onXGrid == false) myGridPositions.Add(new Point(basePt.X + 1, basePt.Y + 1));
            }
            for(int i=0;i<myGridPositions.Count;i++)
            {
                int x = myGridPositions[i].X;
                int y = myGridPositions[i].Y;
                if (x >= 0 && x < _loadedMap.Width
                && y >= 0 && y < _loadedMap.Height)
                {
                    staticObject testObj = _staticObjects[x, y];
                    if (testObj != null && testObj.intersectWith(obj.boundingBox) && testObj.isColideable)
                    {
                        string msg = "[STAT-COLL] " + obj.ToString() + " Kolizja w pkt(myGrid)(" + x + "," + y + ") z obj:" + testObj.ToString();
                        log.WriteLine(msg);
                        return testObj;
                    }
                }
            }
            return null; //nie znaleziono kolizji z obiektami
		}
		/// <summary>Wczytuje grê z podanego jako parrametr obiektu Savegame </summary>
		/// <param name="sav">Savegame do wczytania</param>
		public void loadGame(Savegame sav)
		{
			throw new NotImplementedException();
		}
		/// <summary>Odtwarza plik dŸwiêkowy, funkcja w³aœciwie poprostu przekazuje parametr dalej do <see cref="soundManager"/> i jego <see cref="soundManager.playStream(string)"/></summary>
		public void playSound(string filename)
		{
            _sndMgr.playStream(filename);
		}

        /// <summary>
        /// Metoda wywo³ywana przez klase bazowa <see cref="GameWindow"/>
        /// tutaj dodatkowo inicjuje macierz przekszta³ceñ oraz inne
        /// opcje OpenGL, by na koniec wywo³aæ metodê <see cref="_nextLevel"/>
        /// </summary>
        /// <param name="e">[Jak w bazowej klasie] / tutaj niewykorzystywane</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			_initMatrix();
			GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.ColorMaterial);
            //GL.Disable(EnableCap.DepthTest);
			//Ustawiamy kolor czyszczenia(tla):
			GL.ClearColor(new OpenTK.Graphics.Color4(0.1f,0.1f,0.1f,1f));
           
            //generujemy mape itd:
            _nextLevel();
		}

        /// <summary>
        /// Metoda wywo³ywana przez klase bazowa <see cref="GameWindow"/>
        /// Jedyne co wykonuje, to zamyka plik z <see cref="log"/> z Informacjami
        /// do debugowania, tak aby go nie uszkodziæ.
        /// </summary>
        /// <param name="e">[Jak w bazowej klasie] / tutaj niewykorzystywane</param>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            log.Close();
        }
		/// <summary>
		/// Funkcja nadpisuj¹ca t¹ sam¹ funkcje klasy GameWindow ma za zadanie zale¿nie od obecnego stanu gry:
		/// *MENU - wywo³aæ Render menuManagera
		/// *GAME - wywo³aæ Render wszystkich statycznycch obiektów mapy, pojazdów gracza i wroga a na koniec pocisków i power-upów.
		/// </summary>
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			for (int i = _loadedMap.Width - 1; i >= 0; i--)
				for (int j = _loadedMap.Height-1; j >= 0; j--)
					if(_staticObjects[i,j]!=null)
					_staticObjects[i, j].Render();

            if (_player1 != null)
                _player1.Render();
            if (_player2 != null)
                _player2.Render();

            for (int i = 0; i < _projectiles.Count; i++)
                _projectiles[i].Render();

            for (int i = 0; i < _enemiesOnMap.Count; i++)
                _enemiesOnMap[i].Render();

            // Power-up'y:
            for (int i = 0; i < _powerUps.Count; i++)
                _powerUps[i].Render();

            fonts.Default.Print((int)RenderFrequency + " fps", new Vector2(0, 0));

            
            
			SwapBuffers();
		}
		/// <summary>
		/// Funkcja nadpisuj¹ca t¹ sam¹ funkcje klasy GameWindow ma za zadanie zale¿nie od obecnego stanu gry:
		/// *MENU - wywo³aæ Render menuManagera
		/// *GAME - wywo³aæ Render wszystkich statycznycch obiektów mapy, pojazdów gracza i wroga a na koniec pocisków i power-upów.
		/// </summary>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
            if (Keyboard[OpenTK.Input.Key.Escape]) Exit();
			base.OnUpdateFrame(e);
            Title = "Battle Tanks - " + (int)this.UpdateFrequency + " fps";

            _timeMgr.Update();

            if (!_powerUpsTimer.enabled) _spawnPowerUP();

            if(_player1!=null)
            _player1.Update();
            if(_player2!=null)
            _player2.Update();
            // Pociski:
            for (int i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].Update();
                if (_projectiles[i].toDispose == true)
                {
                    _projectiles.RemoveAt(i); i--;
                }
            }
            // Spawn przeciwników, okreslanie czy gra jest zakonczona
            if (_enemiesOnMap.Count < maxEnemiesOnMap)
            {
                if (_enemiesToAdd.Count > 0)
                {
                    Enemy newEnemy = _enemiesToAdd[0];
                    _SpawnEnemy(newEnemy);
                    _enemiesToAdd.RemoveAt(0);
                }
                else if (_enemiesOnMap.Count==0)//TODO: podsumowanie wynikow, etc
                    _nextLevel();
            }
            // Przeciwnicy:
            for (int i = 0; i < _enemiesOnMap.Count; i++)
            {
                _enemiesOnMap[i].Update();
                if (_enemiesOnMap[i].destroyed)
                {
                    _enemiesOnMap.RemoveAt(i);
                    i--;
                }
            }
            // Power-up'y:
            for (int i = 0; i < _powerUps.Count; i++)
            {
                if (_powerUps[i].toDispose)
                {
                     _powerUps.RemoveAt(i); i--;
                }
                else _powerUps[i].Update();
            }

            if (Keyboard[OpenTK.Input.Key.ControlLeft] && Keyboard[OpenTK.Input.Key.D])
                _breakpointEvent();
		}

        private void _breakpointEvent()
        {
            staticObject collObj = checkCollisionStat(_player1);
            if(collObj==null) return;
        }
        /// <summary>
        /// Metoda utworzy odpowiedni obiekt dla danego jako argument piksela
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void _createObjectFromMapPixel(int x, int y)
		{
            Color pixel = _loadedMap.GetPixel(x, y);
            if (pixel == mapContants.NOTHING)
                return;
            else if (pixel == mapContants.BUSH)
            {
                _staticObjects[x, y] = staticObjectFactory.Create(new Point(x * gridSize, y * gridSize), eStaticObjType.BUSH);
            }
            else if (pixel == mapContants.BASE)
            {
                _staticObjects[x, y] = staticObjectFactory.Create(new Point(x * gridSize, y * gridSize), eStaticObjType.BASE);
            }
            else if (pixel == mapContants.ENEMY)
            {
                staticObject spawnPt = staticObjectFactory.Create(new Point(x * gridSize, y * gridSize), eSpawnPointType.ENEMY);          
                _staticObjects[x, y] = spawnPt;
                enemySpawnPoints.Add(spawnPt);
            }
            else if (pixel == mapContants.PLAYER1)
            {
                if (_player1 == null) //Pierwszy poziom, gracz nadal nie jest utworzony:
                {
                    _player1 = new Player(1, new Point(x * gridSize, y * gridSize), objSpeed.DEFAULT, eDir.U, 2, playerControls[0]);
                    Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(_player1.KeyEvent);
                }
                else _player1.Reinit(x * gridSize, y * gridSize);
            }
            else if (pixel == mapContants.PLAYER2)
            {
                if (_player2 == null)
                {
                    _player2 = new Player(2, new Point(x * gridSize, y * gridSize), objSpeed.DEFAULT, eDir.U, 2, playerControls[1]);
                    Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(_player2.KeyEvent);
                }
                else _player2.Reinit(x * gridSize, y * gridSize);
            }
            else if (pixel == mapContants.WATER)
            {
                _staticObjects[x, y] = staticObjectFactory.Create(new Point(x * gridSize, y * gridSize), eStaticObjType.WATER);
            }
            else
            {   // cegla:
                int cegla_i = 0;
                for (cegla_i = 0; cegla_i < mapContants.BRICK_MAX; cegla_i++)
                {
                    if (pixel == mapContants.BRICK[cegla_i])
                        break;
                }
                if (cegla_i < mapContants.BRICK_MAX)
                {//Dany pixel ma kolor jak cegla z tablicy BRICK o indeksie cegla_i:
                    _staticObjects[x, y] = staticObjectFactory.Create(new Point(x * gridSize, y * gridSize), eStaticObjType.BRICK, mapContants.intToSobQuarters(cegla_i));                 
                }
                // twarda cegla
                int twarda_cegla_i = 0;
                for (twarda_cegla_i = 0; twarda_cegla_i < mapContants.BRICK_MAX; twarda_cegla_i++)
                {
                    if (pixel == mapContants.BRICK_HARD[twarda_cegla_i])
                        break;
                }
                if (twarda_cegla_i < mapContants.BRICK_MAX)
                {
                    _staticObjects[x, y] = staticObjectFactory.Create(new Point(x * gridSize, y * gridSize), eStaticObjType.BRICK_HARD, mapContants.intToSobQuarters(cegla_i));
                }
            }
		}
        /// <summary>
        /// Inicjalizacja macierzu przeksztalceñ
        /// wymagane do prawidlowego wyœwietlania obieków
        /// graficznych.
        /// </summary>
        private void _initMatrix()
		{
			int[] viewPort = new int[4];

			GL.GetInteger(GetPName.Viewport, viewPort);
			GL.MatrixMode(MatrixMode.Projection);
			GL.PushMatrix();
			GL.LoadIdentity();

			GL.Ortho(viewPort[0], viewPort[0] + viewPort[2], viewPort[1] + viewPort[3], viewPort[1], -1, 1);
			GL.MatrixMode(MatrixMode.Modelview);
			GL.PushMatrix();
			GL.LoadIdentity();
			//GL.Translate(0.375, 0.375, 0.0);
			GL.Translate(0.0, 0.0, 0.0);
			GL.PushAttrib(AttribMask.DepthBufferBit);
		}

		/// <summary>
		/// Metoda wywolana przy zakonczeniu gry
		/// </summary>
		private void _gameOver()
		{
			throw new NotImplementedException();
		}

        private List<Enemy> _generateEnemies()
		{
            List<Enemy> enemies = new List<Enemy>();
            // szanse na wylosowanie tego typu przeciwników
            //(15-35)
            int heavyChance = Math.Min(33,15 + currentLevel);          
            //(20-30)
            int improvedChance = Math.Min(27, 20 + currentLevel);
            //(20-40)
            int agiChance = Math.Min(40, 20 + currentLevel);
            // normal - reszta

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < enemiesCount-1; i++)
            {   //TODO: losowanie statystyk wroga (hp / dmg)
                int r = rnd.Next(100);
                int j = 100;
                if (r > (j = (j - agiChance) ))
                    enemies.Add((Enemy)vehicleFactory.createEnemyTank(eTankType.AGILE, 1 + ((r - j) / (agiChance / 2 + 15 - currentLevel)), 1, Point.Empty));
                else if (r > (j = (j - improvedChance) ))
                    enemies.Add((Enemy)vehicleFactory.createEnemyTank(eTankType.IMPROVED, 1 + ((r - j) / (improvedChance / 2 + 10 - currentLevel)), 1, Point.Empty));
                else if (r > (j = (j - heavyChance) ))
                    enemies.Add((Enemy)vehicleFactory.createEnemyTank(eTankType.HEAVY, 2 + ((r - j) / (heavyChance / 2 -currentLevel)), 1 + ((r - j) / (heavyChance / 2 + 5 - currentLevel)), Point.Empty));
                else
                    enemies.Add((Enemy)vehicleFactory.createEnemyTank(eTankType.NORMAL, 1, 1, Point.Empty));
            }
            //BOSS:
            enemies.Add((Enemy)vehicleFactory.createEnemyTank(eTankType.HEAVY, 5 + currentLevel, 2, Point.Empty));
               
            return enemies;
		}
        /// <summary>
        /// Metoda generuje mape dla poziomu
        /// (wczytuje plik zgodnie z map($currentLevel).png)
        /// </summary>
		private void _generateMap()
		{
            string path = _mapsPath + "map"+currentLevel+".png";
            _loadedMap = new Bitmap(path);
            _staticObjects = new staticObject[_loadedMap.Width, _loadedMap.Height];
            for (int x = 0; x < _loadedMap.Width; x++)
            {
                for (int y = 0; y < _loadedMap.Height; y++)
                {
                    _createObjectFromMapPixel(x, y);
                }
            }
		}

        private void _loadMap(string path)
		{
			throw new NotImplementedException();
		}

		private void _nextLevel()
		{
            currentLevel++;
            // zwieksz il. przeciwnikow co poziom
            enemiesCount++;
            _generateMap();
            _enemiesToAdd =_generateEnemies();
            _powerUpsTimer = new Timer(eUnits.MSEC, (10 + _powerUps.Count * 20) * 1000, (1 + _powerUps.Count * 7) * 1000, true, false);
            _powerUpsTimer.start();
            _powerUps.Clear();
            _projectiles.Clear();
		}

		private void _SpawnEnemy(Enemy tank)
		{
            _enemiesOnMap.Add(tank);
            _lastUsedSpawnPoint = (_lastUsedSpawnPoint+1)%enemySpawnPoints.Count;
            Point pos = new Point(enemySpawnPoints[_lastUsedSpawnPoint].gridPos.X * Engine.gridSize, enemySpawnPoints[_lastUsedSpawnPoint].gridPos.Y * Engine.gridSize);
            tank.MoveAbs(pos);
		}
        private void _spawnPowerUP()
        {
            Random rnd = new Random();
            _powerUps.Add(staticObjectFactory.Create(new Point(rnd.Next((_loadedMap.Width-1)*gridSize),rnd.Next((_loadedMap.Height-1)*gridSize)),(ePowerUpType)rnd.Next((int)ePowerUpType.MAX)));
            _powerUpsTimer.start();
        }
		// Entry Point (Main):
		[STAThread]
		static void Main()
		{
			using (Engine game = new Engine())
			{
				game.VSync = VSyncMode.Adaptive;
				game.Title = "Battle Tanks";
				game.Run(60);
			}
		}

        internal void destroyAllEnemies(Player pc)
        {
            for (int i = 0; i < _enemiesOnMap.Count; i++)
            {
                _enemiesOnMap[i].Kill();
                pc.addPoints(_enemiesOnMap[i].hp * 10);
            }
        }

        internal void holdEnemies()
        {

        }
    }



}
