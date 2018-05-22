using UnityEngine;
using System.Collections.Generic;

using Kurisu.Game.Data;

using SGF.Unity;
using Kurisu.Module.Pve;
using Kurisu.Service.Core;
using Kurisu.Module;
using System;
using Kurisu.Service.Map;
using SGF;
using Kurisu.Service.Audio;

namespace Kurisu.Game
{
    public class GameTest : MonoBehaviour
    {
        public BgmPlayer BgmPlayer;

        public List<AudioClip> Bgms;

        // Use this for initialization
        void Start()
        {
            
        }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                BgmPlayer.Init(Bgms);
                BgmPlayer.AutoPlay();
            }
            else if (Input.GetKey(KeyCode.R))
            {
                BgmPlayer.Release();
            }
        }
    }
}

