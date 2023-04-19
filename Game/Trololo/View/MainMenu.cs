using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.PropertyGridInternal;
using System.Windows.Forms.VisualStyles;

namespace Trololo.View
{
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
                this.BackgroundImage = new Bitmap(Image.FromFile($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\menu.png"));
                this.Name = "Trololo";
                this.Size = BackgroundImage.Size;
                var beginGame = AddLables("Начать игру", new Point(200, 400));
                var loadGame = AddLables("Загрузить игру", new Point(200, 500));
                var exitGame = AddLables("Выйти из игры", new Point(200, 600));
                Controls.Add(beginGame);
                Controls.Add(loadGame);
                Controls.Add(exitGame);


            beginGame.Click += (o, e) =>
            {
                Controls.Add(new GameControl());
                this.Hide();
            };

            exitGame.Click += (o, e) =>
            {
                Application.Exit();
            };
        }


        private static Label AddLables(string text, Point location)
        {
            var label = new Label();
            label.Text = text;
            label.Location = location;
            label.Width = 100;
            return label;
        }
    }
}
