using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class Asteroid
    {
        Random rand = new Random();
        GraphicsDeviceManager graphics;

        public List<Sprite> astroids;

        public Texture2D image;
        
        
        public Asteroid(ContentManager Content, GraphicsDeviceManager graphics)
        {
            astroids = new List<Sprite>();
            image = Content.Load<Texture2D>("asteroid");
            this.graphics = graphics;
        }

        public int Update(int wave, GameTime gameTime)
        {
            foreach (Sprite s in astroids)
            {
                s.Rotation += 0.05f;
                s.Update(gameTime);
            }

            if (astroids.Count == 0)
            {
                wave++;
                for (int i = 0; i < wave; i++)
                {
                    Add();
                }
            }

            return wave;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite s in astroids)
            {
                s.Draw(spriteBatch);
            }
        }

        private void Add()
        {
            Sprite newMeteor = new Sprite(graphics, image);
            newMeteor.IsLargeMeteor = true;

            newMeteor.velocity = new Vector2((float)Math.Cos((rand.NextDouble() * Math.PI)),
                                             (float)Math.Sin((rand.NextDouble() * Math.PI) - MathHelper.PiOver2));

            newMeteor.position = new Vector2(rand.Next(0, graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight));
            newMeteor.Rotation = 0.03f;

            
            this.astroids.Add(newMeteor);
        }
    }
}
