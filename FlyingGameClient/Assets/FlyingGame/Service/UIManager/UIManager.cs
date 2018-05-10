using UnityEngine;
using System.Collections;

using Kurisu.Service.Core;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

using SGF;

namespace Kurisu.Service.UIManager
{
    public class UIManager : ServiceModule<UIManager>
    {
        public const string LOG_TAG = "UIManager";

        public static string MainScene = "Main";

        public static string MainPage = "UIMainPage";

        class UIPageTrack
        {
            public string name;
            public string scene;
        }

        private Stack<UIPageTrack> m_pageTrackStack;
        private UIPageTrack m_curPage;
        private Action<string> sceneLoaded;
        private List<UIPanel> m_loadedPanelList;

        private UIManager()
        {
            m_pageTrackStack = new Stack<UIPageTrack>();
            m_loadedPanelList = new List<UIPanel>();
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="uiResRoot">UI资源的根目录，默认为"ui/"</param>
        public void Init(string uiResRoot)
        {
            UIRes.UIResRoot = uiResRoot;

            // 监听UnitySecen加载事件
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (sceneLoaded != null)
                    sceneLoaded(scene.name);
            };
        }

        /// <summary>
        /// 加载UI，如果UIRoot下一级有了，直接返回UIRoot下的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        private T Load<T>(string name) where T : UIPanel
        {
            T ui = UIRoot.Find<T>(name.Replace('/', '_'));

            if (ui == null)
            {
                GameObject original = UIRes.LoadPrefab(name);
                if (original != null)
                {
                    GameObject go = GameObject.Instantiate(original);
                    ui = go.GetComponent<T>();
                    if (ui != null)
                    {
                        go.name = name.Replace('/', '_');
                        UIRoot.AddChild(ui);
                    }
                    else
                    {
                        this.LogError("Load() Prefab中没有对应UI脚本: {0}", name);
                    }
                }
                else
                {
                    this.LogError("Load() Res Not Found: {0}", name);
                }
            }

            if (ui != null && !m_loadedPanelList.Contains(ui))
                m_loadedPanelList.Add(ui);

            return ui;
        }

        private T Open<T>(string name, object arg = null) where T : UIPanel
        {
            T ui = Load<T>(name);

            if (ui != null)
            {
                ui.Open(arg);
            }
            else
            {
                this.LogError("Open() Failed! Name: {0}", name);
            }

            return ui;
        }

        /// <summary>
        /// 关闭所有加载的UI
        /// </summary>
        private void CloseAllLoadedPanels()
        {
            for (int i = 0; i < m_loadedPanelList.Count; i++)
            {
                if (m_loadedPanelList[i].IsOpen)
                    m_loadedPanelList[i].Close();
            }

            // m_loadedPanelList.Clear();
        }

        /// <summary>
        /// 进入主Page
        /// 并清除Page堆栈
        /// </summary>
        public void EnterMainPage()
        {
            m_pageTrackStack.Clear();
            OpenPageWorker(MainScene, MainPage, null);
        }

        //========================================================================
        #region UIPage管理
        /// <summary>
        /// 打开Page
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="page"></param>
        /// <param name="arg"></param>
        public void OpenPage(string scene, string page, object arg = null)
        {
            this.Log("OpenPage() scene: {0}, page: {1}, arg: {2} ", scene, page, arg);

            if (m_curPage != null)
            {
                m_pageTrackStack.Push(m_curPage);
            }
            OpenPageWorker(scene, page, null);

        }

        /// <summary>
        /// 在主场景打开页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="arg"></param>
        public void OpenPage(string page, object arg = null)
        {
            OpenPage(MainScene, page, arg);
        }


        /// <summary>
        /// 返回上一个Page
        /// </summary>
        public void GoBackPage()
        {
            this.Log("GoBackPage()");

            if (m_pageTrackStack.Count > 0)
            {
                var track = m_pageTrackStack.Pop();
                OpenPageWorker(track.scene, track.name, null);
            }
            else
            {
                EnterMainPage();
            }
        }

        private void OpenPageWorker(string scene, string page, object arg)
        {
            this.Log("OpenPageWorker() scene: {0}, page: {1}, arg: {2} ", scene, page, arg);

            string oldScene = SceneManager.GetActiveScene().name;

            m_curPage = new UIPageTrack();
            m_curPage.scene = scene;
            m_curPage.name = page;

            // 关闭当前Page打开的所有的UI
            CloseAllLoadedPanels();

            if (oldScene == scene)
            {
                Open<UIPage>(page, arg);
            }
            else
            {
                sceneLoaded = (sceneName) =>
                {
                    if (sceneName != scene)
                        return;

                    sceneLoaded = null;
                    Open<UIPage>(page, arg);
                };

                SceneManager.LoadScene(scene);
            }
        }
        #endregion

        //========================================================================

        #region UIWindow管理

        public UIWindow OpenWindow(string name, object arg = null)
        {
            return Open<UIWindow>(name, arg);
        }

        public T OpenWindow<T>(object arg = null) where T : UIWindow
        {
            return Open<T>(typeof(T).Name, arg);
        }

        #endregion

        //========================================================================

        #region UIWidget管理

        public UIWidget OpenWidget(string name, object arg = null)
        {
            return Open<UIWidget>(name, arg);
        }

        public T OpenWidget<T>(object arg = null) where T : UIWidget
        {
            return Open<T>(typeof(T).Name, arg);
        }

        #endregion
    }
}

