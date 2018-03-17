using System.Collections;
using SGF;

namespace FlyingGame.Service.Core
{
    public abstract class Module
    {
        public virtual void Release()
        {
            this.Log("Release");
        }
    }
}

