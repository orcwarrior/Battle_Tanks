using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Battle_Tanks.Visuals
{
	/// <summary>
	/// Klasa odpowiadajaca za wizualne przedstawienie obiektów mapy 
	/// </summary>
	public class Sprite
	{
        static private int fpsDefault = 3;
        #region fields
        /// <summary>TRUE - obiekt zostanie zniszczony po zakoñczeniu renderowania ostatniej klatki(JESZCZE NIEZAIMPLEMENTOWANO)!.</summary>
        public bool destroyWhenAniEnd;
        /// <summary>Wspó³rzêdne wg. których bêdzie wykonywany obrót Sprite'a (œrodek obrotu).</summary>
        public Point rotationOrgin;
        /// <summary>Kolor którym wype³nianie jest t³o sprite'a, lub jeœli Sprite nie posiada <see cref="_tex"/> bêdzie to kolor narysowanego prostok¹ta.</summary>
        public OpenTK.Graphics.Color4 color { get; set; }
        /// <summary>(Frames per second) je¿eli sprite jest animowany okreœla to jego il. klatek na sekunde.</summary>
        public int fps { get; set; }
        /// <summary>Pozycja w której zacznie siê rysowanie Sprite'a na ekranie.</summary>
        public Point pos { get; private set; }
        /// <summary>Wysokoœæ renderowanego obiektu sprite (w pikselach)</summary>
		public int height{get; set;} // no longer private set
        /// <summary>Szerokoœæ renderowanego obiektu sprite (w pikselach)</summary>
        public int width { get; set; }
        /// <summary>Obrót (w stopniach) który zostanie wykonany dla Sprite'a przed wyrenderowaniem.</summary>
        public double rotation { get; set; }
        
        //Te pola mozna uzyskac z _tex'a
        //private int _orgHeight, _orgWidth;
		private Texture _tex;
        private int _currentFrame;
        private float[] _framesTexXCoords;
        private int _lastMsec;
        #endregion

        #region properties
        /// <summary> Ustawia skalê lub odczytuje ja na podstawie wysokoœci obiektu Sprite przez wysokoœæ bazowa tekstury</summary>
        public double scale
        {
            get { if (_tex == null)return -1.0; return height / _tex.height; }
            set { if (_tex == null)return; width = (int)(_tex.height * value); height = (int)(_tex.height * value); }
        } 
        private int _framesCount { get { if (_tex == null) return 0; return _tex.width / _frameTexWidth; } }
        private int _frameTexWidth { get { if (_tex == null) return -1; return _tex.height; } }//frame-width = height of full texture
        private int _nxtFrame { get { return (_currentFrame + 1) % _framesCount; } }
        #endregion

        #region methods


        /// <summary>
        /// Podstawowy konstruktor klasy Sprite
        /// generuje odpowiedni obiekt klasy Texture. Wykonuje tak¿e
        /// szereg operacji maj¹cych na celu zainicjonowanie animacji sprite (o ile wystêpuje)
        /// </summary>
        /// <param name="texName">Plik wykorzystywanej tekstury</param>
        /// <param name="p">Punkt na ekranie w którym renderowany bêdzie obiekt</param>
		public Sprite(string texName, Point p)
		{
            if(texName!="")
			    _tex = new Texture(texName);
			pos = p;
			fps = fpsDefault;
            _currentFrame = 0;
            color = new OpenTK.Graphics.Color4(1f, 1f, 1f, 1f);
            // setujemy scale dzieki temu bedziemy mieli ustawione odpowiednie wartosci dla height i width
            // (takie same jak te parametry dla tekstury (_tex)
            scale = 1.0;
            _generateFramesTexcoords();
		}
        /// <summary>
        /// Renderuje sprite na ekranie, wykonuje te¿ wszelkie transformacje jak zmiana obecnej klatki
        /// czy odpowiedni obrót tekstury.
        /// </summary>
        public void Render()
		{   /* 0---1 How
             * |  /| Quad
             * | / | is
             * 3/--2 Made */
            _updateCurrentFrame();
			_spritePreparations();
			
			// Okreslamy ze bedziemy rysowaæ prostok¹ty (Quads)
			GL.Begin(BeginMode.Quads);
			
			// Rysujemy wierzcho³ki prostok¹ta wraz z odpowiednymi koordynantami tekstury
            GL.Color4(color);
            if (_tex != null)
            {
                GL.TexCoord2(_framesTexXCoords[_currentFrame], 1 / (float)_tex.height);							 // 0
                GL.Vertex2(pos.X, pos.Y);					 // 0
                GL.TexCoord2(_framesTexXCoords[_currentFrame + 1], 1 / (float)_tex.height);							 // 1
                GL.Vertex2(pos.X + width, pos.Y);			 // 1
                GL.TexCoord2(_framesTexXCoords[_currentFrame + 1], 1 - 1 / (float)_tex.height);							 // 2
                GL.Vertex2(pos.X + width, pos.Y + height); // 2
                GL.TexCoord2(_framesTexXCoords[_currentFrame], 1 - 1 / (float)_tex.height);							 // 3
                GL.Vertex2(pos.X, pos.Y + height);			 // 3
            }
            else
            {
                GL.Vertex2(pos.X, pos.Y);					 // 0					
                GL.Vertex2(pos.X + width, pos.Y);			 // 1					
                GL.Vertex2(pos.X + width, pos.Y + height);   // 2						
                GL.Vertex2(pos.X, pos.Y + height);			 // 3
            }
			//GL.BindTexture(
			// Konczymy rysowanie prostok¹ta:
            GL.End();
            _spriteEnd();

		}
        /// <summary>
        /// Ustawia pozycje Sprite'a absolutnie
        /// </summary>
        /// <param name="position">pozycja na ktor¹ ma zostaæ przeniesiony sprite</param>
		public void setPos(Point position)
		{
			pos = position;
		}
        /// <summary>
        /// Przesuwa obiekt o podan¹ pozycjê
        /// (Ustawia pozycje relatywnie)
        /// </summary>
        /// <param name="mov">Pozycja wg. której obiekt ma zostaæ przesuniêty.</param>
		public void move(Point mov)
		{
            pos = new Point(pos.X + mov.X, pos.Y + mov.Y);
		}
        /// <summary>
        /// Zmienia wartoœæ obrotu obiektu
        /// </summary>
        /// <param name="deg">obrót (w stopniach)</param>
        public void rotate(double deg)
        {
            rotation += deg;
        }


        private void _spritePreparations()
        {// Bindujemy teksture wczytana dla tego Sprite'a (o ile istnieje)
            if (_tex != null)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, _tex.glTexID);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            }
            else
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            if (rotationOrgin.IsEmpty)
            {
                GL.Translate(pos.X + width / 2, pos.Y + height / 2, 0.0);
                GL.Rotate(rotation, 0.0f, 0.0f, 1.0f);
                GL.Translate(-(pos.X + width / 2), -(pos.Y + height / 2), 0.0);
            }
            else
            {
                GL.Translate(pos.X + rotationOrgin.X, pos.Y + rotationOrgin.Y , 0.0);
                GL.Rotate(rotation, 0.0f, 0.0f, 1.0f);
                GL.Translate(-(pos.X + rotationOrgin.X), -(pos.Y + +rotationOrgin.Y), 0.0);
            }
        }

        private void _spriteEnd()
        {// Bindujemy teksture wczytana dla tego Sprite'a (o ile istnieje)
            if (_tex != null)
            {
                GL.Disable(EnableCap.Texture2D);
            }
            if (rotationOrgin.IsEmpty)
            {
                GL.Translate(pos.X + width / 2, pos.Y + height / 2, 0.0);
                GL.Rotate(-rotation, 0.0f, 0.0f, 1.0f);
                GL.Translate(-(pos.X + width / 2), -(pos.Y + height / 2), 0.0);
            }
            else
            {
                GL.Translate(pos.X + rotationOrgin.X, pos.Y + rotationOrgin.Y, 0.0);
                GL.Rotate(-rotation, 0.0f, 0.0f, 1.0f);
                GL.Translate(-(pos.X + rotationOrgin.X), -(pos.Y + +rotationOrgin.Y), 0.0);
            }
        }
        private void _generateFramesTexcoords()
        {
            // ustawiamy tablice koordynantów mapowania tekstury dla kolejnych klatek animacji:
            _framesTexXCoords = new float[_framesCount + 1];
            _framesTexXCoords[0] = 0;// (1 / (float)_frameTexWidth);
            for (int i = 1; i < _framesCount; i++)
                _framesTexXCoords[i] = (float)1.0 / _framesCount * i;
            _framesTexXCoords[_framesCount] = 1f;// (1 / (float)_frameTexWidth);
        }

        private int lastFrameMSEC;
        private void _updateCurrentFrame()
        {
            if (_tex == null) return;
            // Frame switching need to be synchronized between sprites so we use of miliseconds in system timer
            int msec = System.DateTime.Now.Millisecond;//%1000;
            if (msec - lastFrameMSEC > 1000 / fps)//_framesCount * (_currentFrame + 1))
            { _currentFrame = _nxtFrame; lastFrameMSEC = msec; }
            else if (_lastMsec > msec) //bugfix: looping back
            { _currentFrame = _nxtFrame; lastFrameMSEC = msec; }
            _lastMsec = msec;
        }
        #endregion
    }
}
