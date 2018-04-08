using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SGF.Utils;

namespace Kurisu.Game
{
    /// <summary>
    /// 游戏内的摄像机
    /// </summary>
    public class GameCamera : MonoBehaviour
    {
        public static GameCamera CurCameraScript;

        public static Camera MainCamera;

        public static uint FocusPlayerId = 0;

        public static void Create()
        {
            Camera ca = GameObject.FindObjectOfType<Camera>();
            if (ca != null)
            {
                GameObjectUtils.EnsureComponent<GameCamera>(ca.gameObject);
            } else
            {
                Debugger.LogError("GameCamera", "Create() Cannot Find Camera In Scene!");
            }
        }

        public static void Release()
        {
            if (CurCameraScript != null)
            {
                GameObject.Destroy(CurCameraScript);
                CurCameraScript = null;
            }
        }
    }

}
