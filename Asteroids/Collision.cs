using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public class Collision
    {
        Sprite playerSprite;
        Alien alien;
        BulletList bullets;
        Asteroid asteroids;

        public Collision(Sprite playerSprite, Alien alien, BulletList bullets, Asteroid asteroids)
        {
            this.playerSprite = playerSprite;
            this.alien = alien;
            this.bullets = bullets;
            this.asteroids = asteroids;
        }

        public int Update(GraphicsDeviceManager graphics, int lives)
        {
            lives = checkIfSomethingHitShip(lives, graphics);
            checkForAsteroidShot(graphics);
            checkForAlienShot();

            return lives;
        }

        private int checkIfSomethingHitShip(int lives, GraphicsDeviceManager graphics)
        {
            if (!playerSprite.IsInvincible)
            {
                if (alien.IsAlive && Vector2.DistanceSquared(alien.alienSprite.position, playerSprite.position) <
                            (playerSprite.Image.Height + playerSprite.Image.Width * playerSprite.size))
                {
                    lives -= 1;
                    playerSprite.invincibleTimeLeft = 2000;
                    playerSprite.position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
                    playerSprite.Rotation = 0;
                    playerSprite.velocity = Vector2.Zero;
                }

                foreach (Sprite a in asteroids.astroids)
                {
                    if (Vector2.DistanceSquared(a.position, playerSprite.position) <
                            (playerSprite.Image.Height + playerSprite.Image.Width * playerSprite.size + 10))
                    {
                        lives -= 1;
                        playerSprite.invincibleTimeLeft = 2000;
                        playerSprite.position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
                        playerSprite.Rotation = 0;
                        playerSprite.velocity = Vector2.Zero;
                    }
                }
            }

            return lives;
        }

        private void checkForAlienShot()
        {
            Sprite removeBullet = null;
            Sprite removeAlien = null;

            if (alien.IsAlive)
            {
                for (int j = 0; j < bullets.bullets.Count; j++)
                {
                    if (Vector2.DistanceSquared(bullets.bullets[j].position, alien.alienSprite.position) <
                        (alien.alienSprite.Image.Width + alien.alienSprite.Image.Height * alien.alienSprite.size))
                    {
                        removeBullet = bullets.bullets[j];

                        removeAlien = alien.alienSprite;
                    }
                }
                bullets.bullets.Remove(removeBullet);

                if (removeAlien != null)
                {
                    alien.KillAlien();
                }
            }
        }

        private void checkForAsteroidShot(GraphicsDeviceManager graphics)
        {
            //bullet-asteroid collision check
            Sprite removeBullet = null;
            Sprite removeAsteroid = null;
                    
            for (int i = 0; i < asteroids.astroids.Count; i++)
            {
                for (int j = 0; j < bullets.bullets.Count; j++)
                {

                    if (Vector2.DistanceSquared(bullets.bullets[j].position, asteroids.astroids[i].position) <
                        (asteroids.image.Width + asteroids.image.Height * asteroids.astroids[i].size))
                    {
                        removeBullet = bullets.bullets[j];
            
                        removeAsteroid = asteroids.astroids[i];
                    }
                }
                bullets.bullets.Remove(removeBullet);
            }

            if (removeAsteroid != null && removeAsteroid.IsLargeMeteor)
            {
                Sprite smallMeteor = new Sprite(graphics, asteroids.image);
                smallMeteor.IsLargeMeteor = false;
                smallMeteor.Rotation = removeAsteroid.Rotation;
                smallMeteor.velocity = removeAsteroid.velocity;
                smallMeteor.velocity.X -= 0.25f;
                smallMeteor.velocity.Y += 0.25f;
                smallMeteor.position = removeAsteroid.position;
                smallMeteor.size = 0.15f;

                Sprite smallMeteor1 = new Sprite(graphics, asteroids.image);
                smallMeteor1.IsLargeMeteor = false;
                smallMeteor1.velocity = removeAsteroid.velocity;
                smallMeteor1.Rotation = removeAsteroid.Rotation;
                smallMeteor1.velocity.Y += 0.25f;
                smallMeteor1.velocity.X += 0.25f;
                smallMeteor1.position = removeAsteroid.position;
                smallMeteor1.size = 0.15f;

                asteroids.astroids.Add(smallMeteor);
                asteroids.astroids.Add(smallMeteor1);
            }

            asteroids.astroids.Remove(removeAsteroid);
        }
    }
}
