using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using Levels;

namespace Trololo.Domain
{
    public class Game : HelpMethods
    {
        public GameStage stage = GameStage.NotStarted;
        public Level level;
        public Player player;
        public static int currentLevel;
        public event Action<GameStage> StageChanged;
        
        public Dictionary<EnemySky, EnemyShoot> enemies = new Dictionary<EnemySky, EnemyShoot>();

        public void CreateEnemy(Point x, int type, Game game, bool flag)
        {
            var enemy = new EnemySky(type);
            enemies[enemy] = new EnemyShoot(Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\EnemyShot.png"), enemy.transform.position, game.player);
            enemy.SetTransform(x);
        }

        public Game() 
        {
            currentLevel = 0;
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

            LoadStage(false);
            this.ChangeStage(GameStage.Play); 
        }

        public void Start()
        {
            LoadStage(true);
            this.ChangeStage(GameStage.Play);
        }

        public void ShowMenu()
        {
            player.SetHealth(3);
            currentLevel = 0;
            player.IsShooting = false;
            enemies = new Dictionary<EnemySky, EnemyShoot>(); 
            ChangeStage(GameStage.Menu); 
        }

        public void RestartLevel()
        {
            player.SetHealth(3); 
            LoadStage(false);
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

            if (currentLevel < 10 && isNextToLoad)
                currentLevel += 1;
            if(stage == GameStage.Pause && stage == GameStage.End)
                enemies = new Dictionary<EnemySky, EnemyShoot>();
            level = new Level(File.ReadAllText($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\Domain\\Levels\\level{currentLevel}.txt"), this, isNextToLoad);
        }

        public static void GunIsPicked()
        {
            Player.IsWithGun= true;
        }

    }
}