using UnityEngine;
using System;
using System.Collections;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 定义GameObject的Transform数据
    /// </summary>
    [Serializable]
    public class TransformData
    {

        public Vector3 position;

        public Quaternion rotation;

        public Vector3 scale;

        public TransformData()
        {
           
        }

        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

    }
}

