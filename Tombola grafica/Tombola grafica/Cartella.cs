using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tombola_grafica
{
    internal class Cartella
    {
        public Button[] buttons_cartella;
        private Random random;
        private int x = 650, y = 230;
        private string[] num_cartella;
        
        public Cartella()
        {
            buttons_cartella = new Button[15];
            random = new Random();  
            num_cartella = new string[15];
            nuova();
        }
        public void nuova()
        {
            
            for (int i = 0; i < buttons_cartella.Length; i++)
            {
                num_cartella[i] = (random.Next(1, 91)).ToString(); //estrarre ogni numero
                buttons_cartella[i] = new Button
                {
                    Text = num_cartella[i],
                    Size = new System.Drawing.Size(50,50),
                    Location = new System.Drawing.Point(x,y),
                };
                
                x += 50; //DISTANZIARE OGNI PULSANTE
                if(i == 4 || i == 9) //DIVIDERE LE RIGHE ORIZZONTALI  
                {
                    x = 650;
                    y += 50;
                }
            }
        }
    }
}
