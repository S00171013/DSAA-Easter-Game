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
        public EndTower Target { get; }

        public Enemy(Texture2D image, Vector2 position, Color tint, int frameCountIn, EndTower targetIn) : base(image, position, tint, frameCountIn)
        {
            // Set target for the enemy.
            Target = targetIn;
        }

        public void Update(GameTime gtIn)
        {
            // This method will have the enemy follow its target.
            // Lerp.


        //    // This method will check whether or not the player is within attacking distance of the enemy.

        //    // If the player is within attacking distance...

        //    // Shoot at the player.
        }
        
    }
}
