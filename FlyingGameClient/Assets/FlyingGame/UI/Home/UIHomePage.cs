using UnityEngine;
using System.Collections;
using Kurisu.Service.UIManager;
using Kurisu.Module.CharacterTouch;
using Kurisu.Service.Core;

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
            
                
            CharacterTouchModule touchModule = ModuleManager.Instance.GetModule(ModuleDef.CharacterTouchModule) as CharacterTouchModule;

            if (touchModule != null)
            {
                touchModule.TouchCharacter(CharacterAnim, part);
            }
        }
    }
}

