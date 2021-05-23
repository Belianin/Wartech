using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Wartech
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private int hexWidth = 45;
        private int hexHeight = 18;
        private Texture2D hexTexture;
        private int textureScale = 2;
        private Vector2 camera = Vector2.Zero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hexTexture = Content.Load<Texture2D>("hex");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            for (int y = 0; y < 16; y++)
                for (int x = 0; x < 16; x++)
                    spriteBatch.Draw(hexTexture, new Vector2(
                        (x * (hexWidth / 2)) * textureScale,
                        ((y * hexHeight) + x * (9)) * textureScale), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}