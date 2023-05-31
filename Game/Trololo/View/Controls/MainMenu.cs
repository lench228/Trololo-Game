using System.Drawing; 
using System.Windows.Forms;
using Trololo.Domain; 

namespace Trololo.View
{
    public partial class MainMenu : UserControl
    {
        private  Game game;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Start_MouseClick(object sender, MouseEventArgs e)
        {
            game.Start(); 
        }

        public void Run(Game game)
        {
            this.game = game;
            var start = new Label
            {
                Text = "НАЧАТЬ",
                Location = new Point(200, 500)
            };
            Controls.Add(start);

            start.MouseClick += Start_MouseClick;
        }
    }
}
