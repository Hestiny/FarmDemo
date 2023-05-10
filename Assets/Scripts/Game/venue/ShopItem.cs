using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using static Farm.BackageCtrl;

namespace Farm
{
    public class ShopItem : MonoBehaviour
    {
        public int Shopid;

        public ButtonPress GroupBtn;
        private PropType _curCrop;

        [SerializeField]
        private Transform _cron;
        [SerializeField]
        private Transform _tomato;
        [SerializeField]
        private Transform _potato;

        private void Awake()
        {
            GroupBtn.Call = GroupClick;
            EventDispatcher.AddListener(typeof(SellEventPram).Name, Sell);
            ClearSell();
        }

        private void Sell(IBaseEventStruct baseEvent)
        {
            if (baseEvent is SellEventPram)
            {
                var pram = (SellEventPram)baseEvent;
                if (pram.Id == Shopid)
                {
                    _curCrop = pram.Type;
                    if (ShopCtrl.Instance.GetShopData(Shopid).Count > 0)
                        Createobj(_curCrop);
                    else
                    {
                        ClearSell();
                    }
                }
            }
        }

        private void Createobj(PropType propType)
        {
            ClearSell();
            switch (propType)
            {
                case PropType.Corn:
                    _cron.gameObject.SetActive(true);
                    break;
                case PropType.Potato:
                    _potato.gameObject.SetActive(true);
                    break;
                case PropType.Tomato:
                    _tomato.gameObject.SetActive(true);
                    break;
                case PropType.None:
                default:
                    break;
            }
        }

        private void ClearSell()
        {
            _cron.gameObject.SetActive(false);
            _potato.gameObject.SetActive(false);
            _tomato.gameObject.SetActive(false);
        }

        private void GroupClick()
        {
            var win = UICtrl.OpenWindow<ShopWindow, ShopWindow.WindowParam>(new ShopWindow.WindowParam() { id = Shopid });
        }
    }
}

