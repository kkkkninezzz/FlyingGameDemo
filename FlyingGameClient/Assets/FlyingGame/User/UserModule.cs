using UnityEngine;
using System.Collections.Generic;
using Kurisu.Service.Core;
using Kurisu.Setting.UserSetting;
using SGF;
using SGF.Utils;

namespace Kurisu.User
{
    public class UserModule : ServiceModule<UserModule>
    {
        private UserModule()
        {

        }

        private const string UnlockedChapterDataPath = "Assets/Resources/Config/User/UnlockedChapterData.json";

        /// <summary>
        /// 玩家已解锁的地图章节信息
        /// </summary>
        private UnlockedChapterData m_unlockedChapterData;

        /// <summary>
        /// 最近解锁的章节列表
        /// </summary>
        private List<KeyValuePair<int, List<string>>> m_lastUnlockedChapters = new List<KeyValuePair<int, List<string>>>();

        public void Init()
        {
            InitUnlockedChapterData();
        }

        #region 玩家已解锁的地图章节信息

        private void InitUnlockedChapterData()
        {
            this.Log("InitUnlockedChapterData() Path = " + UnlockedChapterDataPath);
            m_unlockedChapterData = JsonUtils.LoadJsonFromFile<UnlockedChapterData>(UnlockedChapterDataPath);

            if (m_unlockedChapterData == null)
            {
                this.LogWarning("Don't exists UnlockedChapterData in Path = {0}", UnlockedChapterDataPath);
                m_unlockedChapterData = new UnlockedChapterData();
                m_unlockedChapterData.unlockedChapters = new List<KeyValuePair<int, List<string>>>();
            }
        }

        /// <summary>
        /// 获取已经解锁的章节
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, List<string>>> GetUnlockedChapters()
        {
            return m_unlockedChapterData.unlockedChapters;
        }

        /// <summary>
        /// 判断该章节是否已经解锁
        /// </summary>
        /// <param name="chapterNo"></param>
        /// <returns></returns>
        public bool IsChapterUnlocked(int chapterNo)
        {
            foreach (var data in m_unlockedChapterData.unlockedChapters)
            {
                if (data.Key == chapterNo)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 判断该章节是否已经解锁
        /// </summary>
        /// <param name="chapterNo"></param>
        /// <param name="unlockedNo"></param>
        /// <returns></returns>
        public bool IsChapterUnlocked(int chapterNo, string unlockedNo)
        {
            foreach (var data in m_unlockedChapterData.unlockedChapters)
            {
                if (data.Key == chapterNo)
                {
                    List<string> noList = data.Value;
                    return noList.Contains(unlockedNo);
                }
            }

            return false;
        }

        /// <summary>
        /// 解锁章节
        /// </summary>
        /// <param name="chapterNo"></param>
        /// <param name="unlockedNo"></param>
        public void UnlockChapter(int chapterNo, string unlockedNo)
        {
            bool isAdd = false;
            foreach (var data in m_unlockedChapterData.unlockedChapters)
            {
                if (data.Key == chapterNo)
                {
                    List<string> noList = data.Value;
                    if (!noList.Contains(unlockedNo))
                    {
                        noList.Add(unlockedNo);
                    }
                    else
                    {
                        return;
                    }

                    isAdd = true;
                    break;
                }
            }

            if (!isAdd) // 说明不存在目标键值对
            {
                KeyValuePair<int, List<string>> kv = new KeyValuePair<int, List<string>>(chapterNo, new List<string>());
                kv.Value.Add(unlockedNo);
                m_unlockedChapterData.unlockedChapters.Add(kv);
            }

            AddToLastUnlockedChapters(chapterNo, unlockedNo);
            SaveUnlockedChapterData();
        }

        /// <summary>
        /// 添加到最近解锁数据中
        /// </summary>
        /// <param name="chapterNo"></param>
        /// <param name="unlockedNo"></param>
        private void AddToLastUnlockedChapters(int chapterNo, string unlockedNo)
        {
            foreach (var data in m_lastUnlockedChapters)
            {
                if (data.Key == chapterNo)
                {
                    List<string> noList = data.Value;
                    if (!noList.Contains(unlockedNo))
                    {
                        noList.Add(unlockedNo);
                    }

                    return;
                }
            }

            KeyValuePair<int, List<string>> kv = new KeyValuePair<int, List<string>>(chapterNo, new List<string>());
            kv.Value.Add(unlockedNo);
            m_lastUnlockedChapters.Add(kv);
        }

        /// <summary>
        /// 获取最近解锁的关卡
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<int, List<string>>> GetLastUnlockedChapters()
        {
            return m_lastUnlockedChapters;
        }

        /// <summary>
        /// 获取最近解锁的关卡，并且会清除掉列表
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<int, List<string>>[] GetAndClearLastUnlockedChapters()
        {
            KeyValuePair<int, List<string>>[] chapters = new KeyValuePair<int, List<string>>[m_lastUnlockedChapters.Count];
            m_lastUnlockedChapters.CopyTo(chapters);
            m_lastUnlockedChapters.Clear();

            return chapters;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveUnlockedChapterData()
        {
            this.Log("SaveUnlockedChapterData() Path = " + UnlockedChapterDataPath);
            JsonUtils.WriteDataToJsonFile(UnlockedChapterDataPath, m_unlockedChapterData);
        }
        #endregion
    }
}

