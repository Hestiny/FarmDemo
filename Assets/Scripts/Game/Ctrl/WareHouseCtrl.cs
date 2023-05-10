using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Farm.BackageCtrl;

namespace Farm
{
    public class WareHouseCtrl : Singleton<WareHouseCtrl>
    {
        public Dictionary<int, WareHouseData> WareHousesData = new Dictionary<int, WareHouseData>();

        public WareHouseData GetWareHouseData(int id)
        {
            if (WareHousesData.TryGetValue(id, out var data))
            {
                return WareHousesData[id];
            }
            var defaultData = new WareHouseData(id);
            WareHousesData.Add(id, defaultData);
            return WareHousesData[id];
        }

        /// <summary>
        /// 仓库个数
        /// </summary>
        public readonly int WareHouseMaxCount = 4;

        /// <summary>
        /// 增加存储
        /// </summary>
        /// <param name="id"></param>
        /// <param name="propType"></param>
        /// <param name="count"></param>
        public void AddCrop(PropType propType, int count)
        {
            int tmpcpunt = count;//多余项
            for (int i = 0; i < WareHouseMaxCount; i++)
            {
                var data = GetWareHouseData(i);
                if (data.propType == propType)
                {
                    int max = GetMaxCount(i);
                    if (data.Count < max)
                    {
                        data.Count += count;
                        WareHousesData[i] = data;
                        if (data.Count > max)
                        {
                            tmpcpunt = data.Count - max;
                            data.Count = max;
                            WareHousesData[i] = data;
                        }
                    }
                }
            }
            //将剩余个数放到空闲仓库
            for (int i = 0; i < WareHouseMaxCount; i++)
            {
                var data = GetWareHouseData(i);
                if (data.propType == PropType.None)
                {
                    int max = GetMaxCount(i);
                    if (tmpcpunt > 0)
                    {
                        data.Count += tmpcpunt;
                        tmpcpunt = data.Count - max;
                        data.propType = propType;
                        WareHousesData[i] = data;
                        if (data.Count > max)
                        {
                            tmpcpunt = data.Count - max;
                            data.Count = max;
                            WareHousesData[i] = data;
                            continue;
                        }
                        else
                            return;
                    }
                }
            }
            BackageCtrl.Instance.AddProp(propType, count - tmpcpunt);
        }

        /// <summary>
        /// 消耗存储
        /// </summary>
        /// <param name="propType"></param>
        /// <param name="count"></param>
        public bool CostCrop(PropType propType, int count)
        {
            List<WareHouseData> tmpDatas = new List<WareHouseData>();
            int totalCount = 0;
            for (int i = 0; i < WareHouseMaxCount; i++)
            {
                var data = GetWareHouseData(i);
                if (data.propType == propType)
                {
                    totalCount += data.Count;
                    tmpDatas.Add(data);
                    tmpDatas.Sort((x, y) => x.Count.CompareTo(y.Count));
                }
            }
            int tmpCount = count;
            if (totalCount >= count)
            {
                for (int i = 0; i < tmpDatas.Count; i++)
                {
                    tmpCount = tmpCount - tmpDatas[i].Count;
                    tmpDatas[i].Count -= count;
                    WareHousesData[tmpDatas[i].Id] = tmpDatas[i];
                    if (tmpDatas[i].Count < 0)
                    {
                        tmpDatas[i].Count = 0;
                        tmpDatas[i].propType = PropType.None;
                        WareHousesData[tmpDatas[i].Id] = tmpDatas[i];
                    }
                    if (tmpCount <= 0)
                    {
                        BackageCtrl.Instance.AddProp(propType, -count);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取仓库的存储上限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetMaxCount(int id)
        {
            var data = GetWareHouseData(id);
            int baseRatio = (data.lv / 20 + 1) * 10 * (data.lv / 20 + 1);
            float ratio = 1 + 0.05f * (data.ScarecrowCount + data.SprinklerCount);
            int maxcount = (int)(baseRatio * ratio);
            return maxcount;
        }
    }

    public class WareHouseData
    {
        public int Id;
        public int lv = 1;
        public PropType propType = PropType.None;//保存的物品类型
        public int Count = 0;//保持的数量
        public int ScarecrowCount = 0;//稻草人个数
        public int SprinklerCount = 0;//洒水车个数

        public WareHouseData(int id)
        {
            Id = id;
        }
    }

}
