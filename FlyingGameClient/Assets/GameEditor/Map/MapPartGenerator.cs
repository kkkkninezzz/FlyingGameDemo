#if UNITY_EDITOR
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Kurisu.GameEditor.Map
{
    public class MapPartGenerator : MonoBehaviour
    {
        /// <summary>
        /// 创建MapPart模板
        /// </summary>
        [Button("创建MapPart模板")]
        public void GenerateMapPartTemplate()
        {
            Transform mapParts = this.transform;

            GameObject mapPartPrefab = Resources.Load<GameObject>(ChapterEditorDef.MapPartPrefabPath);
            if (mapPartPrefab == null)
            {
                Debug.Log("Don't have MapPartPrefab in " + ChapterEditorDef.MapPartPrefabPath);
                return;
            }

            GameObject mapPart = GameObject.Instantiate(mapPartPrefab);
            mapPart.name = ChapterEditorDef.MapPart + "_" + mapParts.childCount.ToString().PadLeft(3, '0');
            mapPart.transform.parent = mapParts;
        }

        /// <summary>
        /// 创建RandomGameObject模板
        /// </summary>
        [Button("移除所有MapPart模板")]
        public void ClearAllMapPartTemplates()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
#endif