using System.Collections;

namespace Kurisu.Module.CharacterTouch
{
    /// <summary>
    /// 人物动画状态定义
    /// </summary>
    public static class CharacterAnimStateDef
    {

        public struct IntCondition
        {
            public string condition;
            public int value;

            public IntCondition(string condition, int value)
            {
                this.condition = condition;
                this.value = value;
            }
        }

        public struct BoolCondition
        {
            public string condition;
            public bool value;

            public BoolCondition(string condition, bool value)
            {
                this.condition = condition;
                this.value = value;
            }
        }

        /// <summary>
        /// Idle状态
        /// </summary>
        public const string IdleState = "Idle";

        /// <summary>
        /// 跳转到TouchAbdomen状态
        /// </summary>
        public const string TouchAbdomenCondition = "TouchAbdomen";

        /// <summary>
        /// 跳转到TouchFace状态
        /// </summary>
        public const string TouchFaceCondition = "TouchFace";

        /// <summary>
        /// 跳转到TouchLeg状态
        /// </summary>
        public const string TouchLegCondition = "TouchLeg";

        /// <summary>
        /// 跳转到TouchChest状态
        /// </summary>
        public const string TouchChestCondition = "TouchChest";
    }
}

