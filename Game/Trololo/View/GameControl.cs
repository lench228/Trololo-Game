using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trololo.Domain; 

namespace Trololo.View
{
    public partial class GameControl : UserControl
    {
        public GameControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(Game.BackImage, new Point(0, 0));
            DrawLvl(Game.level,  e);
        }

        private static void DrawLvl(Level level, PaintEventArgs e)
        {
            foreach (var a in level.tiles)
                if(a.texture != null)
                    e.Graphics.DrawImage(a.texture, a.transform.position);
            
        }
    }
}
