using Farm;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    public class UICtrl
    {
        public struct WinVariable
        {
            public string ResPath;
            public UIGroupEnum WinGroup;

            public WinVariable(string path, UIGroupEnum group)
            {
                ResPath = path;
                WinGroup = group;
            }
        }
        public static Transform UIContent;
        public static Transform UILayer;
        public static UIPanel CurrentWin;
        public static UIPanel MainWin;
        public static Dictionary<System.Type, List<UIPanel>> WindowList = new Dictionary<System.Type, List<UIPanel>>();
        public static Dictionary<string, WinVariable> WindowResPath = new Dictionary<string, WinVariable>()
        {
            { typeof(MainWindow).Name,new WinVariable(UIPrefabPath.MainWindow,UIGroupEnum.Main ) },
            { typeof(SelectFarmWindow).Name,new WinVariable(UIPrefabPath.SelectFarmWindow,UIGroupEnum.Main ) },
            { typeof(WarehouseWindow).Name,new WinVariable(UIPrefabPath.WarehouseWindow,UIGroupEnum.Main ) },
            { typeof(ShopWindow).Name,new WinVariable(UIPrefabPath.ShopWindow,UIGroupEnum.Main ) },
        };

        public static void Init()
        {
            UILayer = GameObject.Find("UILayer").transform;
            UIContent = UILayer.Find("UICanvas/UIContent");
            Object.DontDestroyOnLoad(UILayer);
        }

        //TODO: 拿到页面预制体的路径,多个实例窗口的创建限制
        private static T GeneratePanel<T, H>(H? pram, Transform target = null) where T : UIWIndow where H : struct
        {
            if (target == null)
            {
                target = UIContent;
            }
            if (!WindowResPath.TryGetValue(typeof(T).Name, out var winVariable))
            {
                Debug.LogError(typeof(T).Name + " 该窗体没有设置预制体路径WindowResPath");
                return default(T);
            }
            var winObj = Resources.Load<GameObject>(winVariable.ResPath);
            var win = Object.Instantiate(winObj, target).GetComponent<T>();
            if (win == null)
            {
                Debug.LogError(string.Format("Component [{0}] not find.", typeof(T).Name));
            }
            if (WindowList.TryGetValue(typeof(T), out var winList))
            {
                winList.Add(win);
            }
            else
            {
                WindowList.Add(typeof(T), new List<UIPanel> { win });
            }
            win.init(pram);
            win.Open();
            return win;
        }

        public static T OpenWindow<T, H>(H? pram = null) where T : UIWIndow where H : struct
        {
            return GeneratePanel<T, H>(pram);
        }

        public static void CloseWindow<T>() where T : UIPanel
        {
            if (WindowList.TryGetValue(typeof(T), out var winList))
            {
                winList[winList.Count - 1].Hide();
                winList.RemoveAt(winList.Count - 1);
                if (winList.Count == 0)
                    WindowList.Remove(typeof(T));
            }
            else
            {
                DebugCtrl.Log("没有加载该窗口:" + typeof(T).Name);
            }
        }

        public static void CloseWindow(UIPanel win)
        {
            var winType = win.GetType();
            if (WindowList.TryGetValue(winType, out var winList))
            {
                winList[winList.Count - 1].Hide();
                winList.RemoveAt(winList.Count - 1);
                if (winList.Count == 0)
                    WindowList.Remove(winType);
            }
            else
            {
                DebugCtrl.Log("没有加载该窗口:" + winType.Name);
            }
        }
    }

}

