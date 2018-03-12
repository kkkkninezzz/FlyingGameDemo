using UnityEngine;
using UnityEditor;
using System;

namespace Flight
{
    public class GameFlight : AbstractFlight
    {
        #region 变量区
        // 左右自动平衡
        private bool autoBalanceLR;

        // 上下自动平衡
        private bool autoBalanceUD;

        // 下落速度
        private float downSpeed;

        // 前进
        private bool isRun;
        #region 


        // 飞行控制
        public override void Operational()
        {
            if (curSpeed < config.TakeoffSpeed)
            {
                Move(-Vector3.up * Time.deltaTime * 10 * (1 - curSpeed / (config.TakeoffSpeed)));
                downSpeed = Mathf.Lerp(downSpeed, 0.1f, Time.deltaTime);
                //print("downSpeed" + downSpeed);
                RoteUD(downSpeed);
            }
            else
                downSpeed = 0;

            AutoBalance();

            if (!isRun)
            {
                if (curSpeed > config.MoveFBSpeed)
                    curSpeed = Mathf.Lerp(curSpeed, config.MoveFBSpeed, Time.deltaTime);
                else if (curSpeed > config.TakeoffSpeed)
                    curSpeed = UnityEngine.Random.Range(config.TakeoffSpeed, config.MoveFBSpeed);
            }
            Move(body.forward * curSpeed * Time.deltaTime);
            isRun = false;
        }

        // 左右移动
        public override void MoveLR(float speed)
        {
            if (isInStuntState)
                return;

            autoBalanceLR = false;

            Rote(speed * Vector3.up * config.RoteLRSpeed * Time.deltaTime * curSpeed / config.MoveFBSpeed);

            Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -config.AxisLR * speed), config.RoteLRSpeed * Time.deltaTime);
        }

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

