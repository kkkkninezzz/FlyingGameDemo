namespace Kurisu.Game.Player.Component
{
    public interface PlayerComponent
    {
        void Release();

        /// <summary>
        /// 用于驱动组件逻辑
        /// </summary>
        /// <param name="frameIndex"></param>
        void EnterFrame(int frameIndex);
    }


    public abstract class AbstractPlayerComponent : PlayerComponent
    {
        protected FlyingPlayer m_player;

        public AbstractPlayerComponent(FlyingPlayer player)
        {
            this.m_player = player;
        }

        public abstract void EnterFrame(int frameIndex);

        public abstract void Release();
    }
}
