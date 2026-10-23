using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.scripts
{
    public static class Utils
    {
        private static WaitForSeconds _shortPause = new(0.4f);
        private static WaitForSeconds _medium = new(0.7f);
        private static WaitForSeconds _long = new(1f);
        
        public static IEnumerator ActionWithPause(PauseTime pausetime, Action action)
        {
            switch (pausetime)
            {
                case PauseTime.Short:
                    yield return _shortPause;
                    break;
                case PauseTime.Medium:
                    yield return _medium;
                    break;
                case PauseTime.Long:
                    yield return _long;
                    break;
            }
            action?.Invoke();
        }

        public static Vector3 GetRandomDir()
        {
            return new Vector3(
                Random.Range(-1f, 1f),
                0,
                Random.Range(-1f, 1f)
                
            ).normalized;
        }

        public static bool ToFlipACoin()
        {
            return Random.Range(0, 2) == 1;
        }
    }
}