using UnityEngine;
using System.Collections.Generic;
using Kurisu.Service.Core;
using Kurisu.Game.Map;
using Kurisu.Game;
using SGF.Utils;

namespace Kurisu.Service.Audio
{
    /// <summary>
    /// 用于管理声音
    /// </summary>
    public class AudioManager : ServiceModule<AudioManager>
    {
        private AudioManager() { }

        private BgmPlayer m_bgmPlayer;

        private AudioSource m_AudioSource;

        public void Init()
        {
            Camera ca = GameObject.FindObjectOfType<Camera>();
            if (ca != null)
            {
                m_bgmPlayer = ca.gameObject.GetComponent<BgmPlayer>();
                m_AudioSource = ca.gameObject.GetComponent<AudioSource>();
            }
            else
            {
                Debugger.LogError("AudioManager", "Create() Cannot Find Camera In Scene!");
                return;
            }
        }

        /// <summary>
        /// 自动播放bgms
        /// </summary>
        public void AutoPlayBgms(List<AudioClip> bgms)
        {
            if (m_bgmPlayer == null)
            {
                return;
            }
            m_bgmPlayer.Init(bgms);
            m_bgmPlayer.AutoPlay();
        }

        /// <summary>
        /// 释放bgms
        /// </summary>
        public void ReleaseBgms()
        {
            if (m_bgmPlayer == null)
            {
                return;
            }
            m_bgmPlayer.Release();
        }
    }
}

