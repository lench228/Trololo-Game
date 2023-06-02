using System.Drawing; 

namespace Trololo.Domain
{
    public class Entity
    {
        public Transform transform = new Transform(new PointF(0,0), new RectangleF());

        private int health;
        public float velocity { get; set; }
        public readonly float gravity = (float)3;

        public Image texture = null;
  
        public void SetTransform(PointF position)
        {
            transform.Position = position;
            if (texture != null)
                transform.HitBox = new RectangleF(position.X, position.Y, texture.Width, texture.Height);
            else
                transform.HitBox =  new RectangleF(position.X, position.Y, 0, 0);
        }

        public int GetHealth()
        {
            return health;
        }

        public void SetHealth(int health)
        {
            if(health > 0)
                this.health = health;
        }

        public void Hurt()
        {
            health--; 
        }
    }
}
