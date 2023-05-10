using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

namespace Farm
{
    public class WarehouseVenue : MonoBehaviour
    {
        public ButtonPress GroupBtn;

        private void Awake()
        {
            GroupBtn.Call = GroupClick;
        }

        private void GroupClick()
        {
            var win = UICtrl.OpenWindow<WarehouseWindow, WarehouseWindow.WindowParam>();
        }
    }
}

