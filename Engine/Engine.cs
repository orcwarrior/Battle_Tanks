using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
namespace Battle_Tanks.Engine
{
	public class Engine : OpenTK.GameWindow
	{
		private List<Enemy> _enemiesOnMap;
		private List<Enemy> _enemiesToAdd;
		private Player _player1;
		private Player _player2;
		private List<powerUpObject> _powerUps;
		private List<Projectile> _projectiles;
		private statistics _stats;
		private menuManager _menuMgr;
		private soundManager _sndMgr;
		private timeManager _timeMgr;
		private staticObject[] _staticObjects;

		/// <summary>
		/// Inicjuje silnik gry czyli:
		/// (?)(TODO)
		/// </summary>
		public Engine()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Obecny poziom (stage) gry.
		/// </summary>
		public int currentLevel	{ get;	private set; }

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

		/// <summary>
		/// Obecny stan gry (MENU/GAME)
		/// </summary>
		public eGameState state { get; private set; }

		/// <summary>
		/// Metoda dodaj¹ca pocisk do listy _Projectiles
		/// </summary>
		/// <param name="proj"></param>
		public void addProjectile(Projectile proj)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sprawdza kolizje dynamiczn¹ obiektu z innymi obiektami (dynamicznymi)
		/// </summary>
		/// <param name="obj">Obiekt którego kolizja ma zostaæ sprawdzona</param>
		/// <returns>Lista obiektów z ktorymi koliduje, null jezeli takowych brak</returns>
		public List<dynamicObject> checkCollisionDyn(dynamicObject obj)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Sprawdza kolizje statyczna obiektu z innymi obiektami (statycznymi)
		/// Obiekt dynamiczny mo¿e kolidowaæ tylko z jednym polem statycznym na raz.
		/// </summary>
		/// <param name="obj">Obiekt którego kolizja ma zostaæ sprawdzona</param>
		/// <returns>Obiekt statyczny z którym koliduje dany obiekt dynamiczny</returns>
		public staticObject checkCollisionStat(dynamicObject obj)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Wczytuje grê z podanego jako parrametr obiektu Savegame
		/// </summary>
		/// <param name="sav">Savegame do wczytania</param>
		public void loadGame(Savegame sav)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Odtwarza plik dŸwiêkowy
		/// Funkcja w³aœciwie poprostu przekazuje parrametr dalej do soundManagera i jego playStream
		/// </summary>
		/// <param name="filename"></param>
		public void playSound(string filename)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Funkcja nadpisuj¹ca t¹ sam¹ funkcje klasy GameWindow ma za zadanie zale¿nie od obecnego stanu gry:
		/// *MENU - wywo³aæ Render menuManagera
		/// *GAME - wywo³aæ Render wszystkich statycznycch obiektów mapy, pojazdów gracza i wroga a na koniec pocisków i power-upów.
		/// </summary>
		public override void onRender()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Funkcja nadpisuj¹ca t¹ sam¹ funkcje klasy GameWindow ma za zadanie zale¿nie od obecnego stanu gry:
		/// *MENU - wywo³aæ Render menuManagera
		/// *GAME - wywo³aæ Render wszystkich statycznycch obiektów mapy, pojazdów gracza i wroga a na koniec pocisków i power-upów.
		/// </summary>
		public override void onUpdate()
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Metoda utworzy odpowiedni obiekt dla danego jako argument piksela
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		private void _createObjectFromMapPixel(int x, int y)
		{
			throw new NotImplementedException();
		}

		private void _gameOver()
		{
			throw new NotImplementedException();
		}

		private List<Enemy> _generateEnemies()
		{
			throw new NotImplementedException();
		}

		private void _generateMap()
		{
			throw new NotImplementedException();
		}

		private void _loadMap(string path)
		{
			throw new NotImplementedException();
		}

		private void _nextLevel()
		{
			throw new NotImplementedException();
		}

		private static void _SpawnVehicle(Vehicle tank)
		{
			throw new NotImplementedException();
		}
	}
}
