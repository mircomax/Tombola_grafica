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
        public Form1()
        {
            primotabellone = new Tabellone();   
            this.Text = "TOMBOLA";
            this.Size = new Size(800, 800);
            // Inizializzare l'array di Buttons
            buttons = new Button[90];
            int y = 10;
            int x = 0;
            //creare label titolo
            titolo = new Label
            {
                Font = new Font("Arial", 15, FontStyle.Italic),
                Text = "TABELLONE",
                Location = new System.Drawing.Point(10, 40),
                Size = new Size(200, 40)
            };
            this.Controls.Add(titolo);

            //creare label titolo
            Estrazione = new Label
            {
                Font = new Font("Arial", 10, FontStyle.Regular),
                Text = $"NUMERO ESTRATTO: {numeroestratto}",
                Location = new System.Drawing.Point(650, 110),
                Size = new Size(110, 50)
            };
            this.Controls.Add(Estrazione);

            // Creare e configurare ogni Button
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new System.Drawing.Size(50, 50);
                if (i % 10 == 0) {
                    y += 70;
                    x = 10;
                }
                
                buttons[i].Location = new System.Drawing.Point(x, y);
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
                Size = new Size(100, 50),
                BackColor = Color.Red,
                Font = new Font("Arial", 10, FontStyle.Bold),
                
            };
            Estrarre.Click += new EventHandler(Estrarre_Click);
            this.Controls.Add (Estrarre);
        }

        private void Button_Click(object sender, EventArgs e)
        {
        }
        private void Estrarre_Click(object sender, EventArgs e)
        {
            numeroestratto = primotabellone.estrai();
            Estrazione.Text = $"NUMERO ESTRATTO: {numeroestratto}";
            for (int i = 0; i < 90; i++)
            { 
                if (int.Parse(buttons[i].Text) == numeroestratto)
                {
                    buttons[i].BackColor = Color.LightYellow;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
