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

        private bool isBulletReleased = false;


        public GameControl()
        {
            InitializeComponent();
            progressBar = new ProgressBar();
            this.Controls.Add(progressBar);

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(Update);

            invincibleTimer = new Timer();
            invincibleTimer.Interval = 1000;
            invincibleTimer.Tick += InvincibleTimer_Tick;

            cooldownTimer = new Timer();
            cooldownTimer.Interval = 1000;
            cooldownTimer.Tick += CooldownTimer_Tick;
            enemyShootPlayer = new SoundPlayer($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Sound\\EnemyShoot.wav");
            playerShootPlayer = new SoundPlayer($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Sound\\PlayerShoot.wav");
            RunProgressBar();
        }

        private void InvincibleTimer_Tick(object sender, EventArgs e)
        {
            Player.invincibleTime -= 1000; 

            if (Player.invincibleTime <= 0)
            {
                invincibleTimer.Stop();
                player.States.IsShooting = true; 
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
            if (!player.States.IsInvincible && Player.invincibleCooldown == 20000)
            {
                player.SetInvins(); 
                invincibleTimer.Start();
                player.States.IsShooting = false;
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
            MouseUp+= GameControl_MouseUp;
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

            if (player.States.IsShooting && player.bullets.Count != 0)
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
            if (!player.States.isOnLadder)
            {
                var move = new PointF(player.Transform.Position.X, player.Transform.Position.Y + player.gravity);
                if (CollitionsControl.Collide(player.Transform.HitBox, move.X, move.Y, player.Transform.HitBox.Width, player.Transform.HitBox.Height, game.level.tiles))
                {
                    player.States.IsInFly = true;
                    player.Transform.Move(new PointF(0, player.gravity));
                }
                else

                    player.States.IsInFly = false;
            }
            foreach (var heal in game.heals)
            {
                var move = new PointF(heal.Transform.Position.X, heal.Transform.Position.Y + 10);
                if (CollitionsControl.Collide(heal.Transform.HitBox, move.X, move.Y, heal.Transform.HitBox.Width, heal.Transform.HitBox.Height, game.level.tiles))
                    heal.Transform.Move(new PointF(2, 10));
            }
        }


        private static void CheckHeal(List<Heal> toDeleteHeals, Player player)
        {
            var intersected = game.heals.Where(x => x.Transform.HitBox.IntersectsWith(player.Transform.HitBox));
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
                GraphicsMethods.DrawLvl(game.level, e, player);

                foreach (var enemy in game.enemies.Keys)
                {
                    GraphicsMethods.DrawEnemy(enemy.Transform, e, enemy.texture);
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
                foreach (var ladder in game.level.Ladders)
                {
                    GraphicsMethods.DrawProjectile(ladder.transform, e, ladder.texture);
                }
                GraphicsMethods.DrawChar(player.Transform, e, player);
                GraphicsMethods.DrawHealth(e, player.GetHealth());
            }
        }

        public static void PlayMedia(SoundPlayer sound)
        {
            sound.Play();
            sound.Dispose();
        }

        /// КОНТРОЛЛЕР ИГРОКА
        private void GameControl_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
                player.States.isMovingLeft = true;
            else if (e.KeyCode == Keys.D)
                player.States.isMovingRight = true;
            else if (e.KeyCode == Keys.Space)
                player.States.isJumping = true;
            else if (e.KeyCode == Keys.W)
                player.States.isMoovingUp = true;
            else if (e.KeyCode == Keys.S)
                player.States.isMoovingDown = true;

            if (e.KeyCode == Keys.Escape)
            {
                timer.Stop();
                game.SetPause();
            }

            if (e.KeyCode == Keys.E)
            {
                ActivateInvincibleMode();
            }
        }

        private void GameControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                player.States.isMovingLeft = false;
            else if (e.KeyCode == Keys.D)
                player.States.isMovingRight = false;
            else if (e.KeyCode == Keys.Space)
                player.States.isJumping = false;
            else if (e.KeyCode == Keys.W)
                player.States.isMoovingUp = false;
            else if (e.KeyCode == Keys.S)
                player.States.isMoovingDown = false;
        }

        private void UpdateCharacterMovement()
        {
            var move = new PointF(0, 0);
            if (player.States.isMovingLeft)
            {
                move.X -= player.velocity;
                player.RotatePlayer(move, game);
            }
            else if (player.States.isMovingRight)
            {
                move.X += player.velocity;
                player.RotatePlayer(move, game);
            }

            if (player.States.isMoovingUp && player.States.isOnLadder)
            {
                move.Y -= player.velocity;
            }
            else if (player.States.isMoovingDown && player.States.isOnLadder)
                move.Y += player.velocity;

            if (player.States.isJumping && !player.States.IsInFly)
            {
                move.Y -= player.velocity * 10;
                player.States.IsInFly = true;
            }

            if (CollitionsControl.Collide(player.Transform.HitBox, player.Transform.Position.X + move.X, player.Transform.Position.Y + move.Y, player.Transform.HitBox.Width, player.Transform.HitBox.Height, game.level.tiles))
                player.Transform.Move(move);
            CheckExitTiles();
            CheckLadders();
            if (game.level.GunTile != null && !player.States.IsWithGun && player.Transform.HitBox.IntersectsWith(game.level.GunTile.transform.HitBox)) 
                player.States.IsWithGun= true;
        }

        private void CheckExitTiles()
        {
            foreach (var exitTile in game.level.ExitTiles)
                if (player.Transform.HitBox.IntersectsWith(exitTile.transform.HitBox) && game.enemies.Count == 0)
                {
                    game.LoadStage(true);
                    break;
                }
        }

        private void CheckLadders()
        {
            player.States.isOnLadder = false;

            foreach (var ladder in game.level.Ladders)
            {
                if (ladder.transform.HitBox.IntersectsWith(player.Transform.HitBox))
                {
                    player.States.isOnLadder = true;
                    break;
                }
            }
        }

        private void GameControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (player.States.IsWithGun && player.bullets.Count <= 3 && !player.States.IsInvincible && !isBulletReleased)
            {
                player.States.IsShooting = true;
                player.bullets.Add(new Bullet(Resources.PlayerShootSprite, player.Transform.Position, player.Transform.Direction));
                PlayMedia(playerShootPlayer);
                isBulletReleased = true;
            }
        }

        private void GameControl_MouseUp(object sender, MouseEventArgs e)
        {
            isBulletReleased = false;
        }
    }
}
