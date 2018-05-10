using UnityEngine;
using System.Collections;
using Kurisu.Service.UIManager;
using UnityEngine.UI;
using System.Collections.Generic;
using Kurisu.Setting;
using Kurisu.Module.Map;

namespace Kurisu.UI.Chapter
{
    public class UIChapterPage : UIPage
    {
        /// <summary>
        /// 地图元素的prefab
        /// </summary>
        public GameObject MapItemPrefab;

        /// <summary>
        /// 关卡模式按钮
        /// </summary>
        public Button ChapterModeBtn;

        /// <summary>
        /// 用于展示关卡模式关卡的panel
        /// </summary>
        public Image ChapterPanel;

        /// <summary>
        /// 无尽模式按钮
        /// </summary>
        public Button EndlessModeBtn;

        /// <summary>
        /// 用于展示无尽模式地图的plane
        /// </summary>
        public Image EndlessPanel;

        /// <summary>
        /// 无尽模式的地图内容
        /// </summary>
        public GameObject EndlessMapContent;

        /// <summary>
        /// 是否是第一次打开关卡面板
        /// </summary>
        private bool m_isFirstOpenChapterPanel = true;

        /// <summary>
        /// 是否是第一次打开无尽面板
        /// </summary>
        private bool m_isFirstOpenEndlessPanel = true;

        //private List

        protected override void OnOpen(object arg = null)
        {
            // 被打开时去主动调用一次ChapterModeBtn的click方法
            ChapterModeBtn.onClick.Invoke();
        }

        public void OnChapterModeBtnClick()
        {
            ChapterPanel.gameObject.SetActive(true);
            EndlessPanel.gameObject.SetActive(false);

            if (m_isFirstOpenChapterPanel)
            {
                LoadDataToChapterPanel();
                m_isFirstOpenChapterPanel = false;
            }
        }

        public void OnEndlessModeBtnClick()
        {
            ChapterPanel.gameObject.SetActive(false);
            EndlessPanel.gameObject.SetActive(true);

            if (m_isFirstOpenEndlessPanel)
            {
                LoadDataToEndlessPanel();
                m_isFirstOpenEndlessPanel = false;
            }
        }

        private void LoadDataToEndlessPanel()
        {
            List<MapConfigData> endlessMaps = MapModule.Instance.GetEndlessModeConfigs();

            foreach (MapConfigData data in endlessMaps)
            {
                GameObject mapItem = GameObject.Instantiate<GameObject>(MapItemPrefab);
                mapItem.transform.parent = EndlessMapContent.transform;
            }
        }

        private void LoadDataToChapterPanel()
        {
            // TODO 加载数据
        }
    }
}

