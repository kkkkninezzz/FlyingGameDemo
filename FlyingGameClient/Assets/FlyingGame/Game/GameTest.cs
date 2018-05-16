using UnityEngine;
using System.Collections.Generic;

using Kurisu.Game.Data;

using SGF.Unity;
using Kurisu.Module.Pve;
using Kurisu.Service.Core;
using Kurisu.Module;
using System;
using Kurisu.Module.Map;
using SGF;

namespace Kurisu.Game
{
    public delegate void MonoUpdaterEvent();
    public class GameTest : MonoBehaviour
    {
        public event MonoUpdaterEvent action;

        // Use this for initialization
        void Start()
        {

            /*
            GameParam param = new GameParam();
            param.mode = GameMode.EndlessPVE;
            param.mapData = MapModule.Instance.GetEndlessModeMapData("1");

            GameLogicManager game = GameLogicManager.Instance;
            game.CreateGame(param);

            PlayerData playerData = new PlayerData();
            playerData.id = 1;
            playerData.vehicleData.id = 0;
            playerData.vehicleData.config = new FlightConfig();

            game.RegPlayerData(playerData);
            */
            /*
            PveModule pveModule = ModuleAPI.PveModule;
            pveModule.StartGame(GameMode.EndlessPVE, MapModule.Instance.GetEndlessModeMapData("1"));
            */

            /*
            action += test1;
            action += test2;
            action += test3;
            */
            
        }

        public void OnEnable()
        {
            Debug.Log("OnEnable");
        }

        // Update is called once per frame
        void Update()
        {
            // 创建玩家
            if (Input.GetKeyDown(KeyCode.R))
            {
                //GameLogicManager.Instance.CreatePlayer(1, GameLogicManager.DEFAULT_POSITION);
                //MonoHelper.AddFixedUpdateListener(() => { Debug.Log(1); });

                /*
                PveModule pveModule = ModuleAPI.PveModule;
                PveGame game = pveModule.CurGame;
                game.CreatePlayer();
                */


                //AppConfig.Save();
                this.gameObject.SetActive(false);
                Debug.Log("在隐藏之后");
            }
        }
    }
}

