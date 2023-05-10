using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Farm
{
    public class WarehouseWindow : UIWIndow
    {
        public Button Close;
        public List<WarehouseItem> warehouseItems;

        public struct WindowParam
        {
            int i;
        }


        public override void OnAwake()
        {
            Close.onClick.AddListener(ToCloseWindow);
        }

        public override void Show()
        {
            for (int i = 0; i < warehouseItems.Count; i++)
            {
                warehouseItems[i].UpdateUI();
            }
        }
    }
}

