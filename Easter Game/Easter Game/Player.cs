using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Easter_Game
{
    class Player : AnimatedSprite
    {
        protected Game myGame;
        Viewport gameScreen;
        
        // Declare const int for the player's speed.
        const int PLAYER_SPEED = 5;
        
        Vector2 previousPosition;       

        // Constructor.
        public Player(Game gameIn, Texture2D image, Vector2 position, Color tint, int frameCount) : base(image, position, tint, frameCount)
        {
            myGame = gameIn;
                                             
            gameScreen = myGame.GraphicsDevice.Viewport;
        }

        public virtual void Update(GameTime gameTime)
        {
            // Set previous position, this is needed to handle collision.
            previousPosition = Position;            

            // Call the method that allows the player to move.
            HandleMovement(gameTime);                   
        }
                     
        public void HandleMovement(GameTime gameTime)
        {
            #region Handle movement
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                // Left             
                Move(new Vector2(-PLAYER_SPEED, 0));               

                UpdateAnimation(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // Right                
                Move(new Vector2(PLAYER_SPEED, 0));             

                UpdateAnimation(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                // Up                
                Move(new Vector2(0, -PLAYER_SPEED));                           
                UpdateAnimation(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                // Down              
                Move(new Vector2(0, PLAYER_SPEED));
               
                UpdateAnimation(gameTime);
            }
            #endregion                                  
        }
       
        // This method determines what will happen when the player collides with a solid object.
        public void Collision(AnimatedSprite other)
        {
            if (Bounds.Intersects(other.Bounds))
            {
                Position = previousPosition;
            }           
        }            
    }
}
