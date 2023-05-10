using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Farm.BackageCtrl;

namespace Farm
{
    public class DataCtrl : Singleton<DataCtrl>
    {
        const string PalyeDataKey = "PalyeDataKey";

        public static void Init()
        {
            string json = PlayerPrefs.GetString(PalyeDataKey, string.Empty);
        }

        public static void Save()
        {
            PlayerPrefs.SetString(PalyeDataKey, string.Empty);
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="propType"></param>
        /// <returns></returns>
        public static int GetPropNum(PropType propType)
        {
            return PlayerPrefs.GetInt(propType.ToString(), 0);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="propType"></param>
        /// <param name="num"></param>
        public static void SetPropNum(PropType propType, int num)
        {
            PlayerPrefs.SetInt(propType.ToString(), num);
            EventDispatcher.TriggerEvent(new PropChangePram());
        }
    }
}

