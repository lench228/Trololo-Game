namespace Trololo.Domain
{
    using System.Drawing;
    using System.Runtime.Serialization;

    public class Tile
    {
        public Transform Transform { get; set; }
        public Image Texture { get; set; }
        public bool IsBorder { get; set; }


        public bool Equals(Tile obj)
        {
            return Transform.Position.X == obj.Transform.Position.X && Transform.Position.Y == obj.Transform.Position.Y;
        }
    }
}
