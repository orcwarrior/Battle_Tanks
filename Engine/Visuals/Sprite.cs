using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Battle_Tanks.Visuals
{
	/// <summary>
	/// Klasa odpowiadajaca za wizualne przedstawienie obiekt�w mapy 
	/// </summary>
	public class Sprite
	{
        static private int fpsDefault = 3;
        #region fields
        /// <summary>TRUE - obiekt zostanie zniszczony po zako�czeniu renderowania ostatniej klatki(JESZCZE NIEZAIMPLEMENTOWANO)!.</summary>
        public bool destroyWhenAniEnd;
        /// <summary>Wsp�rz�dne wg. kt�rych b�dzie wykonywany obr�t Sprite'a (�rodek obrotu).</summary>
        public Point rotationOrgin;
        /// <summary>Kolor kt�rym wype�nianie jest t�o sprite'a, lub je�li Sprite nie posiada <see cref="_tex"/> b�dzie to kolor narysowanego prostok�ta.</summary>
        public OpenTK.Graphics.Color4 color { get; set; }
        /// <summary>(Frames per second) je�eli sprite jest animowany okre�la to jego il. klatek na sekunde.</summary>
        public int fps { get; set; }
        /// <summary>Pozycja w kt�rej zacznie si� rysowanie Sprite'a na ekranie.</summary>
        public Point pos { get; private set; }
        /// <summary>Wysoko�� renderowanego obiektu sprite (w pikselach)</summary>
		public int height{get; set;} // no longer private set
        /// <summary>Szeroko�� renderowanego obiektu sprite (w pikselach)</summary>
        public int width { get; set; }
        /// <summary>Obr�t (w stopniach) kt�ry zostanie wykonany dla Sprite'a przed wyrenderowaniem.</summary>
        public double rotation { get; set; }
        
        //Te pola mozna uzyskac z _tex'a
        //private int _orgHeight, _orgWidth;
		private Texture _tex;
        private int _currentFrame;
        private float[] _framesTexXCoords;
        private int _lastMsec;
        #endregion

        #region properties
        /// <summary> Ustawia skal� lub odczytuje ja na podstawie wysoko�ci obiektu Sprite przez wysoko�� bazowa tekstury</summary>
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
        /// generuje odpowiedni obiekt klasy Texture. Wykonuje tak�e
        /// szereg operacji maj�cych na celu zainicjonowanie animacji sprite (o ile wyst�puje)
        /// </summary>
        /// <param name="texName">Plik wykorzystywanej tekstury</param>
        /// <param name="p">Punkt na ekranie w kt�rym renderowany b�dzie obiekt</param>
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
        /// Renderuje sprite na ekranie, wykonuje te� wszelkie transformacje jak zmiana obecnej klatki
        /// czy odpowiedni obr�t tekstury.
        /// </summary>
        public void Render()
		{   /* 0---1 How
             * |  /| Quad
             * | / | is
             * 3/--2 Made */
            _updateCurrentFrame();
			_spritePreparations();
			
			// Okreslamy ze bedziemy rysowa� prostok�ty (Quads)
			GL.Begin(BeginMode.Quads);
			
			// Rysujemy wierzcho�ki prostok�ta wraz z odpowiednymi koordynantami tekstury
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
			// Konczymy rysowanie prostok�ta:
            GL.End();
            _spriteEnd();

		}
        /// <summary>
        /// Ustawia pozycje Sprite'a absolutnie
        /// </summary>
        /// <param name="position">pozycja na ktor� ma zosta� przeniesiony sprite</param>
		public void setPos(Point position)
		{
			pos = position;
		}
        /// <summary>
        /// Przesuwa obiekt o podan� pozycj�
        /// (Ustawia pozycje relatywnie)
        /// </summary>
        /// <param name="mov">Pozycja wg. kt�rej obiekt ma zosta� przesuni�ty.</param>
		public void move(Point mov)
		{
            pos = new Point(pos.X + mov.X, pos.Y + mov.Y);
		}
        /// <summary>
        /// Zmienia warto�� obrotu obiektu
        /// </summary>
        /// <param name="deg">obr�t (w stopniach)</param>
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
            // ustawiamy tablice koordynant�w mapowania tekstury dla kolejnych klatek animacji:
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
