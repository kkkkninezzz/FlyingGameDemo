using UnityEngine;
using System.Collections.Generic;

using Kurisu.Game.Data;

using SGF.Unity;
using Kurisu.Module.Pve;
using Kurisu.Service.Core;
using Kurisu.Module;
using System;

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
            param.mode = GameMode.NormalPVE;

            GameLogicManager game = GameLogicManager.Instance;
            game.CreateGame(param);

            PlayerData playerData = new PlayerData();
            playerData.id = 1;
            playerData.vehicleData.id = 0;
            playerData.vehicleData.config = new FlightConfig();

            game.RegPlayerData(playerData);
            */

            //PveModule pveModule = ModuleAPI.PveModule;
            //pveModule.StartGame(GameMode.EndlessPVE);
            

            /*
            action += test1;
            action += test2;
            action += test3;
            */
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

                AppConfig.Save();
            }

            //GameLogicManager.Instance.EnterFrame(1);
            
            /*
            if (action != null)
            {
               // Debug.Log(action.GetInvocationList().Length);
                action();
               // Debug.Log(action.GetInvocationList().Length);

            }
            */


        }

        private void test1()
        {
            Debug.Log("test1");
            action -= test1;
        }

        private void test2()
        {
            Debug.Log("test2");
        }

        private void test3()
        {
            Debug.Log("test3");
        }
    }
}

