using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Tombola_grafica
{
    internal class Tabellone
    {
        private int[] num_estratti;
        private bool[] estratti;
        private int conta;
        private Random random;
        

        public Tabellone(Random rand)
        {
            num_estratti = new int[90];
            estratti = new bool[90];
            random = rand; 
            conta = 0;
            
        }

        public int estrai()
        {
            int estratto;
            do
            {
                estratto = random.Next(1, 91); // Genera un numero casuale tra 1 e 90
            } while (estratti[estratto - 1]); // Continua a estrarre finché il numero è già stato estratto

            estratti[estratto - 1] = true; // Segna il numero come estratto
            num_estratti[conta] = estratto; // Memorizza il numero estratto nell'array
            conta++; // Incrementa il contatore

            return estratto; // Restituisce il numero estratto
        }
        public int getConta()
        {
            return conta;
        }
    }
}
