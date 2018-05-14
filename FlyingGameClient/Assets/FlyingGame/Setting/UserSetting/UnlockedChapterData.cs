using UnityEngine;
using System.Collections.Generic;
using System;

namespace Kurisu.Setting.UserSetting
{

    /// <summary>
    /// 已解锁的章节数据
    /// </summary>
    [Serializable]
    public class UnlockedChapterData
    {
        /// <summary>
        /// Key : 关卡编号
        /// Value : 该关卡编号下的小关卡编号
        /// </summary>
        public List<KeyValuePair<int, List<string>>> unlockedChapters;
    }
}

