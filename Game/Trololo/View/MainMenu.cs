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
                this.BackgroundImage = new Bitmap(Image.FromFile($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\photo_2023-04-12_17-12-07.jpg"));
                this.Name = "Trololo";
                this.Size = BackgroundImage.Size;
                var beginGame = AddLables("Начать игру", new Point(200, 400));
                var loadGame = AddLables("Загрузить игру", new Point(200, 500));
                var exitGame = AddLables("Выйти из игры", new Point(200, 600));
                Controls.Add(beginGame);
                Controls.Add(loadGame);
                Controls.Add(exitGame);

            loadGame.Click += (o, e) =>
            {
                Controls.Add(new Load());
                Controls.Remove(this);

            };

            beginGame.Click += (o, e) =>
            {
                Controls.Add(new Game());
                Controls.Remove(this); 
                Controls.Remove(beginGame);
                Controls.Remove(loadGame);
                Controls.Remove(exitGame); 
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
