using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Trololo.View;
using System;
using System.Text;

namespace Trololo.Domain
{
    public class EnemySky : Entity
    {
        private int enemyType;
        private int direction = 1;
        private int counter = 0;
        public bool isLoopEnd = false;
        public bool isShooted;
        public EnemySky(int Type)
        {
            SetHealth(3);
            this.texture = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\EnemySky.png");
            this.velocity = (float)5;
            enemyType = Type;
            isShooted = false;
        }

        public void Patrol(Tile[,] tiles)
        {

            var move = new PointF(direction * velocity * 2, 0);
            if (HelpMethods.Collide(transform.hitBox, move.X + transform.position.X, move.Y + transform.position.Y, transform.hitBox.Width, transform.hitBox.Height, tiles))
            {
                transform.Move(move);
            }
            else
            {
                direction = -direction;
                counter++;
            }
            if (counter == 2)
            {
                direction = 1;
                isLoopEnd = true;
            }
        }

        public void GoGorizontal(Tile[,] tiles)
        {
            if (HelpMethods.Collide(transform.hitBox, transform.position.X, transform.position.Y + 10*direction, transform.hitBox.Width, transform.hitBox.Height, tiles))
                transform.Move(new PointF(0, 6 * direction));
            else
            {
                direction= -direction;
            }
        }
    }

}
