using Kurisu.Game.Data;
using Kurisu.Module;
using Kurisu.Module.Map;
using Kurisu.Module.Pve;
using Kurisu.Setting;
using SGF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kurisu.UI.Chapter
{
    public class UIMapItemDetailWindow : Kurisu.Service.UIManager.UIWindow
    {
        private const int ENTER_GAME_BTN = 1;

        public Text ChapterNoText;

        public Text ChpaterNameText;

        public Text GameModeText;

        public Text MapModeText;

        /// <summary>
        /// 地图数据
        /// </summary>
        private MapConfigData m_data;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);

            m_data = arg as MapConfigData;
            Debug.Log(m_data);
            ChapterNoText.text = m_data.no;
            ChpaterNameText.text = m_data.name;
            GameModeText.text = ConfigConstants.GetDescription(m_data.gameMode);
            MapModeText.text = ConfigConstants.GetDescription(m_data.mapMode);

            this.OnCloseEvent += closeArg =>
            {
                if (closeArg == null)
                    return;

                int btnIndex = (int)closeArg;
                switch (btnIndex)
                {
                    case ENTER_GAME_BTN:
                        PveModule pveModule = ModuleAPI.PveModule;
                        MapData mapData = MapModule.Instance.LoadModeMapData(m_data);
                        pveModule.StartGame(m_data.gameMode, mapData);
                        break;
                }
            };
        }

        protected override void OnClose(object arg = null)
        {
            base.OnClose(arg);
        }

        public void OnEnterGameBtnClick()
        {
            this.Close(ENTER_GAME_BTN);
        }
    }
}

