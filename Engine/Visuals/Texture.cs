using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;

namespace Battle_Tanks.Visuals
{
	/// <summary>
	/// Obiekty klasy to pojedy�cze tekstury inicjalizowane przez wczytanie bitmapy
	/// z podanej przy konstruktorze nazwy tekstury. Podczas konstrukcji wype�niane
	/// s� dane dot. wymiar�w tekstury.
	/// </summary>
	public class Texture
	{
		/// <summary>�cie�ka dostepu do folderu z teksturami. </summary>
		static string texturesPath = "../textures/";
		/// <summary>Identyfikator tekstury przypisany przez openGL przy generowaniu. </summary>
		public int glTexID {get; private set;}

		/// <summary>Szeroko�� tekstury </summary>
		public int width { get; private set; }
		/// <summary>Wysoko�� tekstury </summary>
		public int height { get; private set; }

		/// <summary>
		/// Konstruktor klasy texture przeprowadza ca�y proces generowania
		/// tekstury openGL.
		/// </summary>
		/// <param name="texName">Nazwa pliku z kt�rego ma zosta� utworzona tekstura.</param>
		public Texture(string texName)
		{	// Wczytanie bitmapy za pomoc� obiektu Bitmap z System.Drawing:
            string path = texturesPath + texName;
            Bitmap bitmap = new Bitmap(path);
			width = bitmap.Width;
			height = bitmap.Height;

			// Utworzenie obiektu bitmapData z kt�rego p�niej bitmapa b�dzie obs�u�ona przez openGL
			BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
				ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			// Generujemy nowa teksture w openGL oraz bindujemy j�
			// (Kolejne operacje z GL.Tex* b�da wykonywane dla niej)
			glTexID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, glTexID);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			// Po odczycie z bitmapy mo�na j� odblokowa�.
			bitmap.UnlockBits(data);

			// Filtrowanie tekstury, inaczej przy jakimkolwiek skalowaniu (mniejsze wieksze)
			// bitmapy wyswietli nam sie tylko biel
			//GL.TexEnv(TextureEnvTarget.TextureEnv, TextureEnvParameter.TextureEnvMode, (float)TextureEnvMode.Modulate);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
		}
	}
}
