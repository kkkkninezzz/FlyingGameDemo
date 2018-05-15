using UnityEngine;
using System.Collections;

namespace Kurisu.Game.Entity.Map
{
    public class LastPartReleaser : MonoBehaviour
    {
        public EndlessBasicPartView EndlessBasicPartView;

        private bool m_isTriggered = false;

        public void OnTriggerEnter(Collider other)
        {
            if (m_isTriggered || !GameTagDefine.PLAYER.Equals(other.tag))
            {
                return;
            }

            EndlessBasicPartView.ReleaseLastParts();
            m_isTriggered = true;
        }
    }
}

