using UnityEngine;
using System.Collections;
using Kurisu.Service.UIManager;
using Kurisu.Module.CharacterTouch;

using Kurisu.Module;

namespace Kurisu.UI.Home
{
    public class UIHomePage : UIPage
    {
        [SerializeField]
        private Animator CharacterAnim;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
        }

        public void Update()
        {

        }

        #region 看板娘触摸
        /// <summary>
        /// 摸脸
        /// </summary>
        public void TouchFace()
        {
            TouchCharacter(BodyPart.Face);
        }

        /// <summary>
        /// 摸欧派
        /// </summary>
        public void TouchChest()
        {
            TouchCharacter(BodyPart.Chest);
        }

        /// <summary>
        /// 摸腹部
        /// </summary>
        public void TouchAbdomen()
        {
            TouchCharacter(BodyPart.Abdomen);
        }

        /// <summary>
        /// 摸腿
        /// </summary>
        public void TouchLeg()
        {
            TouchCharacter(BodyPart.Leg);
        }

        private void TouchCharacter(BodyPart part)
        {
            if (CharacterAnim == null)
                return;


            CharacterTouchModule touchModule = ModuleAPI.CharacterTouchModule;

            if (touchModule != null)
            {
                touchModule.TouchCharacter(CharacterAnim, part);
            }
        }
        #endregion

        #region 主界面按钮功能

        private void OpenModule(string name, object arg = null)
        {
            var homeModule = ModuleAPI.HomeModule;

            if (homeModule != null)
            {
                homeModule.OpenModule(name, arg);
            }
        }

        /// <summary>
        /// 出击按钮功能
        /// </summary>
        public void OnPlayGameBtn()
        {
            OpenModule(ModuleDef.PveModule);
        }

        /// <summary>
        /// 装扮按钮
        /// </summary>
        public void OnDressUpBtn()
        {

        }

        /// <summary>
        /// 背包按钮
        /// </summary>
        public void OnBagBtn()
        {

        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        public void OnSettingBtn()
        {

        }


        #endregion
    }
}

