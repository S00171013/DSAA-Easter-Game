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

        // This property is specific to that of a menu scene.
        public List<MenuOption> MenuItems { get; }

        // The following properties are specific to a play scene.
        public List<Collectable> Collectables { get; }
        public Queue<Enemy> Enemies { get; }
        public List<StartTower> STowers { get; }
        public List<EndTower> ETowers { get; }

        public string SceneType { get; }

        // Fields
        Vector2 origin = new Vector2(0, 0);
        Player player;
        

        // Menu Scene Constructor.
        public Scene(Texture2D backgroundIn, List<MenuOption> menuOptionsIn)
        {
            Background = backgroundIn;
            MenuItems = menuOptionsIn;

            // Set scene type to "Menu".
            SceneType = "Menu";
        }

        // Play Scene Constructor.
        public Scene(Player playerIn, Texture2D backgroundIn, List<Collectable> collectablesIn, Queue<Enemy> enemiesIn, List<StartTower> sTowersIn, List<EndTower> eTowersIn)
        {
            player = playerIn;

            Background = backgroundIn;
            Collectables = collectablesIn;
            Enemies = enemiesIn;
            STowers = sTowersIn;
            ETowers = eTowersIn;

            // Set scene type to "Gameplay".
            SceneType = "Gameplay";
        }

        public virtual void Update(GameTime gtIn)
        {
            switch (SceneType)
            {
                case "Menu":
                    // Render the scene's menu items on top of this background.
                    foreach (MenuOption item in MenuItems)
                    {
                        item.Update(gtIn);
                    }
                    break;
                   
                    case "Gameplay":
                    // Update the scene's collectables.
                    foreach (Collectable c in Collectables)
                    {
                        c.CheckPlayerCollision(player);
                    }

                    // Update the scene's enemies.
                    foreach (Enemy e in Enemies)
                    {
                        e.Update(gtIn);
                    }
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spIn)
        {
            // Draw the background first.
            spIn.Draw(Background, origin, Color.White);

            switch(SceneType)
            {
                case "Menu":
                    // Render the scene's menu items on top of this background.
                    foreach (MenuOption item in MenuItems)
                    {
                        spIn.Draw(item.Image, item.Position, item.Tint);
                    }
                    break;

                // Will return to this option later.
                //case "Gameplay":
                //    // Render the scene's menu items on top of this background.
                //    foreach (Collectable c in Collectables)
                //    {
                //        spIn.Draw(c.Image, c.Position, c.Tint);
                //    }
                //    break;
            }            
        }
    }
}
