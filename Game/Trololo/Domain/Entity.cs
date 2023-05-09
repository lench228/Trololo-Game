﻿using System.Drawing; 

namespace Trololo.Domain
{
    public class Entity
    {
        public Transform transform = new Transform(new PointF(0,0), new RectangleF());
        private int health;
        public static float velocity;
        public static float gravity = (float)3.25;
        public Image texture;


        public void SetTransform(PointF position)
        {
            transform.position = position;
            transform.hitBox = new RectangleF(position.X, position.Y, texture.Width-50, texture.Height); 
        }

        public int GetHealth()
        {
            return health;
        }

        public void SetHealth(int health)
        {
            this.health = health;
        }
        public void Hurt()
        {
            health--; 
        }
    }
}
