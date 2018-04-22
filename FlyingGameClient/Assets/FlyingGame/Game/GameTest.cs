using UnityEngine;
using System.Collections;

using Kurisu.Game.Data;

using SGF.Unity;
using Kurisu.Module.Pve;
using Kurisu.Service.Core;
using Kurisu.Module;

namespace Kurisu.Game
{
    public class GameTest : MonoBehaviour
    {

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
            PveModule pveModule = ModuleAPI.PveModule;
            pveModule.StartGame(GameMode.EndlessPVE);
        }

        // Update is called once per frame
        void Update()
        {
            // 创建玩家
            if (Input.GetKeyDown(KeyCode.R))
            {
                //GameLogicManager.Instance.CreatePlayer(1, GameLogicManager.DEFAULT_POSITION);
                //MonoHelper.AddFixedUpdateListener(() => { Debug.Log(1); });

                PveModule pveModule = ModuleAPI.PveModule;
                PveGame game = pveModule.CurGame;
                game.CreatePlayer();
            }

            //GameLogicManager.Instance.EnterFrame(1);
        }

        
    }
}

