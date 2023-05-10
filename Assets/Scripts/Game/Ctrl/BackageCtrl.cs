using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Farm
{
    public class BackageCtrl : Singleton<BackageCtrl>
    {
        public enum PropType
        {
            None = 0,
            Coin = 1,
            Corn = 2,//����
            Potato = 3,//����
            Tomato = 4,//����
        }

        public bool CostProp(PropType propType, int num)
        {
            if (IsEnough(propType, num))
            {
                DataCtrl.SetPropNum(propType, GetPropNum(propType) - num);
                EventDispatcher.TriggerEvent(new PropChangePram());
                return true;
            }
            return false;
        }

        public void AddProp(PropType propType, int num)
        {
            //TODO: Max����
            DataCtrl.SetPropNum(propType, GetPropNum(propType) + num);
            EventDispatcher.TriggerEvent(new PropChangePram());
        }

        public int GetPropNum(PropType propType)
        {
            return DataCtrl.GetPropNum(propType);
        }

        public bool IsEnough(PropType propType, int num)
        {
            return GetPropNum(propType) >= num;
        }
    }

}


