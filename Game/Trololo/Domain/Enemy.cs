using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Trololo.View;

namespace Trololo.Domain
{
    public class Enemy : Entity
    {
        private int enemyType;
        private int direction = 1;
        private int counter = 0;
        public Enemy(int Type)
        {
            transform = new Transform(new PointF(0, 0), new RectangleF());
            health = 2;
            this.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\EnemySky.png");
            velocity = (float)2;
            enemyType = Type; 
        }

        public void Moove(Tile[,] tiles)
        {
            var move = new PointF((this.transform.position.X + direction * velocity * 2), transform.position.Y);
            if (CollisionsController.Collide(move.X, move.Y, Game.player.transform.hitBox.Width, Game.player.transform.hitBox.Height, tiles))
            {
                if (transform.position.X > 1100 - (move.X - transform.position.X))
                {
                    direction = -1;
                    return;
                }
                
                transform.position.X = move.X;
                transform.position.Y = move.Y;
                if(transform.position.X == 0 - (move.X - transform.position.X))
                {
                    direction = 1; 
                    return;
                }
            }

            if (counter == 2)
                GoDown(); 
        }

        private void GoDown()
        {
            Trololo.View.GameControl.GravitationWork();
        }
    }
}
