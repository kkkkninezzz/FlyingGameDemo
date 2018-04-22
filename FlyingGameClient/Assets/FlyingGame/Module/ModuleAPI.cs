using UnityEngine;


using Kurisu.Module.CharacterTouch;
using Kurisu.Service.Core;
using Kurisu.Module.Home;
using Kurisu.Module.Pve;

namespace Kurisu.Module
{
    public class ModuleAPI : MonoBehaviour
    {

        /// <summary>
        /// 获取CharacterTouchModule
        /// </summary>
        public static CharacterTouchModule CharacterTouchModule
        {
            get
            {
                return ModuleManager.Instance.GetModule(ModuleDef.CharacterTouchModule) as CharacterTouchModule;
            }
        }

        /// <summary>
        /// 获取HomeModule
        /// </summary>
        public static HomeModule HomeModule
        {
            get
            {
                return ModuleManager.Instance.GetModule(ModuleDef.HomeModule) as HomeModule;
            }
        }

        public static PveModule PveModule
        {
            get
            {
                return ModuleManager.Instance.GetModule(ModuleDef.PveModule) as PveModule;
            }
        }
    }
}

