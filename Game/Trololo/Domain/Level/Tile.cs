namespace Trololo.Domain
{
    using System.Drawing;
    using System.Runtime.Serialization;

    public class Tile
    {
        public Transform transform { get; set; }
        public Image texture { get; set; }
        public bool IsBorder { get; set; }


        public bool Equals(Tile obj)
        {
            return transform.Position.X == obj.transform.Position.X && transform.Position.Y == obj.transform.Position.Y;
        }
    }
}
