using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Farm
{
    public class GameLauncher : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);

            InitData();
            InitCtrl();
            StartGame();
        }

        /// <summary>
        /// 初始化所有数据
        /// </summary>
        public void InitData()
        {

        }

        /// <summary>
        /// 初始化逻辑
        /// </summary>
        public void InitCtrl()
        {
            UICtrl.Init();
        }

        /// <summary>
        /// 开始游戏逻辑
        /// </summary>
        public void StartGame()
        {
            TerrainCtrl.Instance.LoadScenes();
            UICtrl.OpenWindow<MainWindow, MainWindow.WindowParam>();
        }
    }
}

