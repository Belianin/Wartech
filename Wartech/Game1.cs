using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Wartech
{
    public class Game1 : Game
    {
        private Level Level { get; set; }
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private int hexWidth = 45;
        private int hexHeight = 18;
        private Texture2D hexTexture;
        private int textureScale = 2;
        private Vector2 camera = new Vector2(-300, -100);
        
        private Vector2 selectedHex = Vector2.Zero;
        private Texture2D selectedHexTexture;

        private Texture2D artTexture;

        private Texture2D explosionTexture;

        private List<Effect> effects = new List<Effect>();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Level = new Level();
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hexTexture = Content.Load<Texture2D>("hex");
            selectedHexTexture = Content.Load<Texture2D>("selected_hex");

            artTexture = Content.Load<Texture2D>("art");

            explosionTexture = Content.Load<Texture2D>("a_explosion");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var cameraSpeed = 2;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
                camera.Y -= cameraSpeed;
            else if (state.IsKeyDown(Keys.S))
                camera.Y += cameraSpeed;
            if (state.IsKeyDown(Keys.D))
                camera.X += cameraSpeed;
            else if (state.IsKeyDown(Keys.A))
                camera.X -= cameraSpeed;
            // TODO: Add your update logic here

            var mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                selectedHex = GetHexGameCoordinates(mouseState.X, mouseState.Y);
                effects.Add(new Effect(new Vector2(mouseState.X + camera.X - 24, mouseState.Y + camera.Y - 40), explosionTexture, new Animation(64, 7, 7)));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            void DrawHex(int x, int y)
            {
                if (Level.Hexes[x, y] == null)
                    return;
                var coordinates = GetHexScreenCoordinates(x, y);
                
                spriteBatch.Draw(hexTexture, coordinates, Color.White);
                if (Level.Hexes[x, y].Building != null)
                {
                    coordinates.Y -= 20;
                    spriteBatch.Draw(artTexture, coordinates, Color.White);
                }
            }
            
            spriteBatch.Begin();
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 18; x+=2)
                    DrawHex(x, y);
                for (int x = 1; x < 16; x+=2)
                    DrawHex(x, y);
            }

            foreach (var effect in effects)
            {
                spriteBatch.Draw(effect, camera);
            }
            
            spriteBatch.Draw(selectedHexTexture, GetHexScreenCoordinates((int) selectedHex.X, (int) selectedHex.Y), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 GetHexScreenCoordinates(int x, int y)
        {
            return new Vector2(
                (x * (hexWidth / 2)) * textureScale - camera.X,
                ((y * hexHeight) + ((x % 2) * 9)) * textureScale - camera.Y);
        }

        private Vector2 GetHexGameCoordinates(int x, int y)
        {
            return new Vector2(
                (x + camera.X) / ((hexWidth / 2) * textureScale),
                (((y + camera.Y) / textureScale) - ((x % 2) * 9)) / hexHeight);
        }
    }
}