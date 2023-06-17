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
using Trololo.Properties;

namespace Trololo.View
{
    public partial class LooseControl : UserControl
    {
        public LooseControl()
        {
            InitializeComponent();
            this.Size = new Size(1375, 980); 
        }

        private Game game; 
        public void Run(Game Game)
        {
            var s = Image.FromFile("View//Images//DeathScreen.png");
            game = Game; 
            this.BackgroundImage = s;
            var a = new Label();
            a.Text = "Попробовать снова";
            a.Location = new Point(690, 630);

            a.Size = new Size(125, 20); 
            var b = new Label();
            b.Text = "В меню";
            b.Location = new Point(690, 750);
            a.Parent = this; 
            a.BackColor = Color.Transparent; 
            b.BackColor = Color.Transparent;

            this.Controls.Add(a);
            this.Controls.Add(b);
            a.MouseClick += A_MouseClick;
            b.MouseClick += B_MouseClick;
        }

        private void B_MouseClick(object sender, MouseEventArgs e)
        {
            game.ShowMenu(); 
        }

        private void A_MouseClick(object sender, MouseEventArgs e)
        {
            game.RestartLevel();
        }
    }
}
