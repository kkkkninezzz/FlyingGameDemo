using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

using Kurisu.Game.Data;
namespace Kurisu.Game.Player
{
    public class FlyingVehicleController : MonoBehaviour
    {
        #region 变量区
        /// <summary>
        /// 飞行载具对应的GameObject
        /// </summary>
        private Transform m_body;

        /// <summary>
        /// 飞行载具的配置
        /// </summary>
        private FlightConfig m_config;

        // 当前速度
        private float m_curSpeed;

        // 是否处于特技状态
        private bool m_isInStuntState;

        // 左右自动平衡
        private bool m_autoBalanceLR;

        // 上下自动平衡
        private bool m_autoBalanceUD;

        // 下落速度
        private float m_downSpeed;

        // 前进
        private bool m_isRun;
        #endregion

        public void Start()
        {
            m_body = this.transform;
        }

        #region 参数获取方法
        public FlightConfig Config
        {
            set
            {
                this.m_config = value;
            }
            get
            {
                return m_config;
            }
        }

        public float CurSpeed
        {
            get
            {
                return m_curSpeed;
            }
        }

        public bool IsInStuntState
        {
            get
            {
                return m_isInStuntState;
            }
        }
        #endregion

        #region 飞行控制
        // 飞行控制
        public void Operational()
        {
            if (m_curSpeed < m_config.TakeoffSpeed)
            {
                Move(-Vector3.up * Time.deltaTime * 10 * (1 - m_curSpeed / (m_config.TakeoffSpeed)));
                m_downSpeed = Mathf.Lerp(m_downSpeed, 0.1f, Time.deltaTime);
                RoteUD(m_downSpeed);
            }
            else
                m_downSpeed = 0;

            AutoBalance();

            if (!m_isRun)
            {
                if (m_curSpeed > m_config.MoveFBSpeed)
                    m_curSpeed = Mathf.Lerp(m_curSpeed, m_config.MoveFBSpeed, Time.deltaTime);
                else if (m_curSpeed > m_config.TakeoffSpeed)
                    m_curSpeed = UnityEngine.Random.Range(m_config.TakeoffSpeed, m_config.MoveFBSpeed);
            }
            Move(m_body.forward * m_curSpeed * Time.deltaTime);
            m_isRun = false;
        }

        // 左右移动（平移）
        public void MoveLR(float speed)
        {
            if (m_isInStuntState)
                return;

            Vector3 vector = m_body.right;
            vector.y = 0;

            Move(speed * vector * m_config.MoveLRSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed);

            Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, -m_config.AxisFB * speed), m_config.RoteLRSpeed * Time.deltaTime * 3);
        }

        // 上下旋转
        public void RoteUD(float speed)
        {
            //上下旋转
            //速度和角度
            if (m_isInStuntState)
                return;

            if (m_curSpeed < m_config.MoveFBSpeed / 3.6f && speed < 0)
                return;

            m_autoBalanceUD = false;
            Balance(Quaternion.Euler(m_config.AxisFB * speed, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed);
        }

        // 速度控制
        public void MoveFB(float speed)
        {
            m_isRun = true;

            m_curSpeed += speed * m_config.Acc * Time.deltaTime;
            m_curSpeed = Mathf.Clamp(m_curSpeed, 0, m_config.MaxSpeed);
        }

        // 左右旋转（可以进行转向）
        public void RoteLR(float speed)
        {
            //左右旋转
            if (m_isInStuntState)
                return;

            m_autoBalanceLR = false;
            Rote(speed * Vector3.up * m_config.RoteLRSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed);

            Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, -m_config.AxisLR * speed), m_config.RoteLRSpeed * Time.deltaTime);
        }

        // 自动平衡
        private void AutoBalance()
        {
            if (m_isInStuntState)
                return;

            if (m_autoBalanceLR)
                Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, 0), m_config.RoteLRSpeed * Time.deltaTime / 1.2f);

            if (m_autoBalanceUD)
                Balance(Quaternion.Euler(0, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime / 1.3f);

            m_autoBalanceLR = m_autoBalanceUD = true;
        }

        // 左右侧飞特技（Z轴）
        public void StuntLR(float axis)
        {
            if (m_isInStuntState)
                return;

            if (!m_isInStuntState)
            {
                m_isInStuntState = true;
                StartCoroutine(SLR(axis));
            }
        }

        IEnumerator SLR(float speed)
        {
            //这个特技是指侧飞，获取按下飞机的坐标和速度F1，计算出侧飞半径，
            //直到飞行角度和F1垂直的位置
            speed = (speed > 0 ? 1 : -1);
            Vector3 aim = m_body.right * (speed);
            aim.y = 0;
            while (Vector3.Dot(aim.normalized, m_body.forward.normalized) < 0.99f)
            {
                Rote(speed * Vector3.up * m_config.RoteLRSpeed * Time.deltaTime);

                Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, -85 * (speed)), m_config.RoteLRSpeed * Time.deltaTime * 3.8f);
                Balance(Quaternion.Euler(0, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime * 1.8f);
                yield return new WaitForFixedUpdate();
            }
            while ((m_body.eulerAngles.z > 15) && (m_body.eulerAngles.z < 180) || (m_body.eulerAngles.z < 345) && (m_body.eulerAngles.z > 270))
            {
                Balance(Quaternion.Euler(0, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime);
                Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, 0), m_config.RoteLRSpeed * Time.deltaTime * 3);
                yield return new WaitForFixedUpdate();
            }
            m_isInStuntState = false;
        }

        // 上下倾斜特技（X轴）
        public void StuntUD(float axis)
        {
            if (m_isInStuntState)
                return;

            if (!m_isInStuntState)
            {
                m_isInStuntState = true;
                StartCoroutine(SUD(axis));
            }
        }

        IEnumerator SUD(float speed)
        {
            //这个特技是指侧飞，获取按下飞机的坐标和速度F1，计算出侧飞半径，
            //直到飞行角度和F1垂直的位置
            speed = (speed > 0 ? 1 : -1);
            Vector3 aim = -m_body.forward;
            aim.y = 0;
            while (Vector3.Dot(aim.normalized, m_body.forward.normalized) < 0.8f)
            {
                Vector3 v = m_body.right;
                v.y = 0;
                Rote(m_body.right * Time.deltaTime * -90 * speed);
                Move(-Vector3.up * speed * Time.deltaTime * 10 * (m_curSpeed / (m_config.TakeoffSpeed)));
                //body.Rotate(Vector3.right * Time.deltaTime * -90,Space.Self);
                //Balance(Quaternion.Euler(body.eulerAngles.x, body.eulerAngles.y, 0), aircaft.RoteLRSpeed * Time.deltaTime*5);
                yield return new WaitForFixedUpdate();
            }
            while ((m_body.eulerAngles.z > 15) && (m_body.eulerAngles.z < 180) || (m_body.eulerAngles.z < 345) && (m_body.eulerAngles.z > 270))
            {
                Balance(Quaternion.Euler(0, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime);
                Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, 0), m_config.RoteLRSpeed * Time.deltaTime * 3);
                yield return new WaitForFixedUpdate();
            }
            m_isInStuntState = false;
        }
        // 移动
        private void Move(Vector3 vector)
        {
            m_body.Translate(vector, Space.World);
        }

        // 旋转
        private void Rote(Vector3 vector)
        {
            m_body.Rotate(vector, Space.World);
        }

        // 平衡
        private void Balance(Quaternion r, float speed)
        {
            m_body.rotation = Quaternion.RotateTowards(m_body.rotation,
                   r, speed);
        }
        #endregion

    }
}
