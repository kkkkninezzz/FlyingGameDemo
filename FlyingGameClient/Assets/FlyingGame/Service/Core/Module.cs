using System.Collections;
using SGF;

namespace Kurisu.Service.Core
{
    public abstract class Module
    {
        public virtual void Release()
        {
            this.Log("Release");
        }
    }
}

