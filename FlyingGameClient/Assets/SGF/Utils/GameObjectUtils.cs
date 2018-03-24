using UnityEngine;
using System.Collections;

namespace SGF.Utils
{
    public static class GameObjectUtils
    {
        public static void SetActiveRecursively(GameObject go, bool value)
        {
            go.SetActive(value);
        }
    }
}

