using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Kurisu.GameEditor.Map
{

    public class ChapterEditor : MonoBehaviour
    {
      

        /// <summary>
        /// 地图模式
        /// 
        /// 注：EndlessMode下不包含章节编号与小章节编号
        /// </summary>
        [Title("地图的模式")]
        [InfoBox("注：EndlessMode下不包含章节编号与小章节编号")]
        public MapMode Mode;

        /// <summary>
        /// 章节编号
        /// </summary>
        [Title("章节编号")]
        public int ChapterNo;

        /// <summary>
        /// 小章节编号，填入-1表示没有小章节
        /// </summary>
        [Title("小章节编号")]
        [InfoBox("注：填入-1表示没有小章节")]
        public int SmallChapterNo = -1;

        /// <summary>
        /// 章节名称
        /// </summary>
        [Title("章节名称")]
        public string ChapterName;
       

        /// <summary>
        /// 导出章节配置
        /// </summary>
        [Button("导出ChapterConfig")]
        public void ExportConfig()
        {
            switch(Mode)
            {
                case MapMode.ChapterMode:
                    ExportChapterModeConfig();break;
                case MapMode.EndlessMode:
                    ExportEndlessModeConfig(); break;
            }
        }

        /// <summary>
        /// 导出关卡模式的配置
        /// </summary>
        private void ExportChapterModeConfig()
        {

        }

        /// <summary>
        /// 导出无尽模式的配置
        /// </summary>
        private void ExportEndlessModeConfig()
        {
            Transform mapParts = transform.Find(ChapterEditorDef.MapParts);
            if (mapParts == null)
            {
                Debug.LogError("MapParts 结点不存在，导出失败!!!");
                return;
            }

            if (mapParts.childCount <= 0)
            {
                Debug.LogError("MapParts 结点下不存在 MapPart 结点，导出失败!!!");
                return;
            }

            foreach (Transform mapPart in mapParts)
            {

            }
        }


        private void GenerateMapPartData(Transform mapPart)
        {

        }
    }
}

