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
        
        private static Graphics g;
        private Game game;
        private static Timer timer; 
        public GameControl()
        {
            InitializeComponent();
        }

        public void Run(Game game)
        {
            DoubleBuffered = true; 
            this.game = game;
            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += new EventHandler(Update);
            timer.Start();   
        }

        private void Update(object sender, EventArgs e)
        {
            if(game.Collide(game.level.tiles, Game.player, new PointF(0, Player.gravity)))
                Game.player.Move(new PointF(0, Player.gravity));
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            DrawLvl(game.level, e);
            DrawChar(Player.transform, e);
        }


        public static void DrawChar(Transform transform, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Player.texture, transform.position.X, transform.position.Y, transform.size.Width, transform.size.Height);
        }

        private static void DrawLvl(Level level, PaintEventArgs g)
        {
            foreach (var tile in level.tiles)
                if (tile.texture != null)
                {
                    g.Graphics.DrawImage(tile.texture, tile.transform.position.X, tile.transform.position.Y, tile.transform.size.Width, tile.transform.size.Height);
                }
            level.Drawn = true;
        }

        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 'D' )
            {
                if(game.Collide(game.level.tiles, Game.player, new PointF(Player.velocity * timer.Interval, 0)))
                    Game.player.Move(new PointF(Player.velocity * timer.Interval, 0));
            }
            if (e.KeyValue == 'A'  )
                {
                    if(game.Collide(game.level.tiles, Game.player, new PointF(-Player.velocity * timer.Interval, 0)))
                    Game.player.Move(new PointF(-Player.velocity *timer.Interval, 0));
                }
            if(e.KeyCode == Keys.Space)
            {
                if(game.Collide(game.level.tiles, Game.player, new PointF(0, -Player.velocity * 2 * timer.Interval)))
                Game.player.Move(new PointF(0, -Player.velocity * (float)3.6 * timer.Interval));
            }

           /// if(e.KeyCode == Keys.Shift)
        }   
    }
}
