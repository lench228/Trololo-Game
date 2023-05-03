using System.Drawing;
using Trololo.View;

namespace Trololo.Domain
{
    public class CollisionsController
    {


        public static bool Collide(float x, float y, float width, float height, Tile[,] tiles)
        {
            if (!IsBorder(x, y, tiles))
                if (!IsBorder(x + width, y + height, tiles))
                    if (!IsBorder(x + width, y, tiles))
                        if (!IsBorder(x, y + height, tiles))
                            return true; 
            return false;
        }

        private static bool IsBorder (float x, float y, Tile[,] tiles)
        {
            if (x < 0 || x > 1390)
                return true; 
            if(y < 0 || y > 980)
                return true;

            var value = tiles[(int)(x / 140), (int)(y / 140)]; 

            if(value.IsBorder)
                return true;
            return false;
        }
    }
}