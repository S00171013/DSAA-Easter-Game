using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Easter_Game
{
    class EndTower : AnimatedSprite
    {
        // Specific properties.
        public SpriteFont TowerIDFont { get; }
        public int TowerID { get; }   

        public EndTower(Texture2D image, Vector2 position, Color tint, int frameCountIn, SpriteFont towerIDFontIn, int towerIDIn) 
            : base(image, position, tint, frameCountIn)
        {
            TowerIDFont = towerIDFontIn;
            TowerID = towerIDIn;

            // Ensure the tower stays in the bounds. (Bounds hard-coded for now. It is the bounds of the playing field.)
            Position = Vector2.Clamp(Position, Vector2.Zero, new Vector2(2652, 1376));
        }

        public override void Draw(SpriteBatch sp, SpriteFont sf)
        {
            if (Visible)
            {
                sp.Draw(Image, Position, Tint);
                sp.DrawString(TowerIDFont, Convert.ToString(TowerID), Position, Color.White);
            }
        }
    }
}
