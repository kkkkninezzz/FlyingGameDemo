using UnityEngine;
using System.Collections;

namespace Kurisu.Game.Data
{
    /// <summary>
    /// 定义GameObject的Transform数据
    /// </summary>
    public struct TransformData
    {

        public Vector3 position;

        public Quaternion rotation;

        public Vector3 scale;

        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale) : this()
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

    }
}

