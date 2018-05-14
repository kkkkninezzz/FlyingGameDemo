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
        /// 关卡编号和小关卡的映射 视图模型
        /// </summary>
        private Dictionary<int, KeyValuePair<UIChapterNoBtnModel, List<UIMapItemModel>>> m_chapterModels;

        private bool isFirstOpen = false;

        protected override void OnOpen(object arg = null)
        {
            // 第一次打开时去主动调用一次ChapterModeBtn的click方法
            if (!isFirstOpen)
            {
                ChapterModeBtn.onClick.Invoke();
                isFirstOpen = true;
            }

            UpdateUnlockedChapters();
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
        private void SetDataToMapItem(GameObject mapItem, UIMapItemModel model)
        {
            UIMapItem uiMapItem = mapItem.GetComponent<UIMapItem>();
            if (uiMapItem == null)
            {
                this.LogError("this GameObject = {0} don't have UIMapItem Script!!!", mapItem);
                return;
            }

            uiMapItem.SetModel(model);
        }
        

        private void LoadDataToEndlessPanel()
        {
            List<MapConfigData> endlessMaps = MapModule.Instance.GetEndlessModeConfigs();

            foreach (MapConfigData data in endlessMaps)
            {
                GameObject mapItem = GameObject.Instantiate<GameObject>(EndlessMapItemPrefab);
                GameObjectUtils.SetParent(mapItem, EndlessMapContent);
                SetDataToMapItem(mapItem, new UIMapItemModel(data, true));
            }
        }

        private void LoadDataToChapterPanel()
        {
            List<ChapterMapConfigsData> chapterMaps = MapModule.Instance.GetChapterModeConfigs();

            if (chapterMaps == null || chapterMaps.Count <= 0)
            {
                return;
            }

            UserModule userModule = UserModule.Instance;

            List<Button> chapterSelectedBtns = new List<Button>(chapterMaps.Count);
            List<GameObject> smallChapterPanels = new List<GameObject>(chapterMaps.Count);
            
            m_chapterModels = new Dictionary<int, KeyValuePair<UIChapterNoBtnModel, List<UIMapItemModel>>>(chapterMaps.Count);

            foreach (ChapterMapConfigsData data in chapterMaps)
            {
                // 生成选择按钮
                GameObject selectedBtnGo = GameObject.Instantiate<GameObject>(ChapterSelectedBtnPrefab);
                selectedBtnGo.transform.SetParent(ChapterSelectedContent.transform);

                // 保存预制体的Button脚本
                UIChapterNoBtn selectedChapterNoBtn = selectedBtnGo.GetComponent<UIChapterNoBtn>();
                UIChapterNoBtnModel chapterNoBtnModel = new UIChapterNoBtnModel(data.chapterNo, userModule.IsChapterUnlocked(data.chapterNo));
                selectedChapterNoBtn.SetModel(chapterNoBtnModel);
                chapterSelectedBtns.Add(selectedBtnGo.GetComponent<Button>());

                // 保存章节按钮和小章节的映射
                List<UIMapItemModel> uiMapItemModels = new List<UIMapItemModel>(data.chapterConfigs.Count);
                KeyValuePair<UIChapterNoBtnModel, List<UIMapItemModel>> chapterModelKV = new KeyValuePair<UIChapterNoBtnModel, List<UIMapItemModel>>(chapterNoBtnModel, uiMapItemModels);
                m_chapterModels.Add(data.chapterNo, chapterModelKV);

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

                    // 保存小章节数据的model
                    UIMapItemModel mapItemModel = new UIMapItemModel(mapData, userModule.IsChapterUnlocked(data.chapterNo, mapData.no));
                    SetDataToMapItem(mapItem, mapItemModel);
                    uiMapItemModels.Add(mapItemModel);
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

        private void UpdateUnlockedChapters()
        {
            KeyValuePair<int, List<string>>[] lastUnlockedChapters = UserModule.Instance.GetAndClearLastUnlockedChapters();

            if (lastUnlockedChapters == null || lastUnlockedChapters.Length <= 0)
            {
                return;
            }

            foreach (KeyValuePair<int, List<string>> unlockedChapter in lastUnlockedChapters)
            {
                int chapterNo = unlockedChapter.Key;
                if (!m_chapterModels.ContainsKey(chapterNo))
                {
                    continue;
                }
                KeyValuePair<UIChapterNoBtnModel, List<UIMapItemModel>> chapterModel = m_chapterModels[chapterNo];
                // 更新关卡按钮的信息
                UIChapterNoBtnModel chapterNoBtnModel = chapterModel.Key;
                chapterNoBtnModel.IsUnlocked = true;
                
                // 更新小章节面板的信息
                foreach (string no in unlockedChapter.Value)
                {
                    foreach (UIMapItemModel mapItemModel in chapterModel.Value)
                    {
                        if (no.Equals(mapItemModel.MapConfigData.no))
                        {
                            mapItemModel.IsUnlocked = true;
                            break;
                        }
                    }
                }
                
            }
        }
    }
}

