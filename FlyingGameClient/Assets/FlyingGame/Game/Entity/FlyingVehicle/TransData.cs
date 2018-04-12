using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
/// <summary>
/// 定义Transform的数据
/// </summary>
namespace Kurisu.Game.Entity.FlyingVehicle
{
    public enum TransDataType
    {
        TranslateData,

        RotateData,

        RotationData
    }
    public struct TransData
    {
        public TransDataType type;

        public TranslateData translateData;

        public RotateData rotateData;

        public RotationData rotationData;
    }
    public delegate Vector3 TranslateData(Transform trans);

    public delegate Vector3 RotateData(Transform tans);

    public delegate Quaternion RotationData(Transform tans);
    
}
