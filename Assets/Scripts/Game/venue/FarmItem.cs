using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Farm.BackageCtrl;

namespace Farm
{
    public class FarmItem : MonoBehaviour
    {
        [SerializeField]
        private int _id;
        [SerializeField]
        private Transform _sowPos;
        [SerializeField]
        private Transform _cron;
        [SerializeField]
        private Transform _tomato;
        [SerializeField]
        private Transform _potato;
        private PropType _curCrop;
        private FarmData _farmData;

        public Transform Wait;
        public Transform Complete;
        public ButtonPress CompleteBtn;
        public ButtonPress GroupBtn;

        private void Awake()
        {
            _sowPos = transform.Find("crop");
            ClearSow();
            _farmData = FarmCtrl.Instance.GetFarmData(_id);
            CompleteBtn.Call = GetCrop;
            GroupBtn.Call = GroupClick;
            EventDispatcher.AddListener(typeof(SowEventPram).Name, Sow);
        }

        public void Init()
        {
            Wait.gameObject.SetActive(false);
            Complete.gameObject.SetActive(false);

        }

        private void Sow(IBaseEventStruct sowEventPram)
        {
            if (sowEventPram is SowEventPram)
            {
                var pram = (SowEventPram)sowEventPram;
                if (pram.Id == _id)
                {
                    _curCrop = pram.Type;
                    Createobj(_curCrop);
                }
            }
        }

        private void ClearSow()
        {
            _cron.gameObject.SetActive(false);
            _potato.gameObject.SetActive(false);
            _tomato.gameObject.SetActive(false);
        }

        private void Createobj(PropType propType)
        {
            ClearSow();
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
                default:
                    break;
            }
            ShowPop();
        }

        private int _timeCount = 10;
        private void ShowPop()
        {
            _timeCount = 10;
            Wait.gameObject.SetActive(true);
            TimerCtrl.Instance.AddTimer("cow" + _id, 1, 10, UpdatePop);
        }

        private void UpdatePop()
        {
            _timeCount--;
            if (_timeCount <= 0)
            {
                //收获    
                Wait.gameObject.SetActive(false);
                Complete.gameObject.SetActive(true);
            }
        }

        private void GetCrop()
        {
            int baseRatio = (_farmData.lv / 20 + 1) * 5 * (_farmData.lv / 20 + 1);
            float ratio = 1 + 0.05f * (_farmData.ScarecrowCount + _farmData.SprinklerCount);
            int Getcount = (int)(baseRatio * ratio);
            WareHouseCtrl.Instance.AddCrop(_curCrop, Getcount);
            Complete.gameObject.SetActive(false);
            ClearSow();
        }

        public void GroupClick()
        {
            var win = UICtrl.OpenWindow<SelectFarmWindow, SelectFarmWindow.WindowParam>(new SelectFarmWindow.WindowParam() { id = _id });
        }
    }
}

