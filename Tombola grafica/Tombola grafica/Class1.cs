using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tombola_grafica
{
        internal class Linea
        {
            private Button[] valori;

            public Linea()
            {
                valori = new Button[5];
            }

            public void setValori(Button bottone, int i)
            {
                this.valori[i] = bottone;
            }

            public int contaNumeriEstratti()
            {
                int count = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (valori[i].BackColor == Color.LightYellow)
                    {
                        count++;
                    }
                }
                return count;
            }

        }

}

