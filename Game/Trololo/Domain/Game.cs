using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;
using Levels;
using Trololo.Domain.Projectiles;
using Trololo.Properties;

namespace Trololo.Domain
{
    public class Game 
    {
        public GameStage stage = GameStage.NotStarted;
        public Level level;
        private Player player;
        public static int currentLevel;
        public event Action<GameStage> StageChanged;

        public Dictionary<Enemy, EnemyShoot> enemies = new Dictionary<Enemy, EnemyShoot>();
        public List<Heal> heals= new List<Heal>();

        public Player GetPlayer() => player;
        public void CreateEnemy(Point location)
        {
            var enemy = new Enemy();
            enemies[enemy] = new EnemyShoot(Resources.EnemyShootSprite, enemy.Transform.Position, player);
            enemy.SetTransform(location);
        }

        public void CreateHeal(PointF location)
        {
            heals.Add(new Projectiles.Heal(Resources.Heal, location));
        }

        public Game() 
        {
            currentLevel = 10;
            stage = GameStage.NotStarted;
            if(stage == GameStage.Play)
                LoadStage(false);  
        }

        public void SetPause()
        {
            ChangeStage(GameStage.Pause);
        }

        public void ContinueGame()
        {
            this.ChangeStage(GameStage.Play); 
        }

        public void Start()
        {
            GuideTile.number = 0;
            LoadStage(true);
            this.ChangeStage(GameStage.Play);
        }

        public void ShowMenu()
        {
            player.SetHealth(3);
            currentLevel = 0;
            player.States.IsShooting = false;
            enemies = new Dictionary<Enemy, EnemyShoot>(); 
            ChangeStage(GameStage.Menu); 
        }

        public void RestartLevel()
        {
            player.SetHealth(3);
            enemies = new Dictionary<Enemy, EnemyShoot>();
            LoadStage(false);

            player.Transform.Position = level.PlayerSpawn.transform.Position;
            var tempRect = player.Transform.HitBox; 
            tempRect.Location = level.PlayerSpawn.transform.Position;
            player.Transform.HitBox = tempRect;

            this.ChangeStage(GameStage.Play); 
        }

        public void Death()
        {
            this.ChangeStage(GameStage.End);
        }

        private void ChangeStage(GameStage stage)
        {
            this.stage = stage;
            StageChanged?.Invoke(stage);
        }

        public void CreatePlayer(Point x)
        {
            if (player == null)
                player = new Player(3);
            else
                player = new Player(player.GetHealth());
            player.SetTransform(x);
        }

        public void LoadStage(bool isNextToLoad)
        {

            if (currentLevel < 15 && isNextToLoad)
                currentLevel += 1;
            if (stage == GameStage.Pause && stage == GameStage.End)
            {
                enemies = new Dictionary<Enemy, EnemyShoot>();
            }
            heals = new List<Heal>();
            level = new Level(File.ReadAllText($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\Domain\\Levels\\level{currentLevel}.txt"), this, isNextToLoad);
        }

        public static void GunIsPicked()
        {
            //player.States.IsWithGun= true;
        }
    }
}