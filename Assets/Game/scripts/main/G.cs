using UnityEngine;

namespace Game.scripts
{
    public class Game : MonoBehaviour
    {
        public static Game G;

        public void Awake()
        {
            if (G != null)
                return;

            G = this;
            DontDestroyOnLoad(G);
        }

        public void StartGame()
        {
            CharacterStats.Stats.InitializeStartStats();
        }
    }
}