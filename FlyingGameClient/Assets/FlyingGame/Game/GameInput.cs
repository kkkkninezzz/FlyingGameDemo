using System;
using System.Collections.Generic;
using UnityEngine;

using SGF;
using SGF.Utils;
using Kurisu.Game.Data;

namespace Kurisu.Game
{
    public class GameInput : MonoBehaviour
    {
        /// <summary>
        /// 当键盘输入时，用来记录键盘状态
        /// </summary>
        private static DictionaryEx<KeyCode, bool> m_keyStateMap = new DictionaryEx<KeyCode, bool>();

        /// <summary>
        /// 为调用者抛出虚拟按键事件
        /// </summary>
        public static Action<GameVkey, float> OnVkey;

        private static GameInput m_instance = null;

        //=======================================================================================================
        private const string HORIZONTAL_JOYSTICK = "EasyTouchControlsCanvas/HorizontalJoystick";
        private const string VERTICAL_JOYSTICK = "EasyTouchControlsCanvas/VerticalJoystick";

        /// <summary>
        /// 用来控制水平移动的轮盘
        /// </summary>
        private ETCJoystick m_horizontalJoystick;

        /// <summary>
        /// 用来控制垂直移动的轮盘
        /// </summary>
        private ETCJoystick m_verticalJoystick;

        /// <summary>
        /// 用来加速的按钮
        /// </summary>
        //private ETCButton m_speedUpBtn;

        /// <summary>
        /// 初始化，用来在当前场景添加GameInput对象
        /// </summary>
        public static void Create()
        {
            if (m_instance != null)
            {
                throw new Exception("GameInput 不能重复初始化");
            }

            // 实例化GameInput的prefab，里面预制了EasyJoystick脚本
            // 因为EasyJoystick有一些参数，在prefab中容易配置一些
            GameObject prefab = Resources.Load<GameObject>("GameInput");
            GameObject go = GameObject.Instantiate(prefab);

            m_instance = GameObjectUtils.EnsureComponent<GameInput>(go);
        }

        /// <summary>
        /// 释放当前创建的GameInput对象
        /// </summary>
        public static void Release()
        {
            m_keyStateMap.Clear();
            if (m_instance != null)
            {
                GameObject.Destroy(m_instance.gameObject);
                m_instance = null;
            }
            OnVkey = null;
        }

        public static GameInput Instance
        {
            get
            {
                return m_instance;
            }
        }

        private void Start()
        {
            m_horizontalJoystick = this.transform.Find(HORIZONTAL_JOYSTICK).GetComponent<ETCJoystick>();
            m_verticalJoystick = this.transform.Find(VERTICAL_JOYSTICK).GetComponent<ETCJoystick>();
            /*
            m_horizontalJoystick = this.GetComponentInChildren<ETCJoystick>();
            m_speedUpBtn = this.GetComponentInChildren<ETCButton>();
            
            if (m_horizontalJoystick == null || m_speedUpBtn == null)
            {
                this.LogError("Start() m_joystick == null || m_speedUpBtn == null");
            }
            */
            if (m_horizontalJoystick == null || m_verticalJoystick == null)
            {
                this.LogError("Start() HorizontalJoystick == null || VerticalJoystick == null");
                return;
            }

            RegisterListeners();
        }

        private void RegisterListeners()
        {
            if (m_horizontalJoystick != null)
            {
                m_horizontalJoystick.onMove.AddListener(OnHorizontalJoystickMove);
                m_horizontalJoystick.onMoveEnd.AddListener(OnHorizontalJoystickMoveEnd);
            }

            if (m_verticalJoystick != null)
            {
                m_verticalJoystick.onMove.AddListener(OnVerticalJoystickMove);
                m_verticalJoystick.onMoveEnd.AddListener(OnVerticalJoystickMoveEnd);
            }

            /*
            if (m_speedUpBtn != null)
            {
                m_speedUpBtn.onDown.AddListener(OnSpeedUpBtnDown);
                m_speedUpBtn.onUp.AddListener(OnSpeedUpBtnUp);
            }
            */
        }

        private void RemoveListeners()
        {
            if (m_horizontalJoystick != null)
            {
                m_horizontalJoystick.onMove.RemoveAllListeners();
                m_horizontalJoystick.onMoveEnd.RemoveAllListeners();
            }

            if (m_verticalJoystick != null)
            {
                m_verticalJoystick.onMove.RemoveAllListeners();
                m_verticalJoystick.onMoveEnd.RemoveAllListeners();
            }

            /*
            if (m_speedUpBtn != null)
            {
                m_speedUpBtn.onDown.RemoveAllListeners();
            }
            */
        }

        /*
        private void OnEnable()
        {
            RegisterListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        */

        private void OnDestroy()
        {
            RemoveListeners();
        }

        //=======================================================================================================

        /// <summary>
        /// 水平轮盘移动时
        /// </summary>
        /// <param name="v2"></param>
        private void OnHorizontalJoystickMove(Vector2 v2)
        {
            HandleVkey(GameVkey.MoveHorizontal, v2.x);
        }

        /// <summary>
        /// 水平轮盘结束移动时
        /// </summary>
        private void OnHorizontalJoystickMoveEnd()
        {
            HandleVkey(GameVkey.MoveHorizontal, 0);
        }

        /// <summary>
        /// 垂直轮盘移动时
        /// </summary>
        /// <param name="v2"></param>
        private void OnVerticalJoystickMove(Vector2 v2)
        {
            HandleVkey(GameVkey.MoveVertical, v2.y);
        }

        /// <summary>
        /// 垂直轮盘结束移动时
        /// </summary>
        private void OnVerticalJoystickMoveEnd()
        {
            HandleVkey(GameVkey.MoveVertical, 0);
        }

        /// <summary>
        /// 加速按钮按下时
        /// </summary>
        private void OnSpeedUpBtnDown()
        {
            HandleVkey(GameVkey.SpeedUp, 1);
        }

        /// <summary>
        /// 加速按钮抬起时
        /// </summary>
        private void OnSpeedUpBtnUp()
        {
            HandleVkey(GameVkey.SpeedUp, 0);
        }

        //=======================================================================================================

        private void HandleVkey(GameVkey vkey, float arg)
        {
            if (OnVkey == null)
                return;

            OnVkey(vkey, arg);
        }
    }
}

