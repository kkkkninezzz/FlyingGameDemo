using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kurisu.Service.Core;

namespace Kurisu.Module.CharacterTouch
{
    /// <summary>
    /// 身体部位
    /// </summary>
    public enum BodyPart
    {
        /// <summary>
        /// 脸
        /// </summary>
        Face,

        /// <summary>
        /// 胸口
        /// </summary>
        Chest,

        /// <summary>
        /// 腹部
        /// </summary>
        Abdomen,

        /// <summary>
        /// 腿
        /// </summary>
        Leg,

    }

    public class CharacterTouchModule : BusinessModule
    {
        /// <summary>
        /// 触摸人物
        /// </summary>
        /// <param name="characterAnim">人物的动画组件</param>
        /// <param name="touchPart">触摸的部位</param>
        public void TouchCharacter(Animator characterAnim, BodyPart touchPart)
        {
            if (characterAnim == null)
                return;

            // 如果不是处于idle状态，不允许触发动画
            if (!characterAnim.GetCurrentAnimatorStateInfo(0).IsName(CharacterAnimStateDef.IdleState))
                return;
           
            switch(touchPart)
            {
                case BodyPart.Face:
                    characterAnim.SetTrigger(CharacterAnimStateDef.TouchFaceCondition);
                    break;
                case BodyPart.Chest:
                    characterAnim.SetTrigger(CharacterAnimStateDef.TouchChestCondition);
                    break;
                case BodyPart.Abdomen:
                    characterAnim.SetTrigger(CharacterAnimStateDef.TouchAbdomenCondition);
                    break;
                case BodyPart.Leg:
                    characterAnim.SetTrigger(CharacterAnimStateDef.TouchLegCondition);
                    break;
            }
        }
    }
}


