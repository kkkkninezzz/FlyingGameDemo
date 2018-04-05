using UnityEngine;
using System.Collections;
using Kurisu.Service.UIManager;

namespace Kurisu.UI.Home
{
    public class UIHomePage : UIPage
    {

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
        }

        /// <summary>
        /// 人物动画控制器
        /// </summary>
        public Animator CharactorAnim;

        public void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {

                Debug.Log(CharactorAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
                //CharactorAnim.SetInteger("ToWait", 1);
                //CharactorAnim.SetBool("Next", true);

                CharactorAnim.SetTrigger("ToPose");
            } 

            if (Input.GetKey(KeyCode.D))
            {
                // CharactorAnim.SetTrigger("TouchAbdomen");
                CharactorAnim.Play("TouchAbdomen", 0, 0f);
            }
        }
    }
}

