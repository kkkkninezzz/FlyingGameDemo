using UnityEngine;
using System.Collections;
using Kurisu.Service.UIManager;
using UnityEngine.UI;
using System.Collections.Generic;
using Kurisu.Setting;
using Kurisu.Module.Map;
using Kurisu.UI.Ccommon;
using SGF.Utils;
using SGF;
using Kurisu.User;

namespace Kurisu.UI.Chapter
{
    public class UIChapterPage : UIPage
    {
        /// <summary>
        /// 无尽模式地图元素的prefab
        /// </summary>
        public GameObject EndlessMapItemPrefab;

        /// <summary>
        /// 关卡模式地图元素的prefab
        /// </summary>
        public GameObject ChapterMapItemPrefab;

        /// <summary>
        /// 关卡选择按钮的prefab
        /// </summary>
        public GameObject ChapterSelectedBtnPrefab;

        /// <summary>
        /// 小关卡面板的prefab
        /// </summary>
        public GameObject SmallChapterPanelPrefab;


        /// <summary>
        /// 关卡模式按钮
        /// </summary>
        public Button ChapterModeBtn;

        /// <summary>
        /// 用于展示关卡模式关卡的panel
        /// </summary>
        public Image ChapterPanel;

        /// <summary>
        /// 关卡选择按钮依附的内容部分
        /// </summary>
        public GameObject ChapterSelectedContent;

        /// <summary>
        /// 小关卡的根面板
        /// </summary>
        public Image SmallChapterRootPanel;

        /// <summary>
        /// 无尽模式按钮
        /// </summary>
        public Button EndlessModeBtn;

        /// <summary>
        /// 用于展示无尽模式地图的panel
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

        /// <summary>
        /// 暂存关卡地图的游戏对象
        /// </summary>
        private List<GameObject> m_chapterMapGameObjs = new List<GameObject>();

        private bool isFirstOpen = false;

        protected override void OnOpen(object arg = null)
        {
            // 第一次打开时去主动调用一次ChapterModeBtn的click方法
            if (!isFirstOpen)
            {
                ChapterModeBtn.onClick.Invoke();
                isFirstOpen = true;
            }
        }

        public void OnChapterModeBtnClick()
        {
            ChapterPanel.gameObject.SetActive(true);
            EndlessPanel.gameObject.SetActive(false);

            if (m_isFirstOpenChapterPanel)
            {
                LoadDataToChapterPanel();
                UIDynamicLabelBtnGroup chapterBtnGroup = ChapterSelectedContent.GetComponent<UIDynamicLabelBtnGroup>(); 
                if (chapterBtnGroup != null)
                {
                    chapterBtnGroup.ClickFirstBtn();
                }

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

        /// <summary>
        /// 将MapConfigData填充到mapItem中
        /// </summary>
        /// <param name="mapItem"></param>
        /// <param name="data"></param>
        private void SetDataToMapItem(GameObject mapItem, MapConfigData data, bool isUnlocked = true)
        {
            UIMapItem uiMapItem = mapItem.GetComponent<UIMapItem>();
            if (uiMapItem == null)
            {
                this.LogError("this GameObject = {0} don't have UIMapItem Script!!!", mapItem);
                return;
            }

            uiMapItem.SetData(data, isUnlocked);
        }
        

        private void LoadDataToEndlessPanel()
        {
            List<MapConfigData> endlessMaps = MapModule.Instance.GetEndlessModeConfigs();

            foreach (MapConfigData data in endlessMaps)
            {
                GameObject mapItem = GameObject.Instantiate<GameObject>(EndlessMapItemPrefab);
                mapItem.transform.parent = EndlessMapContent.transform;
                SetDataToMapItem(mapItem, data);
            }
        }

        private void LoadDataToChapterPanel()
        {
            List<ChapterMapConfigData> chapterMaps = MapModule.Instance.GetChapterModeConfigs();

            if (chapterMaps == null || chapterMaps.Count <= 0)
            {
                return;
            }

            UserModule userModule = UserModule.Instance;

            List<Button> chapterSelectedBtns = new List<Button>(chapterMaps.Count);
            List<GameObject> smallChapterPanels = new List<GameObject>(chapterMaps.Count);

            foreach (ChapterMapConfigData data in chapterMaps)
            {
                // 生成选择按钮
                GameObject selectedBtnGo = GameObject.Instantiate<GameObject>(ChapterSelectedBtnPrefab);
                selectedBtnGo.transform.SetParent(ChapterSelectedContent.transform);

                // 保存预制体的Button脚本
                Button selectedBtn = selectedBtnGo.GetComponent<Button>();
                UIUtils.SetButtonText(selectedBtn, data.chapterNo + "章");
                chapterSelectedBtns.Add(selectedBtn);


                // 生成小章节面板
                GameObject smallChapterPanelGo = GameObject.Instantiate<GameObject>(SmallChapterPanelPrefab);
                smallChapterPanelGo.transform.SetParent(SmallChapterRootPanel.transform);
                smallChapterPanels.Add(smallChapterPanelGo);

                // 获取小章节面板的滚动脚本
                UIScrollView smallChapterScrollView = GameObjectUtils.EnsureComponent<UIScrollView>(smallChapterPanelGo);
                
                // 添加数据到小章节面板
                foreach (MapConfigData mapData in data.chapterConfigs)
                {
                    GameObject mapItem = GameObject.Instantiate<GameObject>(ChapterMapItemPrefab);
                    smallChapterScrollView.AddChild(mapItem);
                    SetDataToMapItem(mapItem, mapData, userModule.IsChapterUnlocked(data.chapterNo, mapData.no));
                }
            }

            UIDynamicLabelBtnGroup chapterBtnGroup = GameObjectUtils.EnsureComponent<UIDynamicLabelBtnGroup>(ChapterSelectedContent);
            chapterBtnGroup.Init(chapterSelectedBtns, (int index) => 
            {
                UIDynamicPanelGroup panelGroup = SmallChapterRootPanel.GetComponent<UIDynamicPanelGroup>();
                if (panelGroup == null)
                {
                    return;
                }

                panelGroup.ActivePanel(index);
            });

            UIDynamicPanelGroup smallChapterPanelGroup = GameObjectUtils.EnsureComponent<UIDynamicPanelGroup>(SmallChapterRootPanel.gameObject);
            smallChapterPanelGroup.Init(smallChapterPanels);
        }
    }
}

