using UnityEngine;
using System.Collections;

using Kurisu.Game.Data;

namespace Kurisu.Game
{
    public class GameTest : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GameParam param = new GameParam();
            param.mode = GameMode.NormalPVE;

            GameLogicManager game = GameLogicManager.Instance;
            game.CreateGame(param);

            PlayerData playerData = new PlayerData();
            playerData.id = 1;
            playerData.vehicleData.id = 0;
            playerData.vehicleData.config = new FlightConfig();

            game.RegPlayerData(playerData);
        }

        // Update is called once per frame
        void Update()
        {
            // 创建玩家
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameLogicManager.Instance.CreatePlayer(1, GameLogicManager.DEFAULT_POSITION);
            }
        }
    }
}

