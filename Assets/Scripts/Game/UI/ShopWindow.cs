using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
using static Farm.BackageCtrl;

namespace Farm
{
    public class ShopWindow : UIWIndow
    {

        public Text Count;
        public Button TomatoBtn;
        public Button PomatoBtn;
        public Button CornBtn;
        public Button Close;
        public struct WindowParam
        {
            public int id;
        }

        private int _id;

        protected override void InitWindow<T>(T? obj)
        {
            var param = obj as WindowParam?;
            if (param == null)
                return;
            _id = param.Value.id;
        }

        public override void OnAwake()
        {
            TomatoBtn.onClick.AddListener(() => { AddGoods(PropType.Tomato); });
            PomatoBtn.onClick.AddListener(() => { AddGoods(PropType.Potato); });
            CornBtn.onClick.AddListener(() => { AddGoods(PropType.Corn); });
            Close.onClick.AddListener(ToCloseWindow);
        }

        public override void BeforeShow()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            Count.text = ShopCtrl.Instance.GetShopData(_id).Count.ToString();
        }

        private void AddGoods(PropType propType)
        {
            if (ShopCtrl.Instance.AddGoods(_id, propType))
                EventDispatcher.TriggerEvent(new SellEventPram() { Id = _id, Type = propType });
        }
    }
}

