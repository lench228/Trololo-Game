using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Levels;
using Trololo.Domain.Projectiles;
using Trololo.Properties;

namespace Trololo.Domain
{
    public class Game 
    {
        public GameStage Stage = GameStage.NotStarted;
        public Level Level;
        public static int CurrentLevel;
        public event Action<GameStage> StageChanged;
        public Dictionary<Enemy, EnemyShoot> Enemies = new Dictionary<Enemy, EnemyShoot>();
        public List<Heal> Heals= new List<Heal>();

        private Player player;
        public Player GetPlayer() => player;



        public Game()
        {
            CurrentLevel = 0;
            Stage = GameStage.NotStarted;
            if (Stage == GameStage.Play)
                LoadStage(false);
        }
        
        public void Exit()
        {
            ChangeStage(GameStage.Exit);
        }
        public void Win()
        {
            
            ChangeStage(GameStage.Final);
        }
        public void CreateEnemy(Point location)
        {
            var enemy = new Enemy();
            Enemies[enemy] = new EnemyShoot(Image.FromFile("View//Images//EnemyShot.png"), enemy.Transform.Position, player);
            enemy.SetTransform(location);
        }

        public void CreateHeal(PointF location)
        {
            Heals.Add(new Projectiles.Heal(Image.FromFile("View//Images//Heal.png"), location));
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
            CurrentLevel = 0;
            player.States.IsShooting = false;
            GuideTile.number = 0;
            Enemies = new Dictionary<Enemy, EnemyShoot>(); 
            ChangeStage(GameStage.Menu); 
        }

        public void RestartLevel()
        {
            player.SetHealth(3);
            Enemies = new Dictionary<Enemy, EnemyShoot>();
            LoadStage(false);

            player.Transform.Position = Level.PlayerSpawn.Transform.Position;
            var tempRect = player.Transform.HitBox; 
            tempRect.Location = Level.PlayerSpawn.Transform.Position;
            player.Transform.HitBox = tempRect;

            this.ChangeStage(GameStage.Play); 
        }

        public void Death()
        {
            this.ChangeStage(GameStage.Death);
        }

        private void ChangeStage(GameStage stage)
        {
            this.Stage = stage;
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
            if (CurrentLevel < 12 && isNextToLoad)
                CurrentLevel += 1;
            if (Stage == GameStage.Pause && Stage == GameStage.Death)
            {
                Enemies = new Dictionary<Enemy, EnemyShoot>();
            }
            Heals = new List<Heal>();
            Level = new Level(File.ReadAllText($"Levels\\level{CurrentLevel}.txt"), this, isNextToLoad);
        }
    }
}