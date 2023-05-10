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
        /// ��ʼ����������
        /// </summary>
        public void InitData()
        {

        }

        /// <summary>
        /// ��ʼ���߼�
        /// </summary>
        public void InitCtrl()
        {
            UICtrl.Init();
        }

        /// <summary>
        /// ��ʼ��Ϸ�߼�
        /// </summary>
        public void StartGame()
        {
            TerrainCtrl.Instance.LoadScenes();
            UICtrl.OpenWindow<MainWindow, MainWindow.WindowParam>();
        }
    }
}

