using UnityEngine;
using System.Collections;

namespace Kurisu.Service.UIManager.Example
{
    public class UIPage1 : UIPage
    {
        public void OnOpenPage2Btn()
        {
            UIManager.Instance.OpenPage("UIPage2");
        }
        
    }

}
