using System.Drawing;

namespace Trololo.Domain
{
    public class Floor
    {
        public static Image image;

        static Floor()
        {
            image = Image.FromFile("C:\\Users\\wrwsc\\Desktop\\Trololo-Game\\Game\\Trololo\\View\\Source\\blockTexture.png");
        } 
    }
}
