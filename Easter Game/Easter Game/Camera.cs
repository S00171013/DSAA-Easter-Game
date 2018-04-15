using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easter_Game
{
    class Camera : GameComponent
    {
        // Fields.
        static Vector2 _camPos = Vector2.Zero;
        static Vector2 _worldBound;

        public float CameraSpeed = 5.0f;

        // Read-only properties.
        public static Matrix CurrentCameraTranslation
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(
                    -CamPos,
                    0));
            }
        }

        public static Vector2 CamPos
        {
            get
            {
                return _camPos;
            }

            set
            {
                _camPos = value;
            }
        }
       

        // Constructor.
        public Camera(Game game, Vector2 startPos, Vector2 bound) : base(game)
        {
            game.Components.Add(this);
            CamPos = startPos;
            _worldBound = bound;
           
        }

        public override void Update(GameTime gameTime)
        {          
            #region Follow the player.          
            Player p = (Player)Game.Services.GetService(typeof(Player));

            if (p != null)
            {
                Follow(p.Position, Game.GraphicsDevice.Viewport);

                //Make sure the player stays in the bounds
                p.Position = Vector2.Clamp(p.Position, Vector2.Zero,
                                                new Vector2(_worldBound.X - p.Bounds.Width,
                                                            _worldBound.Y - p.Bounds.Height));
            }
            #endregion

            base.Update(gameTime);
        }

        public void Move(Vector2 delta, Viewport v)
        {
            CamPos += delta;
            CamPos = Vector2.Clamp(CamPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }

        public static void Follow(Vector2 followPos, Viewport v)
        {
            _camPos = followPos - new Vector2(v.Width / 2, v.Height / 2);
            _camPos = Vector2.Clamp(_camPos, Vector2.Zero, _worldBound - new Vector2(v.Width, v.Height));
        }
    }
}
