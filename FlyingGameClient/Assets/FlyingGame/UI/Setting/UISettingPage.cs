using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Kurisu.Service.UIManager;
using Kurisu.Module.Setting;
using Kurisu.Module;

namespace Kurisu.UI.Setting
{
    public class UISettingPage : UIPage
    {
        [SerializeField]
        private Slider BgmSlider;

        protected override void OnOpen(object arg = null)
        {
            float bgmValue = (float)arg;
            BgmSlider.value = bgmValue;   
        }

        public void OnSaveBtnClick()
        {
            SettingModule settingModule = ModuleAPI.SettingModule;
            settingModule.SetBgmVolume(BgmSlider.value);
        }
    }

}
