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

    public static void DrawChar(Transform transform, PaintEventArgs e, Game game)
    {
        e.Graphics.DrawImage(game.player.texture, transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);

        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.hitBox.X, (int)transform.hitBox.Y, (int)transform.hitBox.Width, (int)transform.hitBox.Height));
    }

    public static void DrawEnemy(Transform transform, PaintEventArgs e, Image enemyTexture)
    {
        e.Graphics.DrawImage(enemyTexture, transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);
        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.hitBox.Width, (int)transform.hitBox.Height));
    }

    public static void DrawProjectile(Transform transform, PaintEventArgs e, Image projectileImage)
    {
        e.Graphics.DrawImage(projectileImage, transform.position.X, transform.position.Y, transform.hitBox.Width, transform.hitBox.Height);
        e.Graphics.DrawRectangle(new Pen(Color.Red), new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)transform.hitBox.Width, (int)transform.hitBox.Height));
    }

    public static void DrawLvl(Level level, PaintEventArgs g)
    {
        foreach (var tile in level.tiles)
            if (tile.texture != null)
            {
                if (tile.IsGunTile && Player.IsWithGun)
                    continue;
                g.Graphics.DrawImage(tile.texture, tile.transform.position.X, tile.transform.position.Y, tile.transform.hitBox.Width, tile.transform.hitBox.Height);
            }
    }

    public static void DrawHealth(PaintEventArgs e, int health)
    {
        var emptyHealthTexture = Resources.HealthEmpty;
        var fullHealthTexture = Resources.HealthFull;
        e.Graphics.DrawImage(emptyHealthTexture, 10, 803, emptyHealthTexture.Width, emptyHealthTexture.Height);
        for (var i = 0; i < health; i++)
            e.Graphics.DrawImage(fullHealthTexture, 55 + i * 100, 855, fullHealthTexture.Width, fullHealthTexture.Height);
    }
}