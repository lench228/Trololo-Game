using System.Drawing; 
using System.Windows.Forms;
using Trololo.Domain;
using Trololo.Properties;

namespace Trololo.View
{
    public partial class MainMenu : UserControl
    {
        private  Game game;
        public MainMenu()
        {
            InitializeComponent();
        }

        public void Run(Game game)
        {
            this.game = game;
            var start = new PictureBox()
            {
                Image = Image.FromFile("View//Images//Begin.png"),
                Size = Image.FromFile("View//Images//Begin.png").Size,
                BackColor = Color.Transparent,
                Location = new Point(200, 500)
            };
            var exit = new PictureBox()
            {
                Image = Image.FromFile("View//Images//Exit.png"),
                Size = Image.FromFile("View//Images//Exit.png").Size,

                BackColor = Color.Transparent,
                Location = new Point(200, 700)
            };
            Controls.Add(start);
            Controls.Add(exit);
            start.MouseClick += Start_MouseClick;
            exit.MouseClick += Exit_MouseClick;
        }

        private void Exit_MouseClick(object sender, MouseEventArgs e)
        {
            game.Exit();
        }

        private void Start_MouseClick(object sender, MouseEventArgs e)
        {
            game.Start();
        }

    }
}
