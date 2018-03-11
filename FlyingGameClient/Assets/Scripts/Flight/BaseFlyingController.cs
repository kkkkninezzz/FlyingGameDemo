using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flight
{
    public abstract class BaseFlyingController : MonoBehaviour
    {

        public abstract void MoveLR(float speed);//左右移动

        public abstract void RoteUD(float speed);//上下旋转

        public abstract void MoveFB(float speed);//速度控制

        public abstract void RoteLR(float speed);//左右旋转

        public abstract void Balance(Quaternion r, float speed);//转角

        public abstract void Operational();//飞行状态
    }
}

