using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        Linea linea1, linea2, linea3;
        private int ambofatto = 0, ternafatta = 0, quaternafatta = 0, cinquinafatta = 0;
        public Cartella(Random rand)
        {
            buttons_cartella = new Button[15];
            random = rand;
            ambofatto = new int();
            ternafatta = new int();
            quaternafatta = new int();
            cinquinafatta = new int();
            num_cartella = new int[15];
            num_generati = new bool[90];
            linea1 = new Linea();
            linea2 = new Linea();
            linea3 = new Linea();
            nuova();
            creaCartella();
            setLinee();
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
                num_cartella[i] = numero;
            }
            
        }
        private void bubbleSort(int DimensioneIniziale, int DimensioneFinale)
        {
            for (int i = 0; i < 5 - 1; i++)
            {
                for (int j = DimensioneIniziale; j < DimensioneFinale - i - 1; j++)
                {
                    if (num_cartella[j] > num_cartella[j + 1])
                    {
                        int temp = num_cartella[j];
                        num_cartella[j] = num_cartella[j + 1];
                        num_cartella[j + 1] = temp;
                    }
                }
            }
        }
        public void creaCartella()
        {
            bubbleSort(0,5);
            bubbleSort(5, 10);
            bubbleSort(10, 15);
            for (int i = 0; i < 15; i++)
            {
                buttons_cartella[i] = new Button
                {
                    Text = num_cartella[i].ToString(),
                    Size = new System.Drawing.Size(50, 50),
                    Location = new System.Drawing.Point(x, y),
                };
                x += 50; //DISTANZIARE OGNI PULSANTE
                if (i == 4 || i == 9) //DIVIDERE LE RIGHE ORIZZONTALI  
                {
                    x = 550;
                    y += 50;
                }
            }
            
        }
        public void setLinee()
        {
            for (int i = 0; i < 15; i++)
            {
                if (i < 5)
                {
                    linea1.setValori(buttons_cartella[i], i);
                }
                else if (i < 10)
                {
                    linea2.setValori(buttons_cartella[i], i % 5);
                }
                else
                {
                    linea3.setValori(buttons_cartella[i], i % 5);
                }
            }
        }

        public string ControlloAmboTerna()
        {
            int c1 = 0, c2 = 0, c3 = 0;
            for (int i = 0; i < 5; i++)
            {
                if (linea1.isColoreMatch(Color.LightYellow, i)) { c1++; }
                if (linea2.isColoreMatch(Color.LightYellow, i)) { c2++; }
                if (linea3.isColoreMatch(Color.LightYellow, i)) { c3++; }
            }

            if ((c1 == 2 || c2 == 2 || c3 == 2) && ambofatto == 0)
            {
                PlayVictorySound();
                FlashButtons(buttons_cartella.Where(btn => btn.BackColor == Color.LightYellow).ToArray());
                ambofatto++;
                return "Hai fatto ambo!";
            }
            if ((c1 == 3 || c2 == 3 || c3 == 3) && ternafatta == 0)
            {
                PlayVictorySound();
                FlashButtons(buttons_cartella.Where(btn => btn.BackColor == Color.LightYellow).ToArray());
                ternafatta++;
                return "Hai fatto terna!";
            }
            if ((c1 == 4 || c2 == 4 || c3 == 4) && quaternafatta == 0)
            {
                PlayVictorySound();
                FlashButtons(buttons_cartella.Where(btn => btn.BackColor == Color.LightYellow).ToArray());
                quaternafatta++;
                return "Hai fatto quaterna!";
            }
            if ((c1 == 5 || c2 == 5 || c3 == 5) && cinquinafatta == 0)
            {
                PlayVictorySound();
                FlashButtons(buttons_cartella.Where(btn => btn.BackColor == Color.LightYellow).ToArray());
                cinquinafatta++;
                return "Hai fatto cinquina!";
            }

            return null;
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

        private async void FlashButtons(Button[] buttons)
        {
            for (int i = 0; i < 3; i++)
            {
                foreach (var button in buttons)
                {
                    button.BackColor = Color.Red;  // Colore lampeggiante
                }
                await Task.Delay(300);
                foreach (var button in buttons)
                {
                    button.BackColor = Color.LightYellow;  // Colore originale
                }
                await Task.Delay(300);
            }
        }

        private void PlayVictorySound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\mirco\Desktop\esercizi classi\Tombola grafica\Tombola grafica\Dababy Let's Go Sound Effect.wav");
            player.Play();
        }
    }


}
