using UnityEngine;
using System.Collections;
using Kurisu.Game.Data;
using System;

namespace Kurisu.Game.Map
{
    public class NormalModeMapScript : AbstractMapScript<NormalModeMapData>
    {
        public NormalModeMapScript(NormalModeMapData data, Transform container) : base(data, container)
        {

        }

        public override void EnterFrame(int frameIndex)
        {
            //throw new NotImplementedException();
        }

        public override void FirstLoad()
        {
            LoadMapPart(m_data.mapPart, new Vector3Data(0, 0, 0));
        }
    }

}
