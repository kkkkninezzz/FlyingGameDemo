
using System;
using System.Collections.Generic;

using UnityEngine;

using Kurisu.Game.Entity.Factory;
using Kurisu.Game.Data;

namespace Kurisu.Game.Entity.FlyingVehicle
{
    public class FlyingVehicle : EntityObject
    {
        private FlyingVehicleData m_vehicleData;

        private PlayerData m_playerData;

        public event PlayerOnTriggerEvent onTriggerEvent;

        #region 飞行参数
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

        #region 操作队列
        /// <summary>
        /// 保存transData数据
        /// </summary>
        private Queue<TransData> m_transDataQueue = new Queue<TransData>();
       

        /// <summary>
        /// 是否有下个数据
        /// </summary>
        public bool HasNextTransData
        {
            get
            {
                return m_transDataQueue.Count > 0;
            }
        }

        /// <summary>
        /// 获取下个数据，如果队列为空，则返回null
        /// </summary>
        public TransData NextTransData
        {
            get
            {
                if (HasNextTransData)
                    return m_transDataQueue.Dequeue();

                throw new Exception("m_transDataQueue 为空，不能获取数据");
            }
        }

        private void SaveTransData(TranslateData data)
        {
            TransData transData = new TransData();
            transData.type = TransDataType.TranslateData;
            transData.translateData = data;

            m_transDataQueue.Enqueue(transData);
        }

        private void SaveTransData(RotateData data)
        {
            TransData transData = new TransData();
            transData.type = TransDataType.RotateData;
            transData.rotateData = data;

            m_transDataQueue.Enqueue(transData);
        }


        private void SaveTransData(RotationData data)
        {
            TransData transData = new TransData();
            transData.type = TransDataType.RotationData;
            transData.rotationData = data;

            m_transDataQueue.Enqueue(transData);
        }
        #endregion

        public void Create(PlayerData playerData, Transform container)
        {
            m_vehicleData = playerData.vehicleData;
            m_playerData = playerData;

            m_config = m_vehicleData.config;

            ViewFactory.CreateView("FlyingVehicle/FlyingVehicle" + m_vehicleData.id, "FlyingVehicle/FlyingVehicle0", this, container);
        }

        protected override void Release()
        {
            ViewFactory.ReleaseView(this);

            m_vehicleData = null;
            m_playerData = null;
            m_config = null;

            m_transDataQueue.Clear();
        }

