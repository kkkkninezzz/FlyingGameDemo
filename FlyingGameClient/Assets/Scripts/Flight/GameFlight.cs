using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

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
        #endregion


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

        // 左右移动（平移）
        public override void MoveLR(float speed)
        {
            if (isInStuntState)
                return;

            Vector3 vector = body.right;
            vector.y = 0;

            Move(speed * vector * config.MoveLRSpeed * Time.deltaTime * curSpeed / config.MoveFBSpeed);

            Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -config.AxisFB * speed), config.RoteLRSpeed * Time.deltaTime * 3);
        }

        // 上下旋转
        public override void RoteUD(float speed)
        {
            //上下旋转
            //速度和角度
            if (isInStuntState)
                return;

            if (curSpeed < config.MoveFBSpeed / 3.6f && speed < 0)
                return;

            autoBalanceUD = false;
            Balance(Quaternion.Euler(config.AxisFB * speed, body.eulerAngles.y, body.eulerAngles.z), config.RoteFBSpeed * Time.deltaTime * curSpeed / config.MoveFBSpeed);
        }

        // 速度控制
        public override void MoveFB(float speed)
        {
            isRun = true;

            curSpeed += speed * config.Acc * Time.deltaTime;
            curSpeed = Mathf.Clamp(curSpeed, 0, config.MaxSpeed);
        }

        // 左右旋转（可以进行转向）
        public override void RoteLR(float speed)
        {
            //左右旋转
            if (isInStuntState)
                return;

            autoBalanceLR = false;
            Rote(speed * Vector3.up * config.RoteLRSpeed * Time.deltaTime * curSpeed / config.MoveFBSpeed);

            Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -config.AxisLR * speed), config.RoteLRSpeed * Time.deltaTime);
        }

        // 自动平衡
        public override void AutoBalance()
        {
            if (isInStuntState)
                return;

            if (autoBalanceLR)
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), config.RoteLRSpeed * Time.deltaTime / 1.2f);

            if (autoBalanceUD)
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), config.RoteFBSpeed * Time.deltaTime / 1.3f);

            autoBalanceLR = autoBalanceUD = true;
        }

        // 左右侧飞特技（Z轴）
        public override void StuntLR(float axis)
        {
            if (isInStuntState)
                return;

            if (!isInStuntState)
            {
                isInStuntState = true;
                StartCoroutine(SLR(axis));
            }
        }

        IEnumerator SLR(float speed)
        {
            //这个特技是指侧飞，获取按下飞机的坐标和速度F1，计算出侧飞半径，
            //直到飞行角度和F1垂直的位置
            speed = (speed > 0 ? 1 : -1);
            Vector3 aim = body.right * (speed);
            aim.y = 0;
            while (Vector3.Dot(aim.normalized, body.forward.normalized) < 0.99f)
            {
                Rote(speed * Vector3.up * config.RoteLRSpeed * Time.deltaTime);

                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -85 * (speed)), config.RoteLRSpeed * Time.deltaTime * 3.8f);
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), config.RoteFBSpeed * Time.deltaTime * 1.8f);
                yield return new WaitForFixedUpdate();
            }
            while ((body.eulerAngles.z > 15) && (body.eulerAngles.z < 180) || (body.eulerAngles.z < 345) && (body.eulerAngles.z > 270))
            {
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), config.RoteFBSpeed * Time.deltaTime);
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), config.RoteLRSpeed * Time.deltaTime * 3);
                yield return new WaitForFixedUpdate();
            }
            isInStuntState = false;
        }

        // 上下倾斜特技（X轴）
        public override void StuntUD(float axis)
        {
            if (isInStuntState)
                return;

            if (!isInStuntState)
            {
                isInStuntState = true;
                StartCoroutine(SUD(axis));
            }
        }

        IEnumerator SUD(float speed)
        {
            //这个特技是指侧飞，获取按下飞机的坐标和速度F1，计算出侧飞半径，
            //直到飞行角度和F1垂直的位置
            speed = (speed > 0 ? 1 : -1);
            Vector3 aim = -body.forward;
            aim.y = 0;
            while (Vector3.Dot(aim.normalized, body.forward.normalized) < 0.8f)
            {
                Vector3 v = body.right;
                v.y = 0;
                Rote(body.right * Time.deltaTime * -90 * speed);
                Move(-Vector3.up * speed * Time.deltaTime * 10 * (curSpeed / (config.TakeoffSpeed)));
                //body.Rotate(Vector3.right * Time.deltaTime * -90,Space.Self);
                //Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime*5);
                yield return new WaitForFixedUpdate();
            }
            while ((body.eulerAngles.z > 15) && (body.eulerAngles.z < 180) || (body.eulerAngles.z < 345) && (body.eulerAngles.z > 270))
            {
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), config.RoteFBSpeed * Time.deltaTime);
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), config.RoteLRSpeed * Time.deltaTime * 3);
                yield return new WaitForFixedUpdate();
            }
            isInStuntState = false;
        }
    }

}

