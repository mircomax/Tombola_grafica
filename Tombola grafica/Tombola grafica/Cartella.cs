using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Tombola_grafica
{
    internal class Cartella
    {
        public Button[] buttons_cartella;
        private Random random;
        private int x = 550, y = 230;
        private int[] num_cartella;
        private bool[] num_generati;
        public Cartella(Random rand)
        {
            buttons_cartella = new Button[15];
            random = rand;   
            num_cartella = new int[15];
            num_generati = new bool[90];
            nuova();
        }

        public int GetX()
        {
            return x;
        }
        public int GetY() { 
            return y;
        }
        public void nuova()
        {
            int numero;
            for (int i = 0; i < buttons_cartella.Length; i++)
            {
                do
                {
                    numero = random.Next(1, 91);
                } while (num_generati[numero - 1]);

                num_generati[numero - 1] = true; // Segna il numero come generato
                
                buttons_cartella[i] = new Button
                {
                    Text = numero.ToString(),
                    Size = new System.Drawing.Size(50,50),
                    Location = new System.Drawing.Point(x,y),
                };
                
                x += 50; //DISTANZIARE OGNI PULSANTE
                if(i == 4 || i == 9) //DIVIDERE LE RIGHE ORIZZONTALI  
                {
                    x = 550;
                    y += 50;
                }
            }
        }

        public void Controllo(int estratto)
        {
            for(int i = 0;i < buttons_cartella.Length; i++)
            {
                if (int.Parse(buttons_cartella[i].Text) == estratto)
                {
                    buttons_cartella[i].BackColor = System.Drawing.Color.LightYellow;
                }
            }
        }
    }
}
