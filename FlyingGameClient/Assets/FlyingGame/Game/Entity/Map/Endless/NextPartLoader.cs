using UnityEngine;

namespace Kurisu.Game.Entity.Map
{
    public class NextPartLoader : MonoBehaviour
    {
        public EndlessBasicPartView EndlessBasicPartView;

        private bool m_isTriggered = false;

        public void OnTriggerEnter(Collider other)
        {
            if (m_isTriggered || !GameTagDefine.PLAYER.Equals(other.tag))
            {
                return;
            }

            EndlessBasicPartView.LoadNextParts();
            m_isTriggered = true;
        }
    }
}
