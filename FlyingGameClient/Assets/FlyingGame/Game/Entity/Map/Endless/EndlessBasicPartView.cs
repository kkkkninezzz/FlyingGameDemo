using UnityEngine;
using Kurisu.Game.Entity.Common;
using Kurisu.Game.Map;
using SGF;

namespace Kurisu.Game.Entity.Map
{
    /// <summary>
    /// 无尽模式下地图段视图
    /// </summary>
    public class EndlessBasicPartView : CommonView
    {
        /// <summary>
        /// 加载下一段地图
        /// </summary>
        public void LoadNextParts()
        {

            EndlessModeMapScript mapScript = FindEndlessModeMapScript();
            if (mapScript == null)
            {
                return;
            }

            mapScript.LoadNext = true;
        }

        private EndlessModeMapScript FindEndlessModeMapScript()
        {
            GameLogicManager gameManager = GameLogicManager.Instance;
            if (!gameManager.IsRunning)
            {
                return null;
            }

            GameMap gameMap = gameManager.GameMap;

            EndlessModeMapScript mapScript = gameMap.MapScript as EndlessModeMapScript;
            if (mapScript == null)
            {
                this.LogError("EndlessModeMapScript don't exists !!!");
                return null;
            }

            return mapScript;
        }

        /// <summary>
        /// 移除之前的地图段
        /// </summary>
        public void ReleaseLastParts()
        {
            EndlessModeMapScript mapScript = FindEndlessModeMapScript();
            if (mapScript == null)
            {
                return;
            }

            mapScript.ReleaseLast = true;
        }
    }
}
