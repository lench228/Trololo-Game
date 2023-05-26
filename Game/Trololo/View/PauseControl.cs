using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trololo.Domain; 

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
            this.BackgroundImage = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\PauseBack.png");

            var aPict = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\ContinueButton.png");
            var bPict = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\ToMenuButton.png"); 
            var a = new PictureBox();
            a.Image = aPict; 
            var b = new PictureBox();
            b.Image = bPict;

            b.Size = bPict.Size;
            a.Size = aPict.Size;
            a.BackColor = Color.Transparent
                ;
            b.BackColor = Color.Transparent;
            a.Location = new Point(50, 100);
            b.Location = new Point(50, 500);

            b.MouseClick += A_MouseClick;
            a.MouseClick += B_MouseClick;

            this.Controls.Add(a);
            this.Controls.Add(b);
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
