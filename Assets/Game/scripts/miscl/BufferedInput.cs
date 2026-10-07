using System;
using UnityEngine;

namespace Game.scripts
{
    [Serializable] public struct BufferedInput
    {
        public InputActionType Type;
        public float Timestamp;

        public BufferedInput(InputActionType type)
        {
            Type = type;
            Timestamp = Time.time;
        }
    }
}