using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Trololo.View;
using System.Collections.Generic; 

namespace Trololo.Domain
{
    public class Game : CollisionsController
    {
        private GameStage stage = GameStage.NotStarted;
        public Level level;
        public static Player player;
        public static int currentLevel;
        public event Action<GameStage> StageChanged;
        public static List<Enemy>  enemies; 
        

        public Game() 
        {
            enemies = new List<Enemy>();  
            currentLevel = 1;
            stage = GameStage.NotStarted;
            LoadStage();  
        }


        public void Start()
        {
            this.ChangeStage(GameStage.Play);
        }

        private void ChangeStage(GameStage stage)
        {
            this.stage = stage;
            StageChanged?.Invoke(stage);
        }

        public static void CreatePlayer(Point x)
        {
            player = new Player(); 
            player.SetTransform(x);
        }

        public static void CreateEnemy(Point x, int type)
        {
            var enemy = new Enemy(type);   
            enemies.Add(enemy);

            enemy.SetTransform(x); 
        }

        public void LoadStage()
        {
            enemies = new List<Enemy>();
            level = Level.SplitLines(File.ReadAllText($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\Domain\\Levels\\level{currentLevel}.txt"));
            if(currentLevel < 3)
                currentLevel += 1;
        }
    }
}