using UnityEngine;

namespace Game.scripts
{
    public class Bootstraper : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            var go = Instantiate(new GameObject("G"));
            var g = go.AddComponent<Game>();
            g.StartGame();
        }
    }
}