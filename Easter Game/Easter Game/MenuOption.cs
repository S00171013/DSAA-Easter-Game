using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Easter_Game
{   
    class MenuOption: AnimatedSprite
    {
        protected Game myGame;
             
        public Texture2D Texture { get; set; }      
        public bool Clicked { get; set; }        
             
        public MenuOption(Texture2D textureIn, Vector2 positionIn, Color tintIn, int fCountIn, Game gameIn, bool clickedStatusIn) : base(textureIn, positionIn, tintIn, fCountIn)
        {
            myGame = gameIn;    
            Clicked = clickedStatusIn;                            
        }       

        public void Update(GameTime gameTime)
        {
            // Declare variable to keep track of mouse state.
            var mouseState = Mouse.GetState();

            var mousePosition = new Point(mouseState.X, mouseState.Y);

            // If the user clicks on the menu option...
            if (mouseState.LeftButton == ButtonState.Pressed && Bounds.Contains(mousePosition))
            {
                // ...This option has been selected by the player.
                Clicked = true;          
            }

            // Test click.
            if(Clicked == true)
            {
                Tint = Color.Goldenrod;

                //Clicked = false;
            }

            else
            {
                Tint = Color.White;
            }
        }       
    }
}

