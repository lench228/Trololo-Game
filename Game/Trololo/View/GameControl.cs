using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Timers; 
using System.Windows.Forms;
using Trololo.Domain;

namespace Trololo.View
{

    public partial class GameControl : UserControl
    {

        public static System.Windows.Forms.Timer timer;

        private static Graphics g;
        private static Game game;
        private static System.Timers.Timer invincibleTimer; 
        private static System.Timers.Timer invincibleColdownTimer;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool isJumping = false;
        private bool isMovingDown = false;

        public GameControl()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(Update);

            invincibleTimer = new System.Timers.Timer();
            invincibleTimer.Elapsed += OnInvincibleTimerElapsed;

            invincibleColdownTimer = new System.Timers.Timer();
            invincibleColdownTimer.Elapsed += OnInvincibleCooldownTimerElapsed;

        }
        private void ActivateInvincibleMode()
        {
            if (!game.player.IsInvincible && invincibleColdownTimer.Enabled == false)
            {
                game.player.SetInvins(); 
                invincibleTimer.Interval = Player.invincibleTime;
                invincibleTimer.Start();
            }
        }

        private void OnInvincibleTimerElapsed(object sender, ElapsedEventArgs e)
        {
            game.player.IsInvincible = false;
            invincibleColdownTimer.Interval = Player.invincibleCooldown;
            game.player.UnsetInvins();
            invincibleColdownTimer.Start();
            invincibleTimer.Stop();
        }

        private void OnInvincibleCooldownTimerElapsed(object sender, ElapsedEventArgs e)
        {
            invincibleColdownTimer.Stop();
        }

        public void Run(Game Game)
        {
            g = CreateGraphics();
            DoubleBuffered = true;
            game = Game;
            MouseClick += GameControl_MouseClick;
            this.KeyUp += Form_KeyUp;
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
            UpdateCharacterMovement(); 
            if (game.player.GetHealth() == 0)
            {
                timer.Stop();
                game.Death();
                return; 
            }

            GravitationWork();
            var toDeleteEnemies = new Dictionary<EnemySky, EnemyShoot>();
            var toDeletePlayerShoots = new List<Bullet>();  
            if (game.player.transform.position.X +game.player.transform.hitBox.Width>= 1240)
                game.LoadStage(true);
            if (game.player.IsShooting)
                UpdatePlayerShots(game.player, game.player.bullets, toDeleteEnemies, toDeletePlayerShoots);
            UpdateEnemy(game.enemies, toDeleteEnemies, toDeletePlayerShoots);
            Invalidate();
        }

        private void UpdatePlayerShots(Player player, List<Bullet> bullets, Dictionary<EnemySky, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots)
        {

            foreach (var bullet in bullets)
            {
                bullet.Shoot();
                if(!HelpMethods.Collide(bullet.transform.hitBox, bullet.transform.position.X, bullet.transform.position.Y, bullet.transform.hitBox.Width, bullet.transform.hitBox.Height, game.level.tiles))
                {
                    toDeleteShoots.Add(bullet);
                    break;
                }
                foreach (var value in game.enemies.Keys)
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
                    shoot.Shoot(game.player.transform.position.X, game.player.transform.position.Y);
                }
                if (enemy.isShooted)
                {
                    shoot.transform.Move(new PointF(shoot.directionX * shoot.velocity, shoot.directionY * shoot.velocity));
                    if (game.player.transform.hitBox.IntersectsWith(shoot.transform.hitBox) && !game.player.IsInvincible)
                    {
                        game.player.Hurt();
                        enemy.isShooted = false;
                    }

                    if (!HelpMethods.Collide(game.player.transform.hitBox, shoot.transform.position.X, shoot.transform.position.Y, 100, 100, game.level.tiles))
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
            if (HelpMethods.Collide(game.player.transform.hitBox, move.X, move.Y, game.player.transform.hitBox.Width, game.player.transform.hitBox.Height, game.level.tiles))
                game.player.transform.Move(new PointF(0, Player.gravity));
            else
                game.player.IsJumping = false;
        }

        private void UpdateCharacterMovement()
        {
            var move = new PointF(0, 0);


            // Выполнение действий на основе состояния клавиш
            if (isMovingLeft)
            {
                move.X -= game.player.velocity;
                move.Y += 0;
                game.player.RotatePlayer(move, game);
            }
            else if (isMovingRight)
            {
                move.X += game.player.velocity;
                move.Y += 0;
                game.player.RotatePlayer(move, game);
            }

            if (isJumping && !game.player.IsJumping)
            {
                move.X += 0;
                move.Y -= game.player.velocity*12;
                game.player.IsJumping = true;
            }
            if (HelpMethods.Collide(game.player.transform.hitBox, game.player.transform.position.X + move.X, game.player.transform.position.Y + move.Y, game.player.transform.hitBox.Width, game.player.transform.hitBox.Height, game.level.tiles))
                game.player.transform.Move(move);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (game.stage == GameStage.Play)
            {
                GraphicsDraw.DrawLvl(game.level, e);
                GraphicsDraw.DrawHealth(e, game.player.GetHealth());


                foreach (var enemy in game.enemies.Keys)
                {
                    GraphicsDraw.DrawEnemy(enemy.transform, e, enemy.texture);
                    if (enemy.isShooted)
                    {
                        GraphicsDraw.DrawProjectile(game.enemies[enemy].transform, e, game.enemies[enemy].texture);
                    }
                }

                foreach (var bullet in game.player.bullets)
                {
                    GraphicsDraw.DrawProjectile(bullet.transform, e, bullet.texture);
                }

                GraphicsDraw.DrawChar(game.player.transform, e, game);
            }
        }


        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
                isMovingLeft = true;
            else if (e.KeyCode == Keys.D)
                isMovingRight = true;
            else if (e.KeyCode == Keys.Space)
                isJumping = true;


            if (e.KeyCode == Keys.Escape)
            {
                timer.Stop();
                game.SetPause();
            }

            if(e.KeyCode == Keys.E)
            {
                ActivateInvincibleMode();
            }

        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            // Обработка отпускания клавиш
            if (e.KeyCode == Keys.A)
                isMovingLeft = false;
            else if (e.KeyCode == Keys.D)
                isMovingRight = false;
            else if (e.KeyCode == Keys.Space)
                isJumping = false;
        }

    }
}
