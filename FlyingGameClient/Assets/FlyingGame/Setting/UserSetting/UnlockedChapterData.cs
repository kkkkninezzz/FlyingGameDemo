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
        public List<KeyValuePair<int, List<string>>> unlockedChapters;
    }
}

