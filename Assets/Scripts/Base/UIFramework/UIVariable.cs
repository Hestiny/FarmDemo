using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    public static class UIVariable
    {

    }

    public static class UIPrefabPath
    {
        private static readonly string UI_ROOT = "Prefab/UI/";

        public static readonly string MainWindow = UI_ROOT + "MainWindow";
        public static readonly string SelectFarmWindow = UI_ROOT + "SelectFarmWindow";
        public static readonly string WarehouseWindow = UI_ROOT + "WarehouseWindow";
        public static readonly string ShopWindow = UI_ROOT + "ShopWindow";
    }
}
