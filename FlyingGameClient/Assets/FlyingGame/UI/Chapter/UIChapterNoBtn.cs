using Kurisu.Service.UIManager.View;
using Kurisu.UI.Ccommon;
using SGF.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Kurisu.UI.Chapter
{
    public class UIChapterNoBtnModel : ViewModel
    {
        private int m_chapterNo;

        private bool m_isUnlocked;

        public UIChapterNoBtnModel(int chapterNo, bool isUnlocked)
        {
            this.m_chapterNo = chapterNo;
            this.m_isUnlocked = isUnlocked;

            SetDataChanged();
        }

        public int ChapterNo
        {
            get
            {
                return m_chapterNo;
            }

            set
            {
                m_chapterNo = value;
                SetDataChanged();
            }
        }

        public bool IsUnlocked
        {
            get
            {
                return m_isUnlocked;
            }

            set
            {
                m_isUnlocked = value;
                SetDataChanged();
            }
        }
    }

    [RequireComponent(typeof(Button))]
    public class UIChapterNoBtn : RestrictedButton<UIChapterNoBtnModel>
    {

        public Text ChapterNoText;

        public GameObject LockPanel;


        public override bool CanClick()
        {
            return m_model.IsUnlocked;
        }

        protected override void UpdateView()
        {
            if (m_model == null)
            {
                return;
            }
            ChapterNoText.text = string.Format("第{0}章", m_model.ChapterNo);

            // 根据是否解锁了来展示不同的视图
            GameObjectUtils.SetActiveRecursively(LockPanel, !m_model.IsUnlocked);

            GetComponent<Button>().enabled = m_model.IsUnlocked;
            
        }
    }
}

