using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wartech
{
    public class Effect
    {
        public Animation Animation { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }

        public Effect(Vector2 position, Texture2D texture, Animation animation)
        {
            Animation = animation;
            Texture = texture;
            Position = position;
        }
    }

    public static class SpriteBatchExtensions
    {
        public static void Draw(this SpriteBatch spriteBatch, Effect effect, Vector2 camera)
        {
            spriteBatch.Draw(
                effect.Texture,
                new Vector2(effect.Position.X - camera.X, effect.Position.Y - camera.Y),
                effect.Animation.GetFrame(),
                Color.White);
        }
    }
}