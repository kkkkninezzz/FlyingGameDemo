using UnityEngine;
using System.Collections;

namespace Kurisu.Service.UIManager.Example
{
    public class Example
    {
        public void Init()
        {
            UIManager.Instance.Init("ui/Example/");
            UIManager.MainPage = "UIPage1";
            UIManager.Instance.EnterMainPage();
        }
       
    }
}

