using Kurisu.Module;
using Kurisu.Module.Pve;
using Kurisu.Service.UIManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kurisu.UI.Pve
{
    public class UIPveGamePage : UIPage
    {
        public Button PauseBtn;

        public Text ScoreText;

        public Text PuzzleText;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PveModule pveModule = ModuleAPI.PveModule;

            ScoreText.text = pveModule.GameScore.ToString("N0");

            PuzzleText.text = pveModule.PuzzleCount.ToString("N0");
        }

        public void OnPauseBtnClick()
        {
            PveModule pveModule = ModuleAPI.PveModule;

            pveModule.PauseGame();
        }
    }
}

