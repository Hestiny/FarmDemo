using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Farm
{
    public class WarehouseItem : MonoBehaviour
    {
        public int id;
        public Button UPLvBtn;
        public Button BuyCarBtn;
        public Button BuyDryerBtn;

        public Text lvText;
        public Text CarCount;
        public Text DryerCount;
        public Text PropName;
        public Text PropCount;

        private WareHouseData _wareHouseData;

        private void Awake()
        {
            UPLvBtn.onClick.AddListener(UpBtnOnClick);
            BuyCarBtn.onClick.AddListener(BuyCarOnClcik);
            BuyDryerBtn.onClick.AddListener(BuyDryerOnClick);
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _wareHouseData = WareHouseCtrl.Instance.GetWareHouseData(id);
            UpdateUI();
        }

        public void UpdateUI()
        {
            lvText.text = _wareHouseData.lv.ToString();
            CarCount.text = _wareHouseData.ScarecrowCount.ToString();
            DryerCount.text = _wareHouseData.SprinklerCount.ToString();
            PropCount.text = _wareHouseData.Count.ToString();
            PropName.text = _wareHouseData.propType.ToString();
        }

        private void UpBtnOnClick()
        {
            _wareHouseData.lv++;
            UpdateUI();
        }

        private void BuyCarOnClcik()
        {
            if (_wareHouseData.ScarecrowCount < 2)
                _wareHouseData.ScarecrowCount++;
            UpdateUI();
        }

        private void BuyDryerOnClick()
        {
            if (_wareHouseData.SprinklerCount < 4)
                _wareHouseData.SprinklerCount++;
            UpdateUI();
        }
    }
}

