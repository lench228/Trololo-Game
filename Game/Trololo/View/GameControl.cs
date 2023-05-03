using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
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
        private static Game game;
        private static Timer timer;
        public GameControl()
        {
            InitializeComponent();
        }

        public void Run(Game Game)
        {
            DoubleBuffered = true;
            game = Game;
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
            UpdateEnemy(game.level.enemies);
            Invalidate();
        }

        private static void UpdateEnemy(Dictionary<EnemySky, Projectile> enemies)
        {
            foreach (var enemy in enemies.Keys)
            {
                var rnd = new Random().Next(100);
                if (rnd == 40 && !enemy.isShooted)
                {
                    enemies[enemy].transform = new Transform(enemy.transform.position, new RectangleF(enemy.transform.position.X, enemy.transform.position.Y, 120, 120));
                    enemy.isShooted = true; 
                }
                if (!enemy.isLoopEnd)
                    enemy.Patrol(game.level.tiles);
                else
                    enemy.GoDown(game.level.tiles); 
            }
        }

        public void GravitationWork()
        {
            var move = new PointF(Game.player.transform.position.X, Game.player.transform.position.Y + Player.gravity);
            if (CollisionsController.Collide(move.X, move.Y, Game.player.transform.hitBox.Width, Game.player.transform.hitBox.Height, game.level.tiles))
                Game.player.transform.Move(new PointF(0, Player.gravity));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawLvl(game.level, e);

            foreach (var enemy in game.level.enemies.Keys)
            {
                DrawEnemy(enemy.transform, e, enemy.texture);
                if (enemy.isShooted)
                {
                    var shoot = game.level.enemies[enemy];
                    shoot.Shoot(Game.player);
                    if(Game.player.transform.hitBox.IntersectsWith(shoot.transform.hitBox))
                    {
                        
                    }
                    DrawProjectile(shoot.transform, e, shoot.texture);
                }
            }
            DrawChar(Game.player.transform, e);
        }

        public static void DrawEnemy(Transform transform, PaintEventArgs e, Image enemyTexture)
        {
            e.Graphics.DrawImage(enemyTexture, transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);
            e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.hitBox.Width, (int)transform.hitBox.Height));
        }

        public static void DrawProjectile(Transform transform, PaintEventArgs e, Image enemyTexture)
        {
            e.Graphics.DrawImage(enemyTexture, transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);
            e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.hitBox.Width, (int)transform.hitBox.Height));
        }

        public static void DrawChar(Transform transform, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Game.player.texture, transform.position.X, transform.position.Y, transform.hitBox.Width + 40, transform.hitBox.Height);
            e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.hitBox.Width , (int)transform.hitBox.Height));
        }

        private static void DrawLvl(Level level, PaintEventArgs g)
        {
            foreach (var tile in level.tiles)
                if (tile.texture != null)
                {
                    g.Graphics.DrawImage(tile.texture, tile.transform.position.X, tile.transform.position.Y, tile.transform.hitBox.Width, tile.transform.hitBox.Height);
                }
        }

        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {
            var move = new PointF(0, 0);

            switch (e.KeyCode)
            {
                case Keys.D:
                move.X += Player.velocity * timer.Interval;
                move.Y += 0;
                break;

                case Keys.A:
                move.X -= Player.velocity * timer.Interval;
                move.Y += 0;
                break;

                case Keys.Space:
                move.X += 0;
                move.Y -= Player.velocity * timer.Interval * 4;
                break;
            }
            if (CollisionsController.Collide(Game.player.transform.position.X + move.X, Game.player.transform.position.Y + move.Y, Game.player.transform.hitBox.Width, Game.player.transform.hitBox.Height, game.level.tiles))
                Game.player.transform.Move(move);
            if (move.X > 0)
                Game.player.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            if (move.X < 0)
                Game.player.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayerRotated.png"); 
        }
    }
}
