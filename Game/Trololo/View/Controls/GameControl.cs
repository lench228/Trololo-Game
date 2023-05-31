using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Trololo.Domain;
using System.Media;
using Trololo.Properties;

namespace Trololo.View
{

    public partial class GameControl : UserControl
    {

        public static System.Windows.Forms.Timer timer;

        private static Graphics g;
        private static Game game;
        private static Timer invincibleTimer; 
        private static Timer cooldownTimer;
        private System.Windows.Forms.ProgressBar progressBar;

        public static SoundPlayer enemyShootPlayer;
        private static SoundPlayer playerShootPlayer;
        private static SoundPlayer mvpSound; 


        public GameControl()
        {
            InitializeComponent();
            progressBar = new ProgressBar();
            this.Controls.Add(progressBar);

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(Update);

            invincibleTimer = new Timer();
            invincibleTimer.Interval = 1000; // Интервал 1 секунда
            invincibleTimer.Tick += InvincibleTimer_Tick;

            cooldownTimer = new Timer();
            cooldownTimer.Interval = 1000; // Интервал 1 секунда
            cooldownTimer.Tick += CooldownTimer_Tick;
            enemyShootPlayer = new SoundPlayer($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Sound\\EnemyShoot.wav");
            playerShootPlayer = new SoundPlayer($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Sound\\PlayerShoot.wav");
            mvpSound = new SoundPlayer($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Sound\\mvpSound.wav");
            RunProgressBar();
        }

        private void InvincibleTimer_Tick(object sender, EventArgs e)
        {
            Player.invincibleTime -= 1000; // Уменьшаем время непобедимого режима на 1 секунду

            if (Player.invincibleTime <= 0)
            {
                invincibleTimer.Stop();
                game.player.IsInvincible = false;
                game.player.IsShooting = true; 
                game.player.UnsetInvins();
                // Запускаем отсчет времени до следующего использования непобедимого режима
                cooldownTimer.Start();
                Player.invincibleTime = 5000; 
            }
        }

        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            progressBar.Value += 1000; // Увеличиваем значение прогресс-бара на 1
            Player.invincibleCooldown -= 1000; // Уменьшаем время отката непобедимого режима на 1 секунду

            if (Player.invincibleCooldown <= 0)
            {
                cooldownTimer.Stop();
                progressBar.Value = 0;
                Player.invincibleCooldown = 20000;
            }
        }

        private void RunProgressBar()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = (int)Player.invincibleCooldown; 
            progressBar.Value = 0;
            progressBar.Location = new Point(360, 870);
            progressBar.Size = new Size(180, 60);
        }

        private void ActivateInvincibleMode()
        {
            if (!game.player.IsInvincible && Player.invincibleCooldown == 20000)
            {
                game.player.SetInvins(); 
                invincibleTimer.Start();
                game.player.IsShooting = false;
                PlayMedia(mvpSound); 
            }
        }


        public void Run(Game Game)
        {
            g = CreateGraphics();
            DoubleBuffered = true;
            game = Game;
            MouseClick += GameControl_MouseClick;
            this.KeyUp += GameControl_KeyUp;
        }

        private void GameControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (Player.IsWithGun && game.player.bullets.Count < 3)
            {
                game.player.IsShooting = true;
                game.player.bullets.Add(new Bullet(Resources.PlayerShootSprite, game.player.transform.position,game.player.transform.Direction));
                PlayMedia(playerShootPlayer); 
            }
        }

        private void Update(object sender, EventArgs e)
        {
            UpdateCharacterMovement(); 

            if (game.player.GetHealth() <= 0)
            {
                timer.Stop();
                game.Death();
                return; 
            }

            GravitationWork();

            var toDeleteEnemies = new Dictionary<Enemy, EnemyShoot>();
            var toDeletePlayerShoots = new List<Bullet>();  

            if (game.player.transform.position.X + game.player.transform.hitBox.Width>= 1360 && game.enemies.Count == 0)
                game.LoadStage(true);

            if (game.player.IsShooting)
                game.player.UpdatePlayerShots(game, game.player.bullets, toDeleteEnemies, toDeletePlayerShoots);

            Enemy.UpdateEnemy(game.enemies, game);
            DeleteObjects(game.enemies, toDeleteEnemies, toDeletePlayerShoots, game); 
            Invalidate();
        }

        private static void DeleteObjects(Dictionary<Enemy, EnemyShoot> enemies, Dictionary<Enemy, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots, Game game)
        {
            foreach (var key in toDeleteEnemies.Keys)
            {
                enemies.Remove(key);
            }

            foreach (var value in toDeleteShoots)
                game.player.bullets.Remove(value);
        }




        public void GravitationWork()
        {
            var move = new PointF(game.player.transform.position.X, game.player.transform.position.Y + Player.gravity);
            if (CollitionsControl.Collide(game.player.transform.hitBox, move.X, move.Y, game.player.transform.hitBox.Width, game.player.transform.hitBox.Height, game.level.tiles))
                game.player.transform.Move(new PointF(0, Player.gravity));
            else
                game.player.IsJumping = false;
        }

        private void UpdateCharacterMovement()
        {
            var move = new PointF(0, 0);
            if (game.player.isMovingLeft)
            {
                move.X -= game.player.velocity;
                move.Y += 0;
                game.player.RotatePlayer(move, game);
            }
            else if (game.player.isMovingRight)
            {
                move.X += game.player.velocity;
                move.Y += 0;
                game.player.RotatePlayer(move, game);
            }

            if (game.player.isJumping && !game.player.IsJumping)
            {
                move.X += 0;
                move.Y -= game.player.velocity*15;
                game.player.IsJumping = true;
            }
            if (CollitionsControl.Collide(game.player.transform.hitBox, game.player.transform.position.X + move.X, game.player.transform.position.Y + move.Y, game.player.transform.hitBox.Width, game.player.transform.hitBox.Height, game.level.tiles))
                game.player.transform.Move(move);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (game.stage == GameStage.Play)
            {
                GraphicsMethods.DrawLvl(game.level, e);
                GraphicsMethods.DrawHealth(e, game.player.GetHealth());

                foreach (var enemy in game.enemies.Keys)
                {
                    GraphicsMethods.DrawEnemy(enemy.transform, e, enemy.texture);
                    if (enemy.isShooted)
                    {
                        GraphicsMethods.DrawProjectile(game.enemies[enemy].transform, e, game.enemies[enemy].texture);
                    }
                }

                foreach (var bullet in game.player.bullets)
                {
                    GraphicsMethods.DrawProjectile(bullet.transform, e, bullet.texture);
                }

                GraphicsMethods.DrawChar(game.player.transform, e, game);
            }
        }


        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
                game.player.isMovingLeft = true;
            else if (e.KeyCode == Keys.D)
                game.player.isMovingRight = true;
            else if (e.KeyCode == Keys.Space)
                game.player.isJumping = true;

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

        private void GameControl_KeyUp(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.A)
                game.player.isMovingLeft = false;
            else if (e.KeyCode == Keys.D)
                game.player.isMovingRight = false;
            else if (e.KeyCode == Keys.Space)
                game.player.isJumping = false;
        }

        public static void PlayMedia(SoundPlayer sound)
        {
            sound.Play();
            sound.Dispose();
        }
    }
}
