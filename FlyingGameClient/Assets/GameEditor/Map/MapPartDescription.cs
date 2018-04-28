using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kurisu.GameEditor.Map
{
    public class MapPartDescription : MonoBehaviour
    {

        [DisplayAsString(false), HideLabel]
        public string Description = 
            "StartPosition : MapPart的起始位置，在编辑器调整位置即可。这个位置最好是整个MapPart的起始位置的中心，因为这个涉及到拼接MapPart时的显示效果\n\n" +
            "EndPosition : MapPart的结束位置，在编辑器调整位置即可。这个位置最好是整个MapPart的结束位置的中心，因为这个涉及到拼接MapPart时的显示效果\n\n" +
            "BasicPart : MapPart的基础部分，需要拖入提前制作完成的预制体，BasicPart是必定会实例化的\n\n" +
            "RandomGameObjectPool : 随机列表的池子，每次实例化MapPart时，会从池子中随机抽取一个列表，将列表中的对象进行实例化。每一个RandomGameObjects是一个列表，同一个列表的对象放在同一个RandomGameObjects下。\n\n" +
            "DynamicGameObjects : 动态加载的对象，每个对象在实例化时会判断实例化的概率，然后再进行实例化。目前只配置了0.1 到 1的概率，不同概率的对象放在对应的概率目录下。\n\n";
    }
}

