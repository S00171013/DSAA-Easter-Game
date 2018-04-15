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
        { get
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

        public Player Target { get; }

        // Constructor.
        public Camera(Game game, Vector2 startPos, Vector2 bound, Player targetIn) : base(game)
        {
            game.Components.Add(this);
            CamPos = startPos;
            _worldBound = bound;

            // Set player to follow.
            Target = targetIn;            
        }

        public override void Update(GameTime gameTime)
        {
            #region Moving the camera manually with the arrow keys.
            if (InputEngine.IsKeyHeld(Keys.Left))
                Move(new Vector2(-1, 0) * CameraSpeed, Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.Right))
                Move(new Vector2(1, 0) * CameraSpeed, Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.Down))
                Move(new Vector2(0, 1) * CameraSpeed, Game.GraphicsDevice.Viewport);
            if (InputEngine.IsKeyHeld(Keys.Up))
                Move(new Vector2(0, -1) * CameraSpeed, Game.GraphicsDevice.Viewport);
            #endregion

            #region Following the player.
            //Player p = (Player)Game.Services.GetService(typeof(Player));
            if (Target != null)
            {
                Follow(Target.Position, Game.GraphicsDevice.Viewport);

                //Make sure the player stays in the bounds
                Target.Position = Vector2.Clamp(Target.Position, Vector2.Zero,
                                                new Vector2(_worldBound.X - Target.Bounds.Width,
                                                            _worldBound.Y - Target.Bounds.Height));
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
