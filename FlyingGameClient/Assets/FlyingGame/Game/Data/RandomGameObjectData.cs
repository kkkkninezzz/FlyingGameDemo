using System;


namespace Kurisu.Game.Data
{
    [Serializable]
    public class RandomGameObjectData : GameObjectData
    {
        /// <summary>
        /// 这个游戏对象实例化的概率
        /// </summary>
        public float Probability;
    }
}
