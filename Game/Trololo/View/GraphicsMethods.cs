using System;
using System.Collections.Generic;
using System.Drawing;

using System.Windows.Forms;
using Trololo.Domain;
using Trololo.View;
using Levels;
using Trololo.Properties;

internal static class GraphicsMethods
{
    private static Game game;
    public static void DrawChar(Transform transform, PaintEventArgs e, Player player)
    {
        e.Graphics.DrawImage(player.texture, transform.Position.X, transform.Position.Y, transform.HitBox.Width, transform.HitBox.Height);
        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.HitBox.X, (int)transform.HitBox.Y, (int)transform.HitBox.Width, (int)transform.HitBox.Height));
    }

    public static void DrawEnemy(Transform transform, PaintEventArgs e, Image enemyTexture)
    {
        e.Graphics.DrawImage(enemyTexture, transform.Position.X, transform.Position.Y, transform.HitBox.Width, transform.HitBox.Height);
        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)transform.HitBox.Width, (int)transform.HitBox.Height));
    }

    public static void DrawProjectile(Transform transform, PaintEventArgs e, Image projectileImage)
    {
        e.Graphics.DrawImage(projectileImage, transform.Position.X, transform.Position.Y, transform.HitBox.Width, transform.HitBox.Height);
        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)transform.HitBox.Width, (int)transform.HitBox.Height));
    }

    public static void DrawHeal(Transform transform, PaintEventArgs e, Image healImage)
    {
        e.Graphics.DrawImage(healImage, transform.Position.X, transform.Position.Y, transform.HitBox.Width, transform.HitBox.Height);
        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)transform.HitBox.Width, (int)transform.HitBox.Height));
    }

    public static void DrawLvl(Level level, PaintEventArgs g, Player player)
    {
        foreach (var tile in level.tiles)
        {
            if (tile.texture != null)
            {
                if (tile is GunTile && player.States.IsWithGun) 
                    continue;
                g.Graphics.DrawImage(tile.texture, tile.transform.Position.X, tile.transform.Position.Y, tile.transform.HitBox.Width, tile.transform.HitBox.Height);
            }
        }
    }

    public static void DrawHealth(PaintEventArgs e, int health)
    {
        var emptyHealthTexture = Resources.HealthEmpty;
        var fullHealthTexture = Resources.HealthFull;
        e.Graphics.DrawImage(emptyHealthTexture, 10, 803, emptyHealthTexture.Width, emptyHealthTexture.Height);
        for (var i = 0; i < health; i++)
        {
            e.Graphics.DrawImage(fullHealthTexture, 55 + i * 100, 855, fullHealthTexture.Width, fullHealthTexture.Height);
        }
    }
}
