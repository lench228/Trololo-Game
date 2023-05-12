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
            g = CreateGraphics();
            DoubleBuffered = true;
            game = Game;
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(Update);
            timer.Start();
            MouseClick += GameControl_MouseClick;
        }

        private void GameControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (Player.IsWithGun)
            {
                game.player.IsShooting = true;
                game.player.bullets.Add(new Bullet(Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\PlayerShoot.png"), game.player.transform.position,game.player.transform.Direction));
            }
        }

        private void Update(object sender, EventArgs e)
        {
            GravitationWork();
            var toDeleteEnemies = new Dictionary<EnemySky, EnemyShoot>();
            var toDeletePlayerShoots = new List<Bullet>();  
            if (game.player.transform.position.X >= 1240)
                game.LoadStage();
            if (game.player.IsShooting)
                UpdatePlayerShots(game.player, game.player.bullets, toDeleteEnemies, toDeletePlayerShoots);
            UpdateEnemy(game.level.enemies, toDeleteEnemies, toDeletePlayerShoots);
            Invalidate();
        }

        private void UpdatePlayerShots(Player player, List<Bullet> bullets, Dictionary<EnemySky, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots)
        {

            foreach (var bullet in bullets)
            {
                bullet.Shoot();
                if(!CollisionsController.Collide(bullet.transform.position.X, bullet.transform.position.Y, bullet.transform.hitBox.Width, bullet.transform.hitBox.Height, game.level.tiles))
                {
                    toDeleteShoots.Add(bullet);
                    break;
                }
                foreach (var value in game.level.enemies.Keys)
                {

                    if (bullet.transform.hitBox.IntersectsWith(value.transform.hitBox))
                    {
                        value.Hurt();
                        toDeleteShoots.Add(bullet);
                        if (value.GetHealth() == 0)
                            toDeleteEnemies[value] = null;
                    }
                }
            }
        }

        private static void UpdateEnemy(Dictionary<EnemySky, EnemyShoot> enemies, Dictionary<EnemySky, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots)
        {
            foreach(var key in toDeleteEnemies.Keys)
            {
                enemies.Remove(key);
            }

            foreach(var value in toDeleteShoots)
                game.player.bullets.Remove(value);
            foreach (var enemy in enemies.Keys)
            {
                
                var rnd = new Random().Next(100);
                var shoot = enemies[enemy];
                if (rnd == 40 && !enemy.isShooted)
                {
                    shoot.transform = new Transform(enemy.transform.position, new RectangleF(enemy.transform.position.X, enemy.transform.position.Y, 120, 120));
                    enemy.isShooted = true;
                    shoot.UpdatePlayerPos(game.player.transform.position);
                }
                if (enemy.isShooted)
                {
                    shoot.Shoot();
                    if (game.player.transform.hitBox.IntersectsWith(shoot.transform.hitBox))
                    {
                        game.player.Hurt();
                        enemy.isShooted = false;
                    }

                    if (!CollisionsController.Collide(shoot.transform.position.X, shoot.transform.position.Y, 100, 100, game.level.tiles))
                    {
                        ///enemies[enemy] = null;
                        enemy.isShooted = false;
                    }
                }
                if (!enemy.isLoopEnd)
                    enemy.Patrol(game.level.tiles);
                else
                    enemy.GoGorizontal(game.level.tiles); 
            }

            
        }

        public void GravitationWork()
        {
            var move = new PointF(game.player.transform.position.X, game.player.transform.position.Y + Player.gravity);
            if (CollisionsController.Collide(move.X, move.Y, game.player.transform.hitBox.Width, game.player.transform.hitBox.Height, game.level.tiles))
                game.player.transform.Move(new PointF(0, Player.gravity));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (game.stage == GameStage.Play)
            {
                GameControlHelpers.DrawLvl(game.level, e);
                GameControlHelpers.DrawHealth(e, game.player.GetHealth());


                foreach (var enemy in game.level.enemies.Keys)
                {
                    GameControlHelpers.DrawEnemy(enemy.transform, e, enemy.texture);
                    if (enemy.isShooted)
                    {
                        GameControlHelpers.DrawProjectile(game.level.enemies[enemy].transform, e, game.level.enemies[enemy].texture);
                    }
                }

                foreach (var bullet in game.player.bullets)
                {
                    GameControlHelpers.DrawProjectile(bullet.transform, e, bullet.texture);
                }

                GameControlHelpers.DrawChar(game.player.transform, e, game);
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
                move.Y -= Player.velocity * timer.Interval * 5.5f;
                break;
            }
            if (CollisionsController.Collide(game.player.transform.position.X + move.X, game.player.transform.position.Y + move.Y, game.player.transform.hitBox.Width, game.player.transform.hitBox.Height, game.level.tiles))
                game.player.transform.Move(move);
            if (move.X > 0)
            {
                game.player.transform.Direction = 1; 
                game.player.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayer.png");
            }
            if (move.X < 0)
            {
                game.player.transform.Direction = -1;
                game.player.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\testPlayerRotated.png");
            }
        }
    }
}
