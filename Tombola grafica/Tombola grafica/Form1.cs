using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tombola_grafica
{
    public partial class Form1 : Form
    {
        private Button[] buttons;
        private Label titolo;
        private Label Estrazione;
        public int numeroestratto = 0;
        private Button Estrarre;
        Tabellone primotabellone;
        Cartella primacartella;
        private Label numeriestratti;
        private Label CartellaTitolo;
        
        public Form1()
        {
            
            primotabellone = new Tabellone();  
            primacartella = new Cartella();
            this.Text = "TOMBOLA";
            this.Size = new Size(900, 800);
            // Inizializzare l'array di Buttons
            buttons = new Button[90];
            int y = 10;
            int x = 0;

            foreach (var button in primacartella.buttons_cartella)// Aggiungere i bottoni della cartella al form
            {
                this.Controls.Add(button);
            }
            
            //creare label titolo
            titolo = new Label
            {
                Font = new Font("Arial", 15, FontStyle.Italic),
                Text = "TABELLONE",
                Location = new System.Drawing.Point(10, 10),
                Size = new Size(200, 20)
            };
            this.Controls.Add(titolo);

            numeriestratti = new Label()
            {
                Font = new Font("Arial", 8, FontStyle.Regular),
                Text = $"NUMERI ESTRATTI FINO AD ADESSO: \n",
                Location = new System.Drawing.Point(10, 680),
                Size = new Size(600, 75),
                BackColor = Color.Aquamarine,
            };
            this.Controls.Add(numeriestratti);

            //creare label per contare e visualizzare numeri estrazione
            Estrazione = new Label
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Text = $"NUMERO ESTRATTO: {numeroestratto}",
                Location = new System.Drawing.Point(650, 110),
                Size = new Size(170, 35)
                
            };
            this.Controls.Add(Estrazione);
            // Creare e configurare ogni Button
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new System.Drawing.Size(50, 50);
                if (i % 10 == 0) { //andare a capo ogni 10 bottoni
                    y += 70;
                    x = 10;
                }

                if (i % 5 == 0 && i % 10 == 5) //aggiungere spazio nella colonna centrale
                {
                    x += 15;
                }

                if(i == 30 || i == 60) //aggiungere spazio nelle righe orizzontali per dividere le cartelle
                {
                    y += 10;
                }
                
                buttons[i].Location = new System.Drawing.Point(x, y - 40);
                buttons[i].Text = "" + (i + 1);
                buttons[i].Click += new EventHandler(Button_Click);

                // Aggiungere il Button al Form
                this.Controls.Add(buttons[i]);
                x += 60;
            }

            //creare button per estrarre numeri
            Estrarre = new Button
            {
                Text = "Estrazione",
                Location = new System.Drawing.Point(650, 50),
                Size = new Size(170, 50),
                BackColor = Color.Red,
                Font = new Font("Arial", 10, FontStyle.Bold),
                
            };
            Estrarre.Click += new EventHandler(Estrarre_Click);
            this.Controls.Add (Estrarre);

            //Creare label titolo cartella
            CartellaTitolo = new Label
            {
                Font = new Font("Arial", 13, FontStyle.Italic),
                Text = "LE TUE CARTELLE:",
                Location = new System.Drawing.Point(650, 160),
                Size = new Size(200, 20)
            };
            this.Controls.Add(CartellaTitolo);
        }
        
        private void Button_Click(object sender, EventArgs e)
        {
        }
        private async void Estrarre_Click(object sender, EventArgs e)
        {
            if (primotabellone.getConta() < 89) //controllare se sono finiti i numeri da estrarre
            {
                Estrazione.Font = new Font("Arial", 12, FontStyle.Bold);
                Estrazione.Text = "ESTRAZIONE.....";
                await Task.Delay(1500);
                Estrazione.Font = new Font("Arial", 10, FontStyle.Regular);
                numeroestratto = primotabellone.estrai();
                Estrazione.Text = $"NUMERO ESTRATTO: {numeroestratto}\n TOTALE: {primotabellone.getConta()}";
                for (int i = 0; i < 90; i++)
                { 
                    if (int.Parse(buttons[i].Text) == numeroestratto)
                    {
                    buttons[i].BackColor = Color.LightYellow;
                    }
                }
                numeriestratti.Text += numeroestratto + ", ";
            }
            else
            {
                if(MessageBox.Show("Sono stati estratti tutti i numeri!!", "Fine estrazione", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    == DialogResult.OK)
                {
                    this.Close();
                }
            }

        }
            

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
