using UnityEngine;
using System.Collections;
using Kurisu.Service.Core;
using Kurisu.UI;
using Kurisu.Service.Audio;

namespace Kurisu.Module.Setting
{

    public class SettingModule : BusinessModule
    {
        //显示模块的主UI
        protected override void Show(object arg)
        {
            arg = AudioManager.Instance.BgmVolume;
            base.Show(arg);

            UIAPI.ShowUIPage(UIDef.UISettingPage, arg);
        }

        public void SetBgmVolume(float volume)
        {
            AudioManager audioManager = AudioManager.Instance;
            if (volume > 1 || volume < 0 || audioManager.BgmVolume == volume)
            {
                return;
            }
            
            audioManager.BgmVolume = volume;

            audioManager.SaveConfig();
        }
    }
}
