#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace Kurisu.GameEditor.Uitls
{
    public class CircleSpawner : MonoBehaviour
    {
        /// <summary>
        /// 模型
        /// </summary>
        public GameObject CircleModel;

        /// <summary>
        /// 根结点
        /// </summary>
        public Transform Root;

        /// <summary>
        /// 旋转改变的角度
        /// </summary>
        public float ChangeAngle = 10;

        /// <summary>
        /// 半径
        /// </summary>
        public float Radius;

        [Button("生成圆环")]
        public void Spawn()
        {
            //旋转一周需要的预制物体个数
            int count = (int)(360 / ChangeAngle);
            float angle = 0;

            Vector3 center = Vector3.zero;
            for (int i = 0; i < count; i++)
            {
                GameObject go = Root == null ? Instantiate<GameObject>(CircleModel) : Instantiate<GameObject>(CircleModel, Root);

                float radian = (angle / 180) * Mathf.PI;
                float xx = center.x + Radius * Mathf.Cos(radian);
                float yy = center.y + Radius * Mathf.Sin(radian);
                go.transform.position = new Vector3(xx, yy, 0);
                go.transform.LookAt(center);
                
                if (Radius == Mathf.Abs(yy))
                {
                    Quaternion rotation = go.transform.rotation;
                    Vector3 rota = go.transform.rotation.eulerAngles;
                    rota.y = 90;
                    go.transform.rotation = Quaternion.Euler(rota);
                }
                angle += ChangeAngle;
            }
        }

        [Button("清除圆环")]
        public void Remove()
        {
            if (Root == null)
            {
                Debug.LogError("Root结点不存在！请先指定Root结点！");
                return;
            }
            EditorUtils.RemoveAllChildren(Root);
        }
    }
}

#endif