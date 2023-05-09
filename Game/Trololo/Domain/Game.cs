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
        public Player player;
        public static int currentLevel;
        public event Action<GameStage> StageChanged;


        public Game() 
        {

            currentLevel = 0;
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

        public void CreatePlayer(Point x)
        {
            player = new Player(); 
            player.SetTransform(x);
        }

        public void LoadStage()
        {
            if (currentLevel < 5)
                currentLevel += 1;
            level = new Level(File.ReadAllText($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\Domain\\Levels\\level{currentLevel}.txt"), this);

        }

        public static void GunIsPicked()
        {
            Player.IsWithGun= true;
        }

        internal void Hurt()
        {
            throw new NotImplementedException();
        }
    }
}