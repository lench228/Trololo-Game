using System;
using System.Drawing;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Trololo.View;

namespace Trololo.Domain
{
    public class Game
    {
        private GameStage stage = GameStage.NotStarted;
        public Level level;
        public static Player player;
        public static int currentLevel;
        public event Action<GameStage> StageChanged;

        public Game() 
        {
            player = new Player();
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

        public static void SetPlayerTransform(Point x)
        {
            player.SetTransform(x);
        }


        public bool Collide(Tile[,] tiles, Player player, PointF dMoove)
        {
            var rect = new RectangleF(Player.transform.position.X+dMoove.X, Player.transform.position.Y + dMoove.Y, Player.transform.size.Width, Player.transform.size.Height);

            if (rect.Location.X > 1200)
                this.LoadStage();

             var IMooveTo = tiles[(int)(dMoove.X > 0 ? rect.Right / 140 : rect.Left / 140),(int)(dMoove.Y > 0 ? rect.Bottom / 140 : rect.Top / 140)];
            if (IMooveTo.IsBorder)
            {  
                return false;
            }
            return true;
        }


        public void LoadStage()
        {
            level = Level.SplitLines(File.ReadAllText($"C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\Domain\\Levels\\level{currentLevel}.txt"));
            if(currentLevel < 3)
                currentLevel += 1;
        }
    }
}