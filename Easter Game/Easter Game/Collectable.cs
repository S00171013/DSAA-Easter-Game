using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Easter_Game
{
    class Collectable : AnimatedSprite
    {
        protected Game myGame;

        public int ScoreWorth { get; set; }

        public Collectable(Game gameIn, int scoreWorthIn, Texture2D image, Vector2 position, Color tint, int frameCountIn) : base(image, position, tint, frameCountIn)
        {
            myGame = gameIn;
            ScoreWorth = scoreWorthIn;
        }

        //public virtual void Update(GameTime gtIn)
        //{
        //    // Update collision with the player.
        //    //CheckPlayerCollision();
        //}


        public void CheckPlayerCollision(Player other)
        {
            // If the collectable has collided with the player.
            if ((Bounds.Intersects(other.Bounds)))
            {
                // This collectable is no longer visible and it adds to the player's score.
                Visible = false;                    
            }           
        }
    }
}