        #region 飞行控制
        /// <summary>
        /// 基本控制
        /// </summary>
        public void Operational()
        {
            if (m_curSpeed < m_config.TakeoffSpeed)
            {
                // 保存移动操作
                // Move(-Vector3.up * Time.deltaTime * 10 * (1 - m_curSpeed / (m_config.TakeoffSpeed)));
                SaveTransData(GetTranslateData(-Vector3.up * Time.deltaTime * 10 * (1 - m_curSpeed / (m_config.TakeoffSpeed))));

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

            // Move(m_body.forward * m_curSpeed * Time.deltaTime);
            SaveTransData(GetTranslateDataByForward(m_curSpeed * Time.deltaTime));

            m_isRun = false;
        }

        /// <summary>
        /// 左右平移
        /// </summary>
        /// <param name="speed"></param>
        public void MoveLR(float speed)
        {
            if (m_isInStuntState)
                return;
         

            // Move(speed * vector * m_config.MoveLRSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed);
            SaveTransData(GetTranslateDataForMoveLR(speed * m_config.MoveLRSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed));

            // Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, -m_config.AxisFB * speed), m_config.RoteLRSpeed * Time.deltaTime * 3);

            SaveTransData(GetRotationDataForZ(-m_config.AxisFB * speed, m_config.RoteLRSpeed * Time.deltaTime * 3));

        }

        /// <summary>
        /// 上下旋转
        /// </summary>
        /// <param name="speed"></param>
        public void RoteUD(float speed)
        {
            //上下旋转
            //速度和角度
            if (m_isInStuntState)
                return;

            if (m_curSpeed < m_config.MoveFBSpeed / 3.6f && speed < 0)
                return;

            m_autoBalanceUD = false;
            //Balance(Quaternion.Euler(m_config.AxisFB * speed, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed);

            SaveTransData(GetRotationDataForX(m_config.AxisFB * speed, m_config.RoteFBSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed));
        }

        /// <summary>
        /// 速度控制
        /// </summary>
        /// <param name="speed"></param>
        public void MoveFB(float speed)
        {
            m_isRun = true;

            m_curSpeed += speed * m_config.Acc * Time.deltaTime;
            m_curSpeed = Mathf.Clamp(m_curSpeed, 0, m_config.MaxSpeed);
        }

        /// <summary>
        /// 左右旋转（可以进行转向）
        /// </summary>
        /// <param name="speed"></param>
        public void RoteLR(float speed)
        {
            //左右旋转
            if (m_isInStuntState)
                return;

            m_autoBalanceLR = false;
            // Rote(speed * Vector3.up * m_config.RoteLRSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed);
            SaveTransData(GetRotateData(speed * Vector3.up * m_config.RoteLRSpeed * Time.deltaTime * m_curSpeed / m_config.MoveFBSpeed));


            //Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, -m_config.AxisLR * speed), m_config.RoteLRSpeed * Time.deltaTime);
            SaveTransData(GetRotationDataForZ(-m_config.AxisLR * speed, m_config.RoteLRSpeed * Time.deltaTime));
        }

        /// <summary>
        /// 自动平衡
        /// </summary>
        private void AutoBalance()
        {
            if (m_isInStuntState)
                return;

            if (m_autoBalanceLR)
            {
                // Balance(Quaternion.Euler(m_body.eulerAngles.x, m_body.eulerAngles.y, 0), m_config.RoteLRSpeed * Time.deltaTime / 1.2f);

                SaveTransData(GetRotationDataForZ(0, m_config.RoteLRSpeed * Time.deltaTime / 1.2f));
            }
                

            if (m_autoBalanceUD)
            {
                // Balance(Quaternion.Euler(0, m_body.eulerAngles.y, m_body.eulerAngles.z), m_config.RoteFBSpeed * Time.deltaTime / 1.3f);

                SaveTransData(GetRotationDataForX(0, m_config.RoteFBSpeed * Time.deltaTime / 1.3f));
            }

            m_autoBalanceLR = m_autoBalanceUD = true;
        }
        #endregion

        #region 获取TransData
        private TranslateData GetTranslateData(Vector3 v3)
        {
            return (Transform tans) => v3;
        }

        private TranslateData GetTranslateDataByForward(float speed)
        {
            return (Transform tans) => tans.forward * speed;
        }

        private TranslateData GetTranslateDataForMoveLR(float speed)
        {
            return (Transform tans) => 
            {
                Vector3 vec = tans.right;
                vec.y = 0;

                return vec * speed;
            };
        }

        private RotationData GetRotationDataForZ(float z, float speed)
        {
            return (Transform trans) =>
            {
                Quaternion r = Quaternion.Euler(trans.eulerAngles.x, trans.eulerAngles.y, z);
                return Quaternion.RotateTowards(trans.rotation, r, speed);
            };
        }

        private RotationData GetRotationDataForX(float x, float speed)
        {
            return (Transform trans) =>
            {
                Quaternion r = Quaternion.Euler(x, trans.eulerAngles.y, trans.eulerAngles.z);
                return Quaternion.RotateTowards(trans.rotation, r, speed);
            };
        }

        private RotateData GetRotateData(Vector3 vec)
        {
            return (Transform tans) => vec;
        }

        #endregion

       

        /// <summary>
        /// 被碰撞
        /// </summary>
        /// <param name="tag"></param>
        public void OnTriggered(string tag)
        {
            if (onTriggerEvent != null)
                onTriggerEvent(tag);
        }
    }
}