using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Easter_Game
{
    class Enemy : AnimatedSprite
    {
        // Properties.
        public EndTower Target { get; }

        // Constant for the enemy's movement speed.
        const float MOVE_SPEED = 0.015f;

        public Enemy(Texture2D image, Vector2 position, Color tint, int frameCountIn, EndTower targetIn) : base(image, position, tint, frameCountIn)
        {
            // Set target for the enemy.
            Target = targetIn;
        }

        public void Update(GameTime gtIn)
        {
            // This method will have the enemy move towards its target.
            HeadToTower();
          
            // This method will check whether or not the player is within attacking distance of the enemy.


            // If the player is within attacking distance...

            // Shoot at the player.
        }

        public void HeadToTower()
        {
            Position = Vector2.Lerp(Position, Target.Position, MOVE_SPEED);

            // Update bounds.
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Image.Width / FrameCount, Image.Height);
        }

        public bool CheckTowerCollision(EndTower ctIn)
        {
            // Return true if the enemy has reached their destination.         
            if ((Bounds.Intersects(ctIn.Bounds)))
            {
                Tint = Color.Pink;
                
                return true;
            }

            // Otherwise, return false if the enemy is still on its way to the target.
            else
            {
                return false;
            }
        }
    }
}
