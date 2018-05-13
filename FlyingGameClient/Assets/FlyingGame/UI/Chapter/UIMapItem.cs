using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kurisu.Setting;
using Kurisu.Module.Pve;
using Kurisu.Module;
using Kurisu.Module.Map;
using Kurisu.Game.Data;
using Kurisu.UI;
using SGF.Utils;

public class UIMapItem : MonoBehaviour
{
    /// <summary>
    /// 地图编号
    /// </summary>
    public Text No;

    /// <summary>
    /// 地图名称
    /// </summary>
    public Text Name;

    /// <summary>
    /// 上锁的面板
    /// </summary>
    public GameObject LockPanel;

    /// <summary>
    /// 地图数据
    /// </summary>
    private MapConfigData m_data;

    /// <summary>
    /// 是否已经解锁了
    /// </summary>
    private bool m_isUnlocked;

    private bool isDataChanged = false;

    public void SetData(MapConfigData data, bool isUnlocked)
    {
        this.m_data = data;
        m_isUnlocked = isUnlocked;
        isDataChanged = true;
    }

    public void Update()
    {
        if (isDataChanged)
        {
            UpdateView();
            isDataChanged = false;
        }
    }

    private void UpdateView()
    {
        No.text = m_data.no;
        Name.text = m_data.name;

        // 根据是否解锁了来展示不同的视图
        GameObjectUtils.SetActiveRecursively(LockPanel, !m_isUnlocked);
    }

    public void OnMapItemClick()
    {
        if (m_data == null || !m_isUnlocked)
        {
            return;
        }
        UIAPI.ShowUIWindow(UIDef.UIMapItemDetailWindow, m_data);
    }
}
