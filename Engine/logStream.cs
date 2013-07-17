using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battle_Tanks
{
    /// <summary>
    /// Klasa dziedzicąca po <see cref="System.IO.StreamWriter"/> dodajaca pare udogodnień
    /// do metody <see cref="WriteLine(string)"/> mających na celu udogonić odczyt wiadomości zapisanych w logu.
    /// </summary>
    public class logStream : System.IO.StreamWriter
    {
        string lastLine = "";
        /// <summary>
        /// Konstruktor wywołuje jedynie bazowy konstruktor.
        /// </summary>
        /// <param name="path"></param>
        public logStream(string path)
            : base(path)
        {
        }
        /// <summary>
        /// To jedyna metoda do zapisu lini która została nadpisana, proszę używać tylko jej!
        /// Metoda dodając datę na poczatku podanej wiadomości, dodatkowo nie pozwala na wpisanie.
        /// dwóch takich samych wiadomości do loga po sobie.
        /// </summary>
        /// <param name="value">Tekst do wpisania do pliku z logiem</param>
        public override void WriteLine(string value)
        {
            if (value.Equals(lastLine) == false)
            {
                base.WriteLine("["+DateTime.Now.ToLongTimeString()+"]" + value);
                lastLine = value;
            }

        }
    }
}
