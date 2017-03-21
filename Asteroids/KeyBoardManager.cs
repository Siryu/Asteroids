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
    public class KeyBoardManager
    {
        private KeyboardState _oldKeyboard;
        private KeyboardState _currentKeyboard;
        private Texture2D bulletImage;

        public KeyBoardManager(ContentManager Content)
        {
            bulletImage = Content.Load<Texture2D>("bullet");
        }

        public void Update(KeyboardState state, GraphicsDeviceManager graphics, Sprite playerSprite, BulletList bullets)
        {
            _currentKeyboard = state;

            

            if (_currentKeyboard.IsKeyDown(Keys.Left))
            {
                playerSprite.Rotation -= 0.05f;
            }
            if (_currentKeyboard.IsKeyDown(Keys.Right))
            {
                playerSprite.Rotation += 0.05f;
            }
            if (WasJustPressed(Keys.Space))
            {                
                Sprite newBullet = new Sprite(graphics, bulletImage);

                newBullet.velocity = new Vector2((float)Math.Cos(playerSprite.Rotation - MathHelper.PiOver2),
                                                 (float)Math.Sin(playerSprite.Rotation - MathHelper.PiOver2)) * 4.0f
                                                 + playerSprite.velocity;
             
                newBullet.position = playerSprite.position + newBullet.velocity * 1.75f;
                newBullet.Rotation = playerSprite.Rotation;

                bullets.Add(newBullet);       
            }

            if (_currentKeyboard.IsKeyDown(Keys.Up))
            {
                playerSprite.velocity = new Vector2((float)Math.Cos(playerSprite.Rotation - MathHelper.PiOver2),
                                             (float)Math.Sin(playerSprite.Rotation - MathHelper.PiOver2)) / 4.0f
                                             + playerSprite.velocity;
            }
            //if (_currentKeyboard.IsKeyDown(Keys.Down))
            //{
            //    _movement += Vector2.UnitY * .5f;
            //}


            _oldKeyboard = _currentKeyboard;
        }

        bool WasJustPressed(Keys keyToCheck)
        {
            return _oldKeyboard.IsKeyUp(keyToCheck) && _currentKeyboard.IsKeyDown(keyToCheck);
        }
    }
}
