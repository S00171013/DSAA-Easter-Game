using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Easter_Game
{
    class Scene
    {
        // Properties.
        public Texture2D Background { get; set; }
        public List<AnimatedSprite> SceneItems { get; set; }

        // Fields
        Vector2 origin = new Vector2(0, 0);

        public Scene(Texture2D backgroundIn, List<AnimatedSprite> itemsToRenderIn)
        {
            Background = backgroundIn;
            SceneItems = itemsToRenderIn;
        }

        public virtual void Draw(SpriteBatch spIn)
        {
            // Draw the background first.
            spIn.Draw(Background, origin, Color.White);

            // Render each of the scene's items on top of this background.
            foreach(AnimatedSprite item in SceneItems)
            {
                spIn.Draw(item.Image, item.Position, item.Tint);
            }
        }
    }
}
