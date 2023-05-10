using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Farm
{
    public class MainWindow : UIWIndow
    {
        [SerializeField]
        private Text _price;
        [SerializeField]
        private Text _cornCount;
        [SerializeField]
        private Text _tomatoCount;
        [SerializeField]
        private Text _potatoCount;

        public struct WindowParam
        {
            int i;
        }

        public override void OnAwake()
        {
            EventDispatcher.AddListener(typeof(PropChangePram).Name, UpdateUI);
        }

        protected override void InitWindow<T>(T? obj)
        {
            var param = obj as WindowParam?;
            if (param == null)
                return;
        }

        public override void BeforeShow()
        {
            UpdateUI();
        }

        private void UpdateUI(IBaseEventStruct baseEventStruct = null)
        {
            _price.text = BackageCtrl.Instance.GetPropNum(BackageCtrl.PropType.Coin).ToString();
            _cornCount.text = BackageCtrl.Instance.GetPropNum(BackageCtrl.PropType.Corn).ToString();
            _tomatoCount.text = BackageCtrl.Instance.GetPropNum(BackageCtrl.PropType.Tomato).ToString();
            _potatoCount.text = BackageCtrl.Instance.GetPropNum(BackageCtrl.PropType.Potato).ToString();
        }
    }
}

