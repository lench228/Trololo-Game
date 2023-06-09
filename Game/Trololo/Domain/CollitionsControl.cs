﻿using System;
using System.Drawing;
using System.Runtime.ExceptionServices;
using Trololo.View;

namespace Trololo.Domain
{
    public class CollitionsControl
    {
        public static bool Collide(RectangleF hitbox, float x, float y, float width, float height, Tile[,] tiles)
        {
            if (tiles == null)
                return false;
            for (var i = x; i <= width + x; i += 2)
                for (var j = y; j <= height + y; j += 2)
                    if (IsBorder(i, j, tiles))
                        return false;
            return true;
        }

        private static bool IsBorder (float x, float y, Tile[,] tiles)
        {
            if (x < 0 || x >= tiles.GetLength(0) * 60)
                return true; 
            if(y < 0 || y >= tiles.GetLength(1) * 60)   
                return true;

            var value = tiles[(int)(x / 60), (int)(y / 60)]; 

            if(value.IsBorder)
                return true;
            return false;
        }
    }
}