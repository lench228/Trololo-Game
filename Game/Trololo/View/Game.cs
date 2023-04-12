using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trololo.View
{
    public partial class Game : UserControl
    {
        public Game()
        {
            this.BackgroundImage = new Bitmap("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\gameBack.jpg");
            this.Size = new Size(1280, 910);
            var a = new Graphics();
            a.DrawImage();
        }
    }
}
