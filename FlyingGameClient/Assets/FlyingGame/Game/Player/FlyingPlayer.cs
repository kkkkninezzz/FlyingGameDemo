using Kurisu.Game.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kurisu.Game.Data;

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

        private GameObject m_flyingVehicle;

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
        
        //=====================================================


        public void Create(PlayerData data, Vector3 pos)
        {

        }

        public void Release()
        {

        }

        public void InputVkey(GameVkey vkey, float arg)
        {

        }

        public void EnterFrame(int frameIndex)
        {

        }
    }
}
