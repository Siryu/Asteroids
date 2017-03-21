using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class Alien
    {
        public Sprite alienSprite;
        public bool IsAlive = true;

        public Alien(ContentManager Content, GraphicsDeviceManager graphics, Sprite playerSprite)
        {
            this.alienSprite = new Sprite(graphics, Content.Load<Texture2D>("alienShip"));
            alienSprite.position = new Vector2(100, 100);
        }

        public void Update(Sprite playerSprite, GameTime gameTime)
        {
            if(IsAlive)
            {
                if (alienSprite.position.X < playerSprite.position.X)
                {
                    alienSprite.position.X++;
                }
                else if (alienSprite.position.X > playerSprite.position.X)
                {
                    alienSprite.position.X--;
                }

                if (alienSprite.position.Y < playerSprite.position.Y)
                {
                    alienSprite.position.Y++;
                }
                else if(alienSprite.position.Y > playerSprite.position.Y)
                {
                    alienSprite.position.Y--;
                }

                alienSprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(IsAlive)
                alienSprite.Draw(spriteBatch);
        }

        public void KillAlien()
        {
            IsAlive = false;
        }

        public void LiveAlien()
        {
            IsAlive = true;
        }

    }
}
