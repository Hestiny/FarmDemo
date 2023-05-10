using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
using static Farm.BackageCtrl;

namespace Farm
{
    public class SelectFarmWindow : UIWIndow
    {
        public Button Cron;
        public Button Tomato;
        public Button Potato;
        public Button Close;
        public Text Lv;
        public Button UpLv;

        public Button ScareScrowBtn;
        public Button SprinklerBtn;
        public Text ScareScrowCount;
        public Text SprinklerCount;

        private int _id;
        private FarmData _farmData;

        public struct WindowParam
        {
            public int id;
        }

        protected override void InitWindow<T>(T? obj)
        {
            var param = obj as WindowParam?;
            if (param == null)
                return;
            _id = param.Value.id;
            _farmData = FarmCtrl.Instance.GetFarmData(_id);
            UpdateUI();
        }

        public override void OnAwake()
        {
            Cron.onClick.AddListener(() => { Sow(PropType.Corn); });
            Tomato.onClick.AddListener(() => { Sow(PropType.Tomato); });
            Potato.onClick.AddListener(() => { Sow(PropType.Potato); });
            UpLv.onClick.AddListener(UpLvClick);
            ScareScrowBtn.onClick.AddListener(BuyScarecrow);
            SprinklerBtn.onClick.AddListener(BuySprinkler);
            Close.onClick.AddListener(ToCloseWindow);
        }

        public override void BeforeShow()
        {

        }

        public void Init(int id)
        {
            _id = id;
            _farmData = FarmCtrl.Instance.GetFarmData(_id);
        }

        public void UpdateUI()
        {
            Lv.text = _farmData.lv.ToString();
            ScareScrowCount.text = _farmData.ScarecrowCount.ToString() + "/" + 2;
            SprinklerCount.text = _farmData.SprinklerCount.ToString() + "/" + 4;
        }

        private void UpLvClick()
        {
            _farmData.lv++;
            Lv.text = _farmData.lv.ToString();
        }

        private void BuyScarecrow()
        {
            if (_farmData.ScarecrowCount < 2)
            {
                _farmData.ScarecrowCount++;
                UpdateUI();
            }
        }

        private void BuySprinkler()
        {
            if (_farmData.SprinklerCount < 4)
            {
                _farmData.SprinklerCount++;
                UpdateUI();
            }
        }

        private void Sow(PropType propType)
        {
            EventDispatcher.TriggerEvent(new SowEventPram() { Id = _id, Type = propType }); ;
            ToCloseWindow();
        }
    }
}

