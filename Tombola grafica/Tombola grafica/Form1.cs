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
        private bool[] Tombolagiafatta = { false, false, false };
        public int numeroestratto = 0, numerocartellevero;
        private Button Estrarre, confirmButton;
        Tabellone primotabellone;
        //Cartella primacartella;
        private Cartella[] cartelle;
        private Label numeriestratti;
        private Label CartellaTitolo;
        private Random random;
        private Label sfondotabellone,Ambo;
        private Label[] sfondocartelle;
        private Panel pannelloiniziale;
        private NumericUpDown cartelleNumericUpDown;
        public Form1()
        {
            numerocartellevero = new int();
            random = new Random();
            primotabellone = new Tabellone(random);  
            
            this.Text = "TOMBOLA";
            this.Size = new Size(900, 800);
            initializePanel();
            // Inizializzare l'array di Buttons
            buttons = new Button[90];
            int y = 25;
            int x = 0;
            
            
            //creare label titolo
            titolo = new Label
            {
                Font = new Font("Arial", 15, FontStyle.Italic),
                Text = "TABELLONE",
                BackColor = Color.LightSteelBlue,
                Location = new System.Drawing.Point(10, 10),
                Size = new Size(200, 20)
            };
            this.Controls.Add(titolo);

            numeriestratti = new Label()
            {
                Font = new Font("Arial", 11, FontStyle.Regular),
                Text = $"NUMERI ESTRATTI FINO AD ADESSO: \n",
                Location = new System.Drawing.Point(10, 520),
                Size = new Size(510, 100),
                BackColor = Color.Aquamarine,
            };
            this.Controls.Add(numeriestratti);

            //creare label per contare e visualizzare numeri estrazione
            Estrazione = new Label
            {
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular),
                Text = $"NUMERO ESTRATTO: {numeroestratto}",
                Location = new System.Drawing.Point(550, 110),
                Size = new Size(200, 50)
                
            };
            this.Controls.Add(Estrazione);

            //label tipo "Chat" dove viene mostrato quando viene fatto ambo,terna o quaterna...
            Ambo = new Label
            {
                Font = new Font("Arial", 12, FontStyle.Regular),
                Text = "Partita iniziata\n",
                BackColor = Color.AliceBlue,
                Location = new System.Drawing.Point(10, 620),
                Size = new Size(510, 100)

            };
            this.Controls.Add(Ambo);

            // Creare e configurare ogni Button
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button();
                buttons[i].Size = new System.Drawing.Size(50, 50);
                if (i % 10 == 0) { //andare a capo ogni 10 bottoni
                    y += 50;
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
                x += 50;

            }

            //creare button per estrarre numeri
            Estrarre = new Button
            {
                Text = "Estrazione",
                Location = new System.Drawing.Point(550, 40),
                Size = new Size(180, 60),
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
                Location = new System.Drawing.Point(550, 165),
                Size = new Size(200, 20)
            };
            this.Controls.Add(CartellaTitolo);

            // Migliorare estetica:
            sfondotabellone = new Label
            {
                BackColor = Color.LightSteelBlue,
                Location = new System.Drawing.Point(5, 10),
                Size = new Size(530, 505)
            };
            this.Controls.Add(sfondotabellone);

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
                Estrazione.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                numeroestratto = primotabellone.estrai();
                Estrazione.Text = $"NUMERO ESTRATTO: {numeroestratto}\n TOTALE: {primotabellone.getConta()}";
                
                for (int i = 0; i < 90; i++) //controllo se numero estratto è presente nel tabellone
                { 
                    if (int.Parse(buttons[i].Text) == numeroestratto)
                    {
                        buttons[i].BackColor = Color.LightYellow;
                        buttons[i].Enabled = false;
                    }
                }
                
                for (int i = 0; i < numerocartellevero; i++)
                {
                    var risultato = cartelle[i].AggiornaCartella(numeroestratto);
                    if (cartelle[i].ControlloTombola() && !Tombolagiafatta[i])
                    {
                        Tombolagiafatta[i] = true;
                        MessageBox.Show($"Hai fatto tombola nella cartella {i + 1}!", "TOMBOLA!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    if (risultato != null)
                    {
                        Ambo.Font = new Font("Arial", 10, FontStyle.Bold);
                        Ambo.Text += $"Hai fatto {risultato} nella cartella {i+1}!\n";
                        giafatto(risultato);
                    }
                }

                numeriestratti.Text += numeroestratto + ", "; //aggiunta numero nella label numeri estrazione
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
            
        private void initializePanel()
        {
            pannelloiniziale = new Panel()
            {
                Size = new Size(300, 150),
                Location = new Point((this.ClientSize.Width - 300) / 2, (this.ClientSize.Height - 150) / 2),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label setupLabel = new Label
            {
                Text = "Seleziona il numero di cartelle che desideri:",
                Location = new Point(10, 10),
                Size = new Size(280, 20)
            };

            cartelleNumericUpDown = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 3, // Imposta il massimo numero di cartelle desiderato
                Value = 1,
                Location = new Point(10, 40),
                Size = new Size(280, 20)
            };

            confirmButton = new Button
            {
                Text = "Conferma",
                Location = new Point(10, 70),
                Size = new Size(280, 30)
            };
            confirmButton.Click += confirmButton_Click;

            // Aggiungi i controlli al pannello di setup
            pannelloiniziale.Controls.Add(setupLabel);
            pannelloiniziale.Controls.Add(cartelleNumericUpDown);
            pannelloiniziale.Controls.Add(confirmButton);

            // Aggiungi il pannello di setup al form principale
            this.Controls.Add(pannelloiniziale);
        }
        private void CreaCartelle(int numerocartelle)
        {
            cartelle = new Cartella[numerocartelle];
            for(int i = 0; i < numerocartelle; i++)
            {
                cartelle[i] = new Cartella(random);
                cartelle[i].creaCartella(i * 185);
                foreach (var button in cartelle[i].buttons_cartella)
                {
                    this.Controls.Add(button);
                }
            }
            
        }
        private void creasfondocartelle(int numerocartelle)
        {
            sfondocartelle = new Label[numerocartelle];
            for(int i = 0; i < numerocartelle; i++)
            {
                sfondocartelle[i] = new Label()
                {
                    BackColor = Color.Black,
                    Font = new Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    Text = $"Cartella N.{i+1}:",
                    Location = new System.Drawing.Point(540, 190 + (i * 185)),
                    Size = new Size(270, 180)
                };
                this.Controls.Add(sfondocartelle[i]);
            }
        }
        private void confirmButton_Click(object sender, EventArgs e)
        {
            int numerocartelle = (int)cartelleNumericUpDown.Value;
            numerocartellevero = numerocartelle;
            pannelloiniziale.Hide();
            CreaCartelle(numerocartelle);
            creasfondocartelle(numerocartelle);
        }

        private void giafatto(string risultato)
        {
            for (int i = 0; i < numerocartellevero; i++)
            {
                switch (risultato)
                {
                    case "Ambo":
                        cartelle[i].setAmbofatto();
                        break;

                    case "Terna":
                        cartelle[i].setTernafatto();
                        break;

                    case "Quaterna":
                        cartelle[i].setQuaternafatta();
                        break;

                    case "Cinquina":
                        cartelle[i].setCinquinafatta();
                        break;
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
