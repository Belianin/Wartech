﻿using System;
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
        private Vector2 camera = new Vector2(-300, -100);
        
        private Vector2 selectedHex = Vector2.Zero;

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
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 18; x+=2)
                    spriteBatch.Draw(hexTexture, GetHexScreenCoordinates(x, y), Color.White);
                for (int x = 1; x < 16; x+=2)
                    spriteBatch.Draw(hexTexture, GetHexScreenCoordinates(x, y), Color.White);
            }
            
            spriteBatch.Draw(hexTexture, selectedHex, Color.Aqua);
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
            Console.WriteLine($"{x + camera.X}:{y + camera.Y}");
            var r = new Vector2(
                (x + camera.X) / ((hexWidth / 2) * textureScale),
                (((y + camera.Y) / textureScale) - ((x % 2) * 9)) / hexHeight);
            Console.WriteLine($"{r.X}:{r.Y}");

            return r;
        }
    }
}