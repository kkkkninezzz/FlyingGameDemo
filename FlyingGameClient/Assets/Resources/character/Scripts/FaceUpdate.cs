using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kurisu.Character
{
    public class FaceUpdate : MonoBehaviour
    {
        
        public AnimationClip[] animations;
        Animator anim;
        public float delayWeight;
        public bool isKeepFace = false;
        private float current = 0;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (!isKeepFace)
            {
                current = Mathf.Lerp(current, 0, delayWeight);
            }
            anim.SetLayerWeight(1, current);
        }

        // 响应切换表情事件
        public void OnCallChangeFace(string str)
        {
            int ichecked = 0;
            foreach (var animation in animations)
            {
                if (str == animation.name)
                {
                    ChangeFace(str);
                    break;
                }
                else if (ichecked <= animations.Length)
                {
                    ichecked++;
                }
                else
                {
                    // 当找不到时使用默认的
                    str = "default@unitychan";
                    ChangeFace(str);
                }
            }
        }

        private void ChangeFace(string str)
        {

            isKeepFace = true;
            current = 1;
            anim.CrossFade(str, 0);
        }
    }
}


