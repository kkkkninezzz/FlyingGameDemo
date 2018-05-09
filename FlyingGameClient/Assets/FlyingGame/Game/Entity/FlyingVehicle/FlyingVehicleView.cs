using UnityEngine;
using System.Collections;

using Kurisu.Game.Entity.Factory;
using System;

namespace Kurisu.Game.Entity.FlyingVehicle
{
    public class FlyingVehicleView : ViewObject
    {
        private FlyingVehicle m_entity;
        private GameContext m_context;
        private Transform m_body;
        
        protected override void Create(EntityObject entity)
        {
            m_entity = entity as FlyingVehicle;
            m_context = GameLogicManager.Instance.Context;

            m_body = transform;
        }

        protected override void Release()
        {
            m_entity = null;
            m_context = null;
        }
        

        // Update is called once per frame
        void Update()
        {
            if (m_entity == null)
                return;
            /*
            while (m_entity.HasNextTransData)
            {
                TransData transData = m_entity.NextTransData;

                switch (transData.type)
                {
                    case TransDataType.TranslateData:
                        HandleTranslateData(transData.translateData);
                        break;
                    case TransDataType.RotateData:
                        HandleRotateData(transData.rotateData);
                        break;
                    case TransDataType.RotationData:
                        HandleRotationData(transData.rotationData);
                        break;
                }
            }
            */
        }

        

        public void OnTriggerEnter(Collider other)
        {
            if (m_entity == null)
                return;

            GameObject go = other.gameObject;

            m_entity.OnTriggered(go.tag);
        }
    }
}

