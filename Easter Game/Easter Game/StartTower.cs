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

        // Fields.
        Enemy dequeuedEnemy;
        Random randomG;

        const int MINIMUM_TOWER_DISTANCE = 500;

        public StartTower(Game gameIn, Texture2D image, Vector2 position, Color tint, int frameCountIn, Texture2D eTowerTextureIn, Texture2D enemyTextureIn, Random randomIn) : base(image, position, tint, frameCountIn)
        {
            // Set Game.
            myGame = gameIn;

            // Get the gameScreen
            gameScreen = myGame.GraphicsDevice.Viewport;

            // Set random generator.
            randomG = randomIn;

            // Set up Endtower. 
            CorrespondingTower = new EndTower(
                    eTowerTextureIn,
                    new Vector2(Position.X + RandomInt(-MINIMUM_TOWER_DISTANCE, MINIMUM_TOWER_DISTANCE), Position.Y + RandomInt(-MINIMUM_TOWER_DISTANCE, MINIMUM_TOWER_DISTANCE)),
                    Color.White,
                    1
                    );

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
            #endregion
        }

        public void Update(GameTime gtIn)
        {
            // This method will check whether or not an endtower is within the viewport.

            // If there is an endtower nearby...

            // Dequeue an enemy and update it.

            // If the dequeued enemy comes into contact with the endtower...

            // Re-queue it.
        }

        // Quick method to get a random number within a specified range. Useful for collectables.
        public int RandomInt(int min, int max)
        {
            return randomG.Next(min, max);
        }
    }
}
