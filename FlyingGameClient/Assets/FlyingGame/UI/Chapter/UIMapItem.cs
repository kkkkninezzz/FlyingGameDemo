using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kurisu.Setting;
using Kurisu.Module.Pve;
using Kurisu.Module;
using Kurisu.Module.Map;
using Kurisu.Game.Data;

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
    /// 地图数据
    /// </summary>
    private MapConfigData m_data;

    private bool isDataChanged = false;

    public void SetData(MapConfigData data)
    {
        this.m_data = data;
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
    }

    public void OnMapItemClick()
    {
        if (m_data == null)
        {
            return;
        }

        PveModule pveModule = ModuleAPI.PveModule;
        MapData mapData = MapModule.Instance.LoadModeMapData(m_data);

        pveModule.StartGame(m_data.gameMode, mapData);
    }
}
