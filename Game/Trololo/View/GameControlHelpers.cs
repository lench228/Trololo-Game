using System.Drawing;
using Trololo.Domain;

internal static class GameControlHelpers
{

    public static void GravitationWork()
    {
        var move = new PointF(Game.player.transform.position.X, Game.player.transform.position.Y + Player.gravity);
        if (CollisionsController.Collide(move.X, move.Y, Game.player.transform.hitBox.Width, Game.player.transform.hitBox.Height, game.level.tiles))
            Game.player.Move(new PointF(0, Player.gravity));
    }
}