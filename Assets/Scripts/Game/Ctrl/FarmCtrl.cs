using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm
{
    public class FarmCtrl : Singleton<FarmCtrl>
    {

        public Dictionary<int, FarmData> FarmsData = new Dictionary<int, FarmData>();

        public FarmData GetFarmData(int id)
        {
            if (FarmsData.TryGetValue(id, out var data))
            {
                return data;
            }
            var defaultData = new FarmData(id);
            FarmsData.Add(id, defaultData);
            return FarmsData[id];
        }
    }

    public class FarmData
    {
        public int Id;
        public int lv = 1;
        public int ScarecrowCount = 0;//稻草人个数
        public int SprinklerCount = 0;//洒水车个数

        public FarmData(int id)
        {
            Id = id;
        }
    }
}

