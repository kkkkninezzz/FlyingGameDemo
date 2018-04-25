using UnityEngine;
using System.Collections.Generic;
using Kurisu.Game.Data;

namespace Kurisu.Game.Map
{
    public abstract class MapScript : MonoBehaviour
    {
        public abstract void EnterFrame(int frameIndex);

        /// <summary>
        /// 获取出生位置
        /// </summary>
        /// <returns></returns>
        public abstract List<TransformData> GetBirthPoints();
    }
}

