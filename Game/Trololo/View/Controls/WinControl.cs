
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Trololo.Domain;
using Trololo.Properties;

namespace Trololo.View.Controls
{
    public partial class WinControl : UserControl
    {
        private Game game;
        public WinControl()
        {
            InitializeComponent();
            this.Size = new Size(1375, 980);
        }

        public void Run(Game game_)
        {
            this.BackgroundImage = Resources.WinBack; 
            var b = new PictureBox();
            game = game_;
            b.Location = new System.Drawing.Point(100, 630);
            b.Image= Resources.ExitButton;
            b.Size = b.Image.Size;
            b.BackColor = Color.Transparent;
            b.Click += ExitGame;
            var a = new PictureBox();

            a.Image = Resources.WinBackPict;
            a.Size = a.Image.Size; 
            a.BackColor= Color.Transparent;
            a.Location = new Point(270, 50);

            Controls.Add(a);
            Controls.Add(b);

        }

        private void ExitGame(object s, EventArgs e) 
        {
            game.Exit(); 
        }
    }
}
