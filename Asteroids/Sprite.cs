using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroids
{
    public class Sprite
    {
        private float rotation;
        private Vector2 center;
        public int distance;
        public Vector2 position;
        public Vector2 velocity;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                if (rotation < -MathHelper.TwoPi)
                {
                    rotation = MathHelper.TwoPi;
                }
                if (rotation > MathHelper.TwoPi)
                {
                    rotation = -MathHelper.TwoPi;
                }
            }
        }
        public Texture2D Image { get; set; }
        public bool IsLargeMeteor;
        public float size = 0.25f;
        public bool IsInvincible = false;
        public float invincibleTimeLeft = 0.0f;

        GraphicsDeviceManager graphics;

        public Sprite(GraphicsDeviceManager graphics, Texture2D image)
        {
            this.graphics = graphics;
            position = Vector2.Zero;
            position.X = graphics.PreferredBackBufferWidth / 2;
            position.Y = graphics.PreferredBackBufferHeight / 2;
            Image = image;
            center = Vector2.Zero;
            center.X = image.Width / 2;
            center.Y = image.Height / 2;
        }

        public void Update(GameTime gameTime)
        {
            position = position + velocity;

            if (position.X > graphics.PreferredBackBufferWidth)
            {
                position.X = 0;
            }
            if (position.X < 0)
            {
                position.X = graphics.PreferredBackBufferWidth;
            }
            if (position.Y > graphics.PreferredBackBufferHeight)
            {
                position.Y = 0;
            }
            if (position.Y < 0)
            {
                position.Y = graphics.PreferredBackBufferHeight;
            }

            if (invincibleTimeLeft > 0)
            {
                IsInvincible = true;
                invincibleTimeLeft -= gameTime.ElapsedGameTime.Milliseconds;
            }
            else 
                IsInvincible = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, position, null, Color.White, Rotation, center , size, SpriteEffects.None, 1.0f);
        }
    }
}
