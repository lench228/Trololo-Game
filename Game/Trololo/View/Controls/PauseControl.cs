using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trololo.Domain;
using Trololo.Properties;

namespace Trololo.View
{
    public partial class PauseControl : UserControl
    {
        private Game game; 
        public PauseControl()
        {
            InitializeComponent();
        }

        public void Run (Game Game)
        {
            game = Game; 
            this.Size = new Size(1380, 980);
            this.BackgroundImage = Image.FromFile("View//Images//PauseBack.png");

            var aPict = Image.FromFile("View//Images//ContinueButton.png");
            var bPict = Image.FromFile("View//Images//ToMenuButton.png");
            var cPict = Image.FromFile("View//Images//LevelRestart.png"); ;   
            var a = new PictureBox();
            var b = new PictureBox();
            var c = new PictureBox(); 


            SetButton(new Point(50, 100), aPict, a);
            SetButton(new Point(50, 600), bPict, b);
            SetButton(new Point(60, 380), cPict, c);
            b.MouseClick += A_MouseClick;
            a.MouseClick += B_MouseClick;
            c.MouseClick += C_MouseClick;

            this.Controls.Add(a);
            this.Controls.Add(b);
            this.Controls.Add(c);
        }

        private void SetButton(Point position, Image image, PictureBox button)
        {
            button.Image = image;
            button.BackColor= Color.Transparent;
            button.Size = image.Size;
            button.Location = position;
        }

        private void C_MouseClick(object sender, MouseEventArgs e)
        {
            game.RestartLevel(); 
        }

        private void B_MouseClick(object sender, MouseEventArgs e)
        {
            game.ContinueGame();
        }

        private void A_MouseClick(object sender, MouseEventArgs e)
        {
            game.ShowMenu(); 
        }
    }
}
