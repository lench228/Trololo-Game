namespace Trololo.Domain
{
    using System.Drawing;
    using System.Runtime.Serialization;

    public class Tile
    {
        public Transform transform; 
        public Image texture;
        public bool IsBorder;
        public bool IsGunTile; 
        public string Name;

        public bool Equals(Tile obj)
        {
            return transform.position.X == obj.transform.position.X && transform.position.Y == obj.transform.position.Y;
        }
    }
}
