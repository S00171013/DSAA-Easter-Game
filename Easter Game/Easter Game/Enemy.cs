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
        public Enemy(Texture2D image, Vector2 position, Color tint, int frameCountIn) : base(image, position, tint, frameCountIn)
        {
        }

        //public virtual void Update(GameTime gtIn)
        //{
        //    // This method will check whether or not the player is within attacking distance of the enemy.

        //    // If the player is within attacking distance...

        //    // Shoot at the player.
        //}

        public virtual void  
    }
}
