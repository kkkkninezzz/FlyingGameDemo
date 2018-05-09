using UnityEngine;
using System.Collections;
using Kurisu.Game.Entity.Factory;
using System;
using Kurisu.Game.Data;
using SGF.Utils;
using SGF;

namespace Kurisu.Game.Entity.Common
{
    /// <summary>
    /// 普通的entity对象，只能持有CommonView脚本的GameObject
    /// </summary>
    public class CommonEntity : EntityObject
    {
        /*
        protected CommonView m_viewScript = null;
        */

        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="container"></param>
        /// <param name="prefabPath"></param>
        public void Create(Transform container, string prefabPath)
        {
            ViewFactory.CreateView(prefabPath, ConfigConstants.DEFAULT_PREFAB_PATH, this, container);
            //m_viewScript = m_view.GetComponent<CommonView>();
        }

        /// <summary>
        /// 初始化transform数据
        /// </summary>
        /// <param name="data"></param>
        public void InitTransform(TransformData data)
        {
            Transform trans = m_view.transform;
            trans.position = GameObjectUtils.ToVector3(data.position);
            trans.rotation = GameObjectUtils.ToQuaternion(data.rotation);
            trans.localScale = GameObjectUtils.ToVector3(data.scale);
        }

        /// <summary>
        /// 初始化位置
        /// </summary>
        /// <param name="position"></param>
        public void InitPosition(Vector3Data position)
        {
            Transform trans = m_view.transform;
            trans.position = GameObjectUtils.ToVector3(position);
        }

        /*
        public void EnterFrame(int frameIndex)
        {
            if (m_viewScript != null)
            {
                
            }
        }
        */

        protected override void Release()
        {
            ViewFactory.ReleaseView(this);
        }
    }
}

