using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Easter_Game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Declare the game's textures.
        // Menu.
        Texture2D menuBG, playOp, highScoreOp, exitOp, cursor;
        // Menu texture dictionary.
        Dictionary<string, Texture2D> menuTextures = new Dictionary<string, Texture2D>();
        // Gameplay.
        Texture2D player, cannonball, bKnight, startTower, endTower;
        // Gameplay texture dictionary.
        Dictionary<string, Texture2D> gameplayTextures = new Dictionary<string, Texture2D>();
        #endregion

        Player p1;

        #region Declare BGM and SFX
        // BGM
        Song playTrack;
        // SFX
        SoundEffect cannonFire, impact, backingTrack;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // Set resolution.
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {                    
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Load Textures.
            // Menu.
            menuTextures = Loader.ContentLoad<Texture2D>(Content, "Menu");
            // Gameplay.
            gameplayTextures = Loader.ContentLoad<Texture2D>(Content, "Default Assets");
            #endregion

            p1 = new Player(this, gameplayTextures["Game_0_Player"], new Vector2(100, 100), Color.White, 1);       

            // Load SFX.


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            p1.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            p1.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
