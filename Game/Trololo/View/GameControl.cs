using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
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
            timer.Interval = 10;
            timer.Tick += new EventHandler(Update);
            timer.Start();   
        }

        private void Update(object sender, EventArgs e)
        {
            GravitationWork();

            if (Game.player.transform.position.X >= 1240)
                game.LoadStage();

            foreach (var enemy in Game.enemies)
                enemy.Moove(game.level.tiles); 
            Invalidate();
        }

        public static void GravitationWork()
        {
            var move = new PointF(Game.player.transform.position.X, Game.player.transform.position.Y + Player.gravity);
            if (CollisionsController.Collide(move.X, move.Y, Game.player.transform.hitBox.Width, Game.player.transform.hitBox.Height, game.level.tiles))
                Game.player.Move(new PointF(0, Player.gravity));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var enemy in Game.enemies)
                DrawEnemy(enemy.transform, e); 
            DrawLvl(game.level, e);
            DrawChar(Game.player.transform, e);
        }

        public static void DrawEnemy(Transform transform, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\EnemySky.png"), transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);
        }

        public static void DrawChar(Transform transform, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Game.player.texture, transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);
        }

        private static void DrawLvl(Level level, PaintEventArgs g)
        {
            foreach (var tile in level.tiles)
                if (tile.texture != null)
                {
                    g.Graphics.DrawImage(tile.texture, tile.transform.position.X, tile.transform.position.Y, tile.transform.hitBox.Width, tile.transform.hitBox.Height);
                }
            level.Drawn = true;
        }

        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {
            var move = new PointF(0, 0);

            if (e.KeyValue == 'D' )
            {
                move.X += Player.velocity * timer.Interval;
                move.Y += 0;
            }
           if (e.KeyValue == 'A'  )
              {
                move.X -= Player.velocity * timer.Interval;
                move.Y+= 0;
               }
           if(e.KeyCode == Keys.Space)
           {
                move.X += 0;
                move.Y -= Player.velocity * timer.Interval * 4;           
           }
            if (CollisionsController.Collide(Game.player.transform.position.X + move.X, Game.player.transform.position.Y + move.Y, Game.player.transform.hitBox.Width, Game.player.transform.hitBox.Height, game.level.tiles))
                Game.player.Move(move);

            ///// if(e.KeyCode == Keys.Shift)
        }   
    }
}
