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

        public Vector3Data position;

        public QuaternionData rotation;

        public Vector3Data scale;

        public TransformData()
        {
           
        }

        public TransformData(Vector3Data position, QuaternionData rotation, Vector3Data scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

    }
}

