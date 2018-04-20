using Kurisu.Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kurisu.Game.Entity.FlyingVehicle;
using Kurisu.Game.Entity.Factory;
using Kurisu.Game.Player.Component;

namespace Kurisu.Game.Player
{
    /// <summary>
    /// 玩家飞行逻辑
    /// </summary>
    public class FlyingPlayer
    {
        private string LOG_TAG = "FlyingPlayer";
        //=====================================================

        private PlayerData m_data = new PlayerData();

        private GameContext m_context;

        private PlayerGameState m_gameState = PlayerGameState.Normal;

        private GameObject m_container;

        private FlyingVehicle m_flyingVehicle;

        //----------------------------------------------------------------------
        /// <summary>
        /// 作为Player实体，有些功能需要用组合的方式去实现，从而需要有组件的概念
        /// 并不是每一个Entity都会有组件的
        /// </summary>
        private List<PlayerComponent> m_compoentList = new List<PlayerComponent>();

        //=====================================================

        public uint Id
        {
            get
            {
                return m_data.id;
            }
        }

        public PlayerData Data
        {
            get
            {
                return m_data;
            }
        }

        public PlayerGameState GameState
        {
            set
            {
                m_gameState = value;
            }
            get
            {
                return m_gameState;
            }
        }

        public ViewObject FlyingVehicleView
        {
            get
            {
                return m_flyingVehicle.View;
            }
        }

        //=====================================================


        public void Create(PlayerData data, TransformData transformData)
        {
            LOG_TAG = LOG_TAG + "[" + data.id + "]";

            m_data = data;
            m_context = GameLogicManager.Instance.Context;

            // 创建用来显示的视图
            m_container = new GameObject("FlyingPlayer" + data.id);

            // 创建玩家的飞行载具
            InitFlyingVehicle(transformData);
        }

        private void InitFlyingVehicle(TransformData transformData)
        {
            m_flyingVehicle = EntityFactory.InstanceEntity<FlyingVehicle>();
            m_flyingVehicle.Create(m_data, m_container.transform);

            m_flyingVehicle.onTriggerEvent += (string tag) => 
            {
                if (string.IsNullOrEmpty(tag))
                    return;

                switch (tag)
                {
                    case GameTagDefine.OBSTACLE:
                        m_gameState = PlayerGameState.Death;
                        break;
                    case GameTagDefine.END:
                        m_gameState = PlayerGameState.ArrivedAtTheEnd;
                        break;
                }
            };

            // 初始化飞行载具的transform信息
            TranslateData tarnslateData = (Transform body) => transformData.position;
            m_flyingVehicle.SaveTransData(tarnslateData);

            RotationData rotationData = (Transform body) => transformData.rotation;
            m_flyingVehicle.SaveTransData(rotationData);
        }
       

        #region 释放资源
        public void Release()
        {
            ReleaseCompoent();

            EntityFactory.ReleaseEntity(m_flyingVehicle);
            m_flyingVehicle = null;

            if (m_container != null)
            {
                GameObject.Destroy(m_container);
                m_container = null;
            }

            m_context = null;

            m_data = null;
        }

        private void ReleaseCompoent()
        {
            foreach (PlayerComponent component in m_compoentList)
            {
                component.Release();
            }
            m_compoentList.Clear();

        }
        #endregion




        public void InputVkey(GameVkey vkey, float arg)
        {
            bool isHandled = false;
            isHandled = isHandled || HandleVkey(vkey, arg);
        }

        public void EnterFrame(int frameIndex)
        {
            foreach (PlayerComponent component in m_compoentList)
            {
                component.EnterFrame(frameIndex);
            }

            m_flyingVehicle.Operational();
        }

        #region 响应玩家输入
        private bool HandleVkey(GameVkey vkey, float arg)
        {
            switch (vkey)
            {
                case GameVkey.MoveHorizontal:
                    m_flyingVehicle.RoteLR(arg);
                    break;
                case GameVkey.MoveVertical:
                    m_flyingVehicle.RoteUD(arg);
                    break;
                case GameVkey.SpeedUp:
                    m_flyingVehicle.MoveFB(arg);
                    break;
                default:
                    return false;
            }

            return true;
        }
        #endregion
    }
}
