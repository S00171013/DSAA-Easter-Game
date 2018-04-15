using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Easter_Game
{
    class StartTower : AnimatedSprite
    {
        protected Game myGame;
        Viewport gameScreen;

        // Properties.
        public Queue<Enemy> TowerEnemies { get; }
        public EndTower CorrespondingTower { get; }
        public SpriteFont TowerIDFont { get; }

        // Fields.
        Enemy dequeuedEnemy;
        Random randomG;
        int towerID = 0;

        const int MINIMUM_TOWER_DISTANCE = 500;

        Texture2D eTexture;



        public StartTower(Game gameIn, Texture2D image, Vector2 position, Color tint, int frameCountIn, Texture2D eTowerTextureIn, Texture2D enemyTextureIn, Random randomIn, SpriteFont displayIDFontIn)
            : base(image, position, tint, frameCountIn)
        {
            // Set Game.
            myGame = gameIn;

            // Get the gameScreen
            gameScreen = myGame.GraphicsDevice.Viewport;

            // Set random generator.
            randomG = randomIn;

            // Set the tower's display font.
            TowerIDFont = displayIDFontIn;

            // Set the tower's ID.
            towerID = RandomInt(0, 200);

            // Make sure the tower stays in the bounds. (Bounds hard-coded for now. It is the bounds of the playing field.)
            Position = Vector2.Clamp(Position, Vector2.Zero, new Vector2(2652, 1376));

            #region Set up Endtower. 
            CorrespondingTower = new EndTower(
                    eTowerTextureIn,
                    new Vector2(Position.X + RandomInt(100, MINIMUM_TOWER_DISTANCE), Position.Y + RandomInt(100, MINIMUM_TOWER_DISTANCE)),
                    Color.White,
                    1,
                    TowerIDFont,
                    towerID
                    );
            #endregion

            // Set up enemy queue.
            TowerEnemies = new Queue<Enemy>();

            #region Create a random number of enemies to add to the queue. Won't be doing anything with these yet.
            for (int i = 0; i < RandomInt(5, 10); i++)
            {
                TowerEnemies.Enqueue(new Enemy(
                    enemyTextureIn,
                    new Vector2(Position.X, Position.Y),
                    Color.White,
                    1,
                    CorrespondingTower));
            }

            eTexture = enemyTextureIn;

            dequeuedEnemy = TowerEnemies.Dequeue();
            #endregion
        }

        public void Update(GameTime gtIn)
        {
            // This method will check whether or not an endtower is within the viewport.
            if (CheckCTPosition(CorrespondingTower) == true)
            {

                // Update the dequeued enemy.
                if (dequeuedEnemy != null)
                {
                    dequeuedEnemy.Update(gtIn);

                    // Check tower collision.
                    dequeuedEnemy.CheckTowerCollision(CorrespondingTower);
                }

                // For now the only way to have the queue act as a proper conveyor belt is to create a new enemy and enqueue it each time.
                // Otherwise, when I would instead re-queue the dequeued enemy, the queue would eventually become empty, as if something was making each enemy null when they reached their target.
                // If the dequeued enemy comes into contact with the endtower...
                if (dequeuedEnemy.CheckTowerCollision(CorrespondingTower) == true)
                {         
                    // Add a new enemy to the queue.           
                    TowerEnemies.Enqueue(new Enemy(
                       eTexture,
                       new Vector2(Position.X, Position.Y),
                       Color.White,
                       1,
                       CorrespondingTower));

                    // Nullify the dequeued enemy.
                    dequeuedEnemy = null;

                    // Dequeue the next enemy.
                    DequeueEnemy();
                }
            }

            gameScreen = myGame.GraphicsDevice.Viewport;
        }

        public override void Draw(SpriteBatch spIn)
        {
            // Draw the start tower
            spIn.Draw(Image, Position, Tint);
            spIn.DrawString(TowerIDFont, Convert.ToString(towerID), Position, Color.White);

            // Draw the start tower's corresponding end tower.
            CorrespondingTower.Draw(spIn, TowerIDFont);

            // Draw the tower's currently dequeued enemy.
            if (dequeuedEnemy != null)
            {
                dequeuedEnemy.Draw(spIn);
            }
        }

        public void DequeueEnemy()
        {
            // ...Dequeue an enemy.
            dequeuedEnemy = TowerEnemies.Dequeue();
        }

        public bool CheckCTPosition(EndTower ctIn)
        {
            if (CorrespondingTower.Position.X < gameScreen.Width + CorrespondingTower.Image.Width || CorrespondingTower.Position.Y < gameScreen.Height + CorrespondingTower.Image.Height)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        // Quick method to get a random number within a specified range. Useful for collectables.
        public int RandomInt(int min, int max)
        {
            return randomG.Next(min, max);
        }
    }
}
