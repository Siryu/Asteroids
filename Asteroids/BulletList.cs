using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class BulletList
    {
        public List<Sprite> bullets;

        public BulletList()
        {
            bullets = new List<Sprite>();
        }

        public void Update(GameTime gameTime)
        {
            Sprite removeMe = null;
            foreach (Sprite b in bullets)
            {
                b.Update(gameTime);
                b.distance -= 1;

                if (b.distance <= 0)
                {
                    removeMe = b;
                }
            }
            if (removeMe != null)
            {
                bullets.Remove(removeMe);
            }
        }

        public void Add(Sprite newBullet)
        {
            newBullet.distance = 100;
            bullets.Add(newBullet);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite b in bullets)
            {
                b.Draw(spriteBatch);
            }
        }

    }
}
