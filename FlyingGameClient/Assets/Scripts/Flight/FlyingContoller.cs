using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flight
{

    public class FlyingContoller : BaseFlyingController
    {
        public float MaxSpeed = 200;
        private bool IsRun;
        private float CurrentSpeed;

        // 下落速度
        private float downSpeed;

        // 特技状态
        private bool IsSing;

        // 是否处于左右自动平衡
        private bool IsLRB;

        // 是否处于前倾后倾的自动平衡
        private bool IsFBB;

        private FlightConfig flyingConfig;

        // 飞机的主体
        private Transform body;


        public override void MoveFB(float speed)//速度控制
        {
            IsRun = true;//主动控制打开
            CurrentSpeed += speed * flyingConfig.Acc * Time.deltaTime;//加/减速
            CurrentSpeed = Mathf.Clamp(CurrentSpeed, 0, flyingConfig.MaxSpeed);//控制速度在最大值范围内
        }
        public override void MoveLR(float speed)//水平移动飞机，飞机的侧飞
        {
            //左右移动
            if (IsSing) return;//如果在地面或者飞机处于特技状态
                                               //IsLRB = false;
            Vector3 vector = body.right;
            vector.y = 0;

            Move(speed * vector * flyingConfig.MoveLRSpeed * Time.deltaTime * CurrentSpeed / flyingConfig.MoveFBSpeed);//侧飞
            //Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -aircaft.AxisLR * speed), aircaft.RoteLRSpeed * Time.deltaTime);/ 旋转机身，实现侧飞的效果
            //print("MoveLR" + speed);
    }
        public override void Operational()//飞机的状态控制
        {

            if (CurrentSpeed < flyingConfig.TakeoffSpeed)//小于起飞速度
            {   
                //落下
                Move(-Vector3.up * Time.deltaTime * 10 * (1 - CurrentSpeed / (flyingConfig.TakeoffSpeed)));//失重下落
                downSpeed = Mathf.Lerp(downSpeed, 0.1f, Time.deltaTime);
                //print("downSpeed" + downSpeed);
                RoteUD(downSpeed);//机身前倾实现下落效果
            }
            else
            {
                downSpeed = 0;
            }
            Balance();//保持飞机的平衡
            if (!IsRun)
            {//保持飞机以正常速度飞行
                if (CurrentSpeed > flyingConfig.MoveFBSpeed)
                    CurrentSpeed = Mathf.Lerp(CurrentSpeed, flyingConfig.MoveFBSpeed, Time.deltaTime);

                else if (CurrentSpeed > flyingConfig.TakeoffSpeed)
                    CurrentSpeed = UnityEngine.Random.Range(flyingConfig.TakeoffSpeed, flyingConfig.MoveFBSpeed);
                
            }
            Move(body.forward * CurrentSpeed * Time.deltaTime);//调用飞行方法

        }
        public override void RoteLR(float speed)//飞机的转向
        {
            //左右旋转
            if (IsSing)
                return;

            IsLRB = false;
            Rote(speed * Vector3.up * flyingConfig.RoteLRSpeed * Time.deltaTime * CurrentSpeed / flyingConfig.MoveFBSpeed);

            Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, -flyingConfig.AxisLR * speed), flyingConfig.RoteLRSpeed * Time.deltaTime);
            //print("RoteLR" + speed);
        }


        public override void RoteUD(float speed)//飞机的转向
        {
            //上下旋转
            //速度和角度
            if (IsSing)
                return;

            if (CurrentSpeed < flyingConfig.MoveFBSpeed / 3.6f && speed < 0)
                return;

            IsFBB = false;
            Balance(Quaternion.Euler(flyingConfig.AxisFB * speed, body.eulerAngles.y, body.eulerAngles.z), flyingConfig.RoteFBSpeed * Time.deltaTime * CurrentSpeed / flyingConfig.MoveFBSpeed);
            //print("RoteUD" + speed);
        }

        public override void Balance(Quaternion r, float speed)
        {
            body.rotation = Quaternion.RotateTowards(body.rotation, r, speed);
        }

        public void Balance()//飞机的平衡方法，当无输入事件时，飞机自动平衡
        {
            if (IsSing) return;
            if (IsLRB)//z轴平衡（左右）
            {
                Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), flyingConfig.RoteLRSpeed * Time.deltaTime / 1.2f);
            }
            if (IsFBB)//x轴平衡（上下）
            {
                Balance(Quaternion.Euler(0, body.eulerAngles.y, body.eulerAngles.z), flyingConfig.RoteFBSpeed * Time.deltaTime / 1.3f);
            }
            IsLRB = true;//自动平衡打开
            IsFBB = true;//自动平衡打开
        }

        private void Move(Vector3 translation)
        {
            body.Translate(translation);
        }

        private void Rote(Vector3 rotation)
        {
            body.Rotate(rotation);
        }
    }
}
