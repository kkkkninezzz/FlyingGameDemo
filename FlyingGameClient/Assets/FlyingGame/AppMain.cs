using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SGF;

using Kurisu.Service.Core;
using Kurisu.Service.UIManager;
using Kurisu.Module;

namespace Kurisu
{
    public class AppMain : MonoBehaviour
    {

        // Use this for initialization

        void Start()
        {
            Debugger.EnableLog = true;

            InitServiceModules();
            InitBusinessModules();
        }

        private void InitServiceModules()
        {
            ModuleManager.Instance.Init();

            UIManager.Instance.Init("ui/");
        }

        private void InitBusinessModules()
        {
            ModuleManager.Instance.CreateModule(ModuleDef.CharacterTouchModule);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

