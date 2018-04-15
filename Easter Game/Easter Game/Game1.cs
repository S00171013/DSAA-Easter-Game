using System;
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
    class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Declare Textures.       
        // Menu texture dictionary.
        Dictionary<string, Texture2D> menuTextures = new Dictionary<string, Texture2D>();
        // Gameplay texture dictionary.
        Dictionary<string, Texture2D> gameplayTextures = new Dictionary<string, Texture2D>();
        // Declare a separate variable for the playing field.
        Texture2D playingField;
        #endregion     

        #region Declare BGM and SFX.
        // BGM.
        Song playTrack;
        // SFX dictionary.
        Dictionary<string, SoundEffect> soundFX = new Dictionary<string, SoundEffect>();
        #endregion

        #region Declare Game Objects
        // Menu
        List<MenuOption> menuOptions;
        MenuOption playOp, highScoreOp, exitOp;

        // Scenes
        List<Scene> gameScenes;
        Stack<Scene> sceneStack = new Stack<Scene>();
        Scene activeScene, menu, gameplay, highScore;
        SceneManager sceneManager;

        // Player
        public Player p1;

        // Collectables
        List<Collectable> collectables = new List<Collectable>();
        // Constants for number of collectables in the game.
        const int MINIMUM_COLLECTABLES = 10;
        const int MAXIMUM_COLLECTABLES = 20;
        // Constants for the min and max score value of a collectable.
        const int MINIMUM_SCORE = 1;
        const int MAXIMUM_SCORE = 10;

        // Random generator.
        Random randomG = new Random();

        // Start Towers
        List<StartTower> sTowers = new List<StartTower>();
        const int MINIMUM_TOWERS = 3;
        const int MAXIMUM_TOWERS = 6;

        // End Towers
        List<EndTower> eTowers = new List<EndTower>();

        // Declare the game font.
        SpriteFont gameFont;
        #endregion


        bool gameOver = false;
        int collectedCounter = 0;
        Texture2D gameOverScreen;

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
            // Set mouse to "visible".
            IsMouseVisible = true;

            new InputEngine(this);
                     
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
            playingField = gameplayTextures["Game_0_Background"];
            // Load game font.
            gameFont = Content.Load<SpriteFont>("gameFont");
            #endregion

            gameOverScreen = Content.Load<Texture2D>("Default Assets/Game_7_Complete");

            #region Load SFX and BGM.
            // SFX.
            soundFX = Loader.ContentLoad<SoundEffect>(Content, "Default SFX");


            // BGM.
            // Track composed by Yasunori Mitsuda (Chrono Trigger, 1995).
            playTrack = Content.Load<Song>("BGM/Guardia Castle ~Pride & Glory~");
            #endregion

            #region Create menu item objects. Must change positions to screen centre later.
            playOp = new MenuOption(menuTextures["Menu_1_Play"], new Vector2(500, 300), Color.White, 1, this, false);
            highScoreOp = new MenuOption(menuTextures["Menu_2_HighScore"], new Vector2(503, 430), Color.White, 1, this, false);
            exitOp = new MenuOption(menuTextures["Menu_3_Exit"], new Vector2(500, 560), Color.White, 1, this, false);
            List<MenuOption> menuOptions = new List<MenuOption>();

            // Add to option list.
            menuOptions.Add(playOp);
            menuOptions.Add(highScoreOp);
            menuOptions.Add(exitOp);
            #endregion

            #region Create scene objects.
            // Create main menu.
            Scene menu = new Scene(menuTextures["Menu_0_Background"], menuOptions);
            #endregion

            #region Create gameplay objects.
            // Create player.
            Services.AddService(p1 = new Player(this, gameplayTextures["Game_1_Player"], new Vector2(100, 100), Color.White, 1));

            // Camera set-up.
            new Camera(this, Vector2.Zero,
              new Vector2(playingField.Width, playingField.Height)
                );

            #region Create a random number of collectable coins, scattered in random locations.
            for (int i = 0; i < RandomInt(10, 20); i++)
            {
                #region Set a random position for the new collectable on the playing field.
                int xPos = RandomInt(
                    100,
                    playingField.Width-gameplayTextures["Game_6_Collectable"].Width);

                int yPos = RandomInt(
                    100,
                    playingField.Height-gameplayTextures["Game_6_Collectable"].Height);
                #endregion

                collectables.Add(new Collectable(this,
                    RandomInt(MINIMUM_SCORE, MAXIMUM_SCORE),
                    gameplayTextures["Game_6_Collectable"],
                    new Vector2(xPos, yPos),
                    Color.White,
                    6));
            }
            #endregion

            #region Create a random number of start towers, scattered in random locations.
            for (int i = 0; i < RandomInt(MINIMUM_TOWERS, MAXIMUM_TOWERS); i++)
            {
                #region Set a random position for the new start tower on the playing field.
                int xPos = RandomInt(
                    100,
                    playingField.Width-500);

                int yPos = RandomInt(
                    100,
                    playingField.Height-500);
                #endregion

                sTowers.Add(new StartTower(
                    this,
                    gameplayTextures["Game_4_StartT"],
                    new Vector2(xPos, yPos),
                    Color.White,
                    1,
                    gameplayTextures["Game_5_EndT"],
                    gameplayTextures["Game_3_BKnight"],
                    randomG,
                    gameFont));
            }
            #endregion
            #endregion

            // Create gameplay scene.
            Scene gameplay = new Scene(p1, gameplayTextures["Game_0_Background"],
                collectables,
                sTowers,
                eTowers);            

            // Set the initial active scene to that of the main menu.
            // For now it will be set to the gameplay screen.
            activeScene = gameplay;

            // Push the gameplay scene onto the screen stack.
            sceneStack.Push(gameplay);

            sceneManager = new SceneManager(menu, gameplay, sceneStack);
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
            if (gameOver == false)
            {

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                #region In progress.
                // Does not work.
                //if (playOp.Clicked == true && sceneStack.Peek() != menu)
                //{
                //    // Activate the gameplay scene.
                //    Scene activeScene = sceneStack.Pop();

                //    // Push the menu scene onto the stack. (The menu becomes null now for some reason, because of the pop() function?)
                //    sceneStack.Push(menu);            
                //}

                //if(playOp.Clicked == true && activeScene != gameplay)
                //{
                //    // Must fix.
                //    //sceneManager.StartGame();

                //    activeScene = gameplay;
                //}
                #endregion

                // Update the current scene.
                activeScene.Update(gameTime);

                // Update the player if gameplay has been initiated.
                if (activeScene.SceneType == "Gameplay")
                {
                    p1.Update(gameTime);
                }

                #region Quick, basic gameover code.
                //foreach (Collectable coin in collectables)
                //{
                //    if (coin.Visible == false)
                //    {
                //        collectedCounter++;
                //    }
                //}

                //if (collectedCounter == collectables.Count)
                //{
                //    gameOver = true;
                //}
                #endregion
            }
            base.Update(gameTime);           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp, null, null, null, Camera.CurrentCameraTranslation);

            if (gameOver == false)
            {
                // Draw the current scene.
                activeScene.Draw(spriteBatch);

                // Draw the player if gameplay has been initiated.
                if (activeScene.SceneType == "Gameplay")
                {
                    p1.Draw(spriteBatch);
                }
            }

            //else
            //{
            //    spriteBatch.Draw(gameOverScreen, new Vector2(0, 0), Color.White);
            //}
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // Quick method to get a random number within a specified range. Useful for collectables.
        public int RandomInt(int min, int max)
        {
            return randomG.Next(min, max);
        }
    }
}
