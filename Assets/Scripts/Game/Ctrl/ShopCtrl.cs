using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Farm.BackageCtrl;

namespace Farm
{
    public class ShopCtrl : Singleton<ShopCtrl>
    {
        private int ShopMaxCoutn = 4;//总货架量

        public Dictionary<int, ShopData> ShopData = new Dictionary<int, ShopData>();

        public ShopData GetShopData(int id)
        {
            if (ShopData.TryGetValue(id, out var data))
            {
                return ShopData[id];
            }
            var defaultData = new ShopData(id);
            ShopData.Add(id, defaultData);
            return ShopData[id];
        }

        public int GetPrice(PropType propType)
        {
            switch (propType)
            {
                case PropType.None:
                    break;
                case PropType.Coin:
                    break;
                case PropType.Corn:
                    return 1;
                case PropType.Potato:
                    return 2;
                case PropType.Tomato:
                    return 3;
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// 售卖
        /// </summary>
        /// <param name="propType"></param>
        public void Sell(PropType propType)
        {
            for (int i = 0; i < ShopMaxCoutn; i++)
            {
                var data = GetShopData(i);
                if (data.Count > 0)
                {
                    data.Count--;
                    ShopData[i] = data;
                    BackageCtrl.Instance.AddProp(PropType.Coin, GetPrice(propType));
                    EventDispatcher.TriggerEvent(new SellEventPram() { Id = i, Type = propType });
                    return;
                }
            }
        }

        public bool Sell()
        {
            for (int i = 0; i < ShopMaxCoutn; i++)
            {
                var data = GetShopData(i);
                if (data.Count > 0)
                {
                    data.Count--;
                    ShopData[i] = data;
                    BackageCtrl.Instance.AddProp(PropType.Coin, GetPrice(data.propType));
                    EventDispatcher.TriggerEvent(new SellEventPram() { Id = i, Type = data.propType });
                    return true;
                }
            }
            return false;
        }

        public bool AddGoods(int id, PropType propType)
        {
            int max = GetMaxCount(id);
            if (GetShopData(id).Count < max)
            {
                if (WareHouseCtrl.Instance.CostCrop(propType, 1))
                {
                    var data = GetShopData(id);
                    data.Count++;
                    ShopData[id] = data;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取格子的存储上限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetMaxCount(int id)
        {
            var data = GetShopData(id);
            int baseRatio = (data.lv / 20 + 1) * 2 * (data.lv / 20 + 1);
            float ratio = 1 + 0.05f * (data.ScarecrowCount + data.SprinklerCount);
            int maxcount = (int)(baseRatio * ratio);
            return maxcount;
        }
    }

    public class ShopData
    {
        public int Id;
        public int lv = 1;
        public PropType propType = PropType.None;//销售的物品类型
        public int Count = 0;//保持的数量
        public int ScarecrowCount = 0;//稻草人个数
        public int SprinklerCount = 0;//洒水车个数

        public ShopData(int id)
        {
            Id = id;
        }
    }
}

