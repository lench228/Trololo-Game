using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Trololo.Domain;
using System.Media;
using Trololo.Properties;
using System.Linq;
using Trololo.Domain.Projectiles;
using System.Linq.Expressions;

namespace Trololo.View
{

    public partial class GameControl : UserControl
    {

        public static System.Windows.Forms.Timer timer;

        private static Graphics g;
        private static Game game;
        private Player player;
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
            Player.invincibleTime -= 1000; 

            if (Player.invincibleTime <= 0)
            {
                invincibleTimer.Stop();
                player.IsShooting = true; 
                player.UnsetInvins();
                cooldownTimer.Start();
                Player.invincibleTime = 5000; 
            }
        }

        private void CooldownTimer_Tick(object sender, EventArgs e)
        {
            progressBar.Value += 1000;
            Player.invincibleCooldown -= 1000;

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
            if (!player.IsInvincible && Player.invincibleCooldown == 20000)
            {
                player.SetInvins(); 
                invincibleTimer.Start();
                player.IsShooting = false;
                PlayMedia(mvpSound);
                player.bullets.Clear();
            }
        }


        public void Run(Game Game)
        {
            g = CreateGraphics();
            DoubleBuffered = true;
            game = Game;
            MouseClick += GameControl_MouseClick;
            this.KeyUp += GameControl_KeyUp;
            player = game.GetPlayer();
        }

        private void GameControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (Player.IsWithGun && player.bullets.Count < 3 && !player.IsInvincible)
            {
                player.IsShooting = true;
                player.bullets.Add(new Bullet(Resources.PlayerShootSprite, player.transform.Position,player.transform.Direction));
                PlayMedia(playerShootPlayer); 
            }
        }

        private void Update(object sender, EventArgs e)
        {
            player = game.GetPlayer();
            UpdateCharacterMovement();

            if (player.GetHealth() <= 0)
            {
                timer.Stop();
                game.Death();
                return; 
            }

            GravitationWork();

            var toDeleteEnemies = new Dictionary<Enemy, EnemyShoot>();
            var toDeletePlayerShoots = new List<Bullet>();
            var toDeleteHeals = new List<Domain.Projectiles.Heal>();

            if (player.transform.Position.X + player.transform.HitBox.Width>= 1360 && game.enemies.Count == 0)
                game.LoadStage(true);

            if (player.IsShooting && player.bullets.Count != 0)
                player.UpdatePlayerShots(game, player.bullets, toDeleteEnemies, toDeletePlayerShoots);

            CheckHeal(toDeleteHeals, player);

            foreach (var enemy in game.enemies.Keys)
                enemy.UpdateEnemy(game.enemies, game, player);
            DeleteObjects(toDeleteEnemies,toDeletePlayerShoots, toDeleteHeals, game, player);
            Invalidate();
        }

        private static void DeleteObjects(Dictionary<Enemy, EnemyShoot> toDeleteEnemies, List<Bullet> toDeleteShoots, List<Heal> heals ,Game game, Player player)
        {

            foreach (var value in toDeleteShoots)
                player.bullets.Remove(value);

            foreach(var heal in heals)
                game.heals.Remove(heal);

            foreach(var value in toDeleteEnemies.Keys)
                game.enemies.Remove(value);
        }

        public void GravitationWork()
        {
            if (!player.isOnLadder)
            {
                var move = new PointF(player.transform.Position.X, player.transform.Position.Y + player.gravity);
                if (CollitionsControl.Collide(player.transform.HitBox, move.X, move.Y, player.transform.HitBox.Width, player.transform.HitBox.Height, game.level.tiles))
                {
                    player.IsInFly = true;
                    player.transform.Move(new PointF(0, player.gravity));
                }
                else

                    player.IsInFly = false;
            }
            foreach (var heal in game.heals)
            {
                var move = new PointF(heal.Transform.Position.X, heal.Transform.Position.Y + 10);
                if (CollitionsControl.Collide(heal.Transform.HitBox, move.X, move.Y, heal.Transform.HitBox.Width, heal.Transform.HitBox.Height, game.level.tiles))
                    heal.Transform.Move(new PointF(2, 10));
            }
        }

        private void UpdateCharacterMovement()
        {
            var move = new PointF(0, 0);
            if (player.isMovingLeft)
            {
                move.X -= player.velocity;
                player.RotatePlayer(move, game);
            }
            else if (player.isMovingRight)
            {
                move.X += player.velocity;
                player.RotatePlayer(move, game);
            }

            if(player.isMoovingUp && player.isOnLadder)
            {
                move.Y -= player.velocity; 
            }
            else if(player.isMoovingDown && player.isOnLadder)
                move.Y += player.velocity;

            if (player.isJumping && !player.IsInFly)
            {
                move.Y -= player.velocity * 15;
                player.IsInFly = true;
            }

            if (CollitionsControl.Collide(player.transform.HitBox, player.transform.Position.X + move.X, player.transform.Position.Y + move.Y, player.transform.HitBox.Width, player.transform.HitBox.Height, game.level.tiles))
                player.transform.Move(move);

            player.isOnLadder = false; 

            foreach (var ladder in game.level.ladders)
            {
                if (ladder.transform.HitBox.IntersectsWith(player.transform.HitBox))
                {
                    player.isOnLadder = true; 
                    break; 
                }
            }

        }

        private static void CheckHeal(List<Heal> toDeleteHeals, Player player)
        {
            var intersected = game.heals.Where(x => x.Transform.HitBox.IntersectsWith(player.transform.HitBox));
            if (intersected.Count() == 0)
                return;
            foreach (var heal in intersected)
            {
                if (player.GetHealth() < 3)
                {
                    player.SetHealth(player.GetHealth() + 1);
                    toDeleteHeals.Add(heal);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (game.stage == GameStage.Play)
            {
                GraphicsMethods.DrawLvl(game.level, e);
                GraphicsMethods.DrawHealth(e, player.GetHealth());

                foreach (var enemy in game.enemies.Keys)
                {
                    GraphicsMethods.DrawEnemy(enemy.transform, e, enemy.texture);
                    if (enemy.isShooted)
                    {
                        GraphicsMethods.DrawProjectile(game.enemies[enemy].Transform, e, game.enemies[enemy].Texture);
                    }
                }

                foreach (var bullet in player.bullets)
                {
                    GraphicsMethods.DrawProjectile(bullet.Transform, e, bullet.Texture);
                }

                foreach (var heal in game.heals)
                    GraphicsMethods.DrawHeal(heal.Transform, e, heal.Texture);
                foreach (var ladder in game.level.ladders)
                {
                    GraphicsMethods.DrawProjectile(ladder.transform, e, ladder.texture);
                }
                GraphicsMethods.DrawChar(player.transform, e, player);
            }
        }


        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
                player.isMovingLeft = true;
            else if (e.KeyCode == Keys.D)
                player.isMovingRight = true;
            else if (e.KeyCode == Keys.Space)
                player.isJumping = true;
            else if(e.KeyCode == Keys.W)
                player.isMoovingUp= true;
            else if(e.KeyCode == Keys.S)
                player.isMoovingDown= true;

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
                player.isMovingLeft = false;
            else if (e.KeyCode == Keys.D)
                player.isMovingRight = false;
            else if (e.KeyCode == Keys.Space)
                player.isJumping = false;
            else if(e.KeyCode == Keys.W)
                player.isMoovingUp = false;
            else if(e.KeyCode == Keys.S)
                player.isMoovingDown= false;
        }

        public static void PlayMedia(SoundPlayer sound)
        {
            sound.Play();
            sound.Dispose();
        }
    }
}
