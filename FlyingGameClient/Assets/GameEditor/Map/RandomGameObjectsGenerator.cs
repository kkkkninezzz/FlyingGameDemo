﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Kurisu.GameEditor.Map
{
    public class RandomGameObjectsGenerator : MonoBehaviour
    {
        /// <summary>
        /// 创建RandomGameObject模板
        /// </summary>
        [Button("创建RandomGameObjects模板")]
        public void GenerateRandomGameObjectTemplate()
        {
            GameObject randomGo = new GameObject(ChapterEditorDef.RandomGameObjectPool + "_" + transform.childCount.ToString().PadLeft(3, '0'));
            randomGo.transform.parent = transform;
        }

        /// <summary>
        /// 创建RandomGameObject模板
        /// </summary>
        [Button("移除所有RandomGameObjects模板")]
        public void ClearAllRandomGameObjectTemplates()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}