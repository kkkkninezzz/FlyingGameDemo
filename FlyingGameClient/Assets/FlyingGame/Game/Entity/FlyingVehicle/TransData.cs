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
    /// <summary>
    /// 移动数据
    /// </summary>
    /*
    public interface TranslateData
    {
       
    }
    */
    public delegate Vector3 TranslateData(Transform trans);

    public delegate Vector3 RotateData(Transform tans);
    /*
    public interface RotateData
    {
        Vector3 calculate(Transform trans);
    }
    */
    public delegate Quaternion RotationData(Transform tans);
    /*
    public interface RotationData
    {
        Quaternion calculate(Transform trans);
    }
    */
}
