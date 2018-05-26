using System;
using UnityEngine;

namespace Kurisu.Setting
{
    [Serializable]
    public class AppConfigSetting
    {
        /// <summary>
        /// 是否打开音乐
        /// </summary>
        public bool EnableMusic;

        /// <summary>
        /// 背景音乐音量大小
        /// </summary>
        public float BgmVolume;

        /// <summary>
        /// 是否打开声音特效
        /// </summary>
        public bool EnableSoundEffect;
    }
}
