using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter_Game
{
    class SceneManager
    {
        // Will use this later.
        public List<Scene> Scenes { get; }

        public Scene Menu { get; }
        public Scene Gameplay { get; }
        public Stack<Scene> SceneStack { get; }


        public SceneManager(Scene menuSceneIn, Scene gameplaySceneIn, Stack<Scene> sceneStackIn)
        {
            Menu = menuSceneIn;
            Gameplay = gameplaySceneIn;

            SceneStack = sceneStackIn;
        }

        public Scene StartGame()
        {
            // Activate the gameplay scene.
            Scene activeScene = SceneStack.Pop();

            // Push the menu scene onto the stack. (The menu becomes null now for some reason, because of the pop() function?)
            SceneStack.Push(Menu);

            return activeScene;
        }
    }
}
