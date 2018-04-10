using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kurisu.Game.Player
{
    /// <summary>
    /// 飞行载具的碰撞脚本
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class FlyingVehicleTrigger : MonoBehaviour
    {
        private FlyingPlayer m_player;

        public FlyingPlayer Player
        {
            set
            {
                m_player = value;
            }
            get
            {
                return m_player;
            }
        }

        public void Start()
        {
            Collider colider = this.GetComponent<Collider>();

            // 将碰撞器设置为触发器
            if (colider != null && !colider.isTrigger)
            {
                colider.isTrigger = true;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (m_player == null)
                return;
            // 当玩家碰撞到墙体时认为玩家死亡
            m_player.GameState = PlayerGameState.Death;
        }
    }
}
