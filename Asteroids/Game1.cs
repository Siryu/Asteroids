using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite player;
        Alien alien;
        Background background;
        KeyBoardManager keyboard;
        int wave = 0;
        int lives = 3;
        int counter = 0;

        SpriteFont font;

        BulletList bullets;
        Asteroid asteroids;

        Collision collision;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.IsFullScreen = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            player = new Sprite(graphics, Content.Load<Texture2D>("playerShip"));

            alien = new Alien(Content, graphics, player);
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new Background(graphics.GraphicsDevice, spriteBatch, Content);
            bullets = new BulletList();
            asteroids = new Asteroid(Content, graphics);
            keyboard = new KeyBoardManager(Content);
            collision = new Collision(player, alien, bullets, asteroids);

            font = Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            keyboard.Update(Keyboard.GetState(), graphics, player, bullets);
            
            player.Update(gameTime);

            alien.Update(player, gameTime);

            bullets.Update(gameTime);

            int previousWave = wave;

            wave = asteroids.Update(wave, gameTime);

            if (wave != previousWave)
            {
                counter = 100;
                alien.alienSprite.position = Vector2.Zero;
                alien.LiveAlien();
            }

            lives = collision.Update(graphics, lives);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin();
            bullets.Draw(spriteBatch);
            asteroids.Draw(spriteBatch);
            player.Draw(spriteBatch);
            alien.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            for (int i = 0; i < lives; i++)
            {
                Sprite life = new Sprite(graphics, player.Image);
                life.Rotation = 0;
                life.size = 0.1f;
                life.position = new Vector2(10 + (i * 20), graphics.PreferredBackBufferHeight - 50);
                life.Draw(spriteBatch);
            }
            spriteBatch.End();

            if (counter > 0)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Wave " + wave, new Vector2(graphics.PreferredBackBufferWidth / 2 - 60, 200), Color.White * 0.7f);
                spriteBatch.End();
                counter--;
            }
            spriteBatch.Begin();
            if (lives < 0)
            {
                spriteBatch.DrawString(font, "GAME OVER!", new Vector2(200, graphics.PreferredBackBufferHeight / 2), Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
