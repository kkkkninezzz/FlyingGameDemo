using UnityEngine;
using System.Collections;

namespace Kurisu.Game.Entity.Map
{
    /// <summary>
    /// 自动旋转脚本
    /// </summary>
    public class AutoRotation : MonoBehaviour
    {
        public Vector3 EulerAngle = new Vector3(0, 100, 100);

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(EulerAngle * Time.deltaTime);
        }
    }
}

