#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Kurisu.GameEditor.Map
{
    public class BirthPointsDescription : MonoBehaviour
    {

        [DisplayAsString(false), HideLabel]
        public string Description = "这个结点下所有的游戏对象都认为是出生点的位置，根据调整这些对象来确定玩家的出生点位置。";

        /// <summary>
        /// 清除出生点
        /// </summary>
        [Button("清除所有的出生点")]
        public void ClearAllBirthPoints()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
#endif
