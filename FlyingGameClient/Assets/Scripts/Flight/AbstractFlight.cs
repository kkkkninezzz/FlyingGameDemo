using UnityEngine;
using System.Collections;

namespace Flight
{
    public abstract class AbstractFlight : MonoBehaviour
    {
        [HideInInspector]
        public Transform body;
        [HideInInspector]
        public FlightConfig config;

        // 当前速度
        public float curSpeed;

        // 是否处于特技状态
        protected bool isInStuntState;

        public void Start()
        {
            body = this.transform;

            config = new FlightConfig();
        }


        // 移动
        protected void Move(Vector3 vector)
        {
            body.Translate(vector, Space.World);
        }

        // 旋转
        protected void Rote(Vector3 vector)
        {
            body.Rotate(vector, Space.World);
        }

        // 平衡
        protected void Balance(Quaternion r, float speed)
        {
            body.rotation = Quaternion.RotateTowards(body.rotation,
                   r, speed);
        }

        // 飞行控制
        public abstract void Operational();

        // 左右移动
        public abstract void MoveLR(float speed);

        // 上下旋转
        public abstract void RoteUD(float speed);

        // 速度控制
        public abstract void MoveFB(float speed);

        // 左右旋转
        public abstract void RoteLR(float speed);

        // 自动平衡
        public abstract void AutoBalance();

        // 左右侧飞特技（Z轴）
        public abstract void StuntLR(float axis);

        // 上下倾斜特技（X轴）
        public abstract void StuntUD(float axis);
    }
}

