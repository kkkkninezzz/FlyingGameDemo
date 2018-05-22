using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Kurisu.Service.Audio
{
    /// <summary>
    /// 背景音乐播放
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class BgmPlayer : MonoBehaviour
    {
        /// <summary>
        /// 在播放结束时事件
        /// </summary>
        private delegate void OnAudioPlayEnd();

        [SerializeField]
        private AudioSource AudioSource;

        private List<AudioClip> m_bgms;

        private int m_curBgmIndex = 0;

        private IEnumerator m_curEnumerator;

        public void Init(List<AudioClip> bgms)
        {
            Release();
            this.m_bgms = bgms;
        }

        /// <summary>
        /// 释放资源，并进行复位
        /// </summary>
        public void Release()
        {
            if (m_bgms == null || m_bgms.Count <= 0)
            {
                return;
            }

            m_curBgmIndex = 0;
            if (m_curEnumerator != null)
            {
                StopCoroutine(m_curEnumerator);
            }

            // 停止播放音频
            AudioSource.Stop();
            AudioSource.loop = false;
            AudioSource.clip = null;

            m_bgms = null;
        }
        
        /// <summary>
        /// 自动播放音乐，并且进行循环播放，
        /// 如果有多首音乐，则会自动进行切换
        /// </summary>
        public void AutoPlay()
        {
            if (m_bgms == null || m_bgms.Count <= 0)
            {
                return;
            }

            if (m_bgms.Count == 1)  // 当只有一首bgm时，直接设置为循环播放
            {
                SetAndPlayAudioClip();
                AudioSource.loop = true;
            }
            else
            {
                LoopPlayByCoroutine();
            }
        }

        private void SetAndPlayAudioClip()
        {
            AudioSource.clip = m_bgms[m_curBgmIndex];
            AudioSource.Play();
        }

        #region 通过协程进行歌曲的循环播放
        /// <summary>
        /// 通过协程进行歌曲的循环播放
        /// </summary>
        private void LoopPlayByCoroutine()
        {
            m_curEnumerator = null;
            SetAndPlayAudioClip();
            m_curEnumerator = DelayedAudioPlay(AudioSource.clip.length, () =>
            {
                m_curBgmIndex++;
                if (m_curBgmIndex == m_bgms.Count)
                {
                    m_curBgmIndex = 0;
                }
                LoopPlayByCoroutine();
            });
            StartCoroutine(m_curEnumerator);
        }

        /// <summary>
        /// 等待播放结束
        /// </summary>
        private IEnumerator DelayedAudioPlay(float time, OnAudioPlayEnd endEvent = null)
        {
            yield return new WaitForSeconds(time);
            if (endEvent != null)
            {
                endEvent();
            }
        }
        #endregion


    }
}

