using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trololo.Domain.Projectiles
{
    public class Heal : Projectile
    {
        public Heal(Image projectTexture, PointF position) : base(projectTexture, position)
        {
        }
    }
}
