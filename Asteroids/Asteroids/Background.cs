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
    public class Background
    {
        public Texture2D background;

        public Background(GraphicsDevice graphics, SpriteBatch spriteBatch, ContentManager Content)
        {
            background = CreateBackground(graphics, spriteBatch, Content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }

        private Texture2D CreateBackground(GraphicsDevice graphics, SpriteBatch spriteBatch, ContentManager Content)
        {

            RenderTarget2D target = new RenderTarget2D(graphics, 2048, 2048);
            //tell the GraphicsDevice we want to render to the gamesMenu rendertarget (an in-memory buffer)
            graphics.SetRenderTarget(target);

            //clear the background
            graphics.Clear(Color.Transparent);

            //begin drawing
            spriteBatch.Begin();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("starBackground"), new Vector2(x * 256, y * 256), Color.White);
                }
            }

            spriteBatch.End();
            //reset the GraphicsDevice to draw on the backbuffer (directly to the backbuffer)
            graphics.SetRenderTarget(null);

            return target;
        }
    }
}
