using UnityEngine;
using System.Collections.Generic;
using Kurisu.Game.Data;

namespace Kurisu.Game.Map
{
    public interface MapScript
    {
        void EnterFrame(int frameIndex);

        /// <summary>
        /// 获取出生位置
        /// </summary>
        /// <returns></returns>
        List<TransformData> GetBirthPoints();
    }
}

