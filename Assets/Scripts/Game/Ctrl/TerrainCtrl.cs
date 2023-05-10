using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Farm
{
    public class TerrainCtrl : Singleton<TerrainCtrl>
    {
        private Transform _farmpos;//农田点位
        private Transform _warehousepos;//仓库点位
        private Transform _shoppos;//商店点位

        public void UpdatePos()
        {
            _farmpos = GameObject.Find("farmpos").transform;
            _warehousepos = GameObject.Find("warehousepos").transform;
            _shoppos = GameObject.Find("shoppos").transform;
        }

        public void LoadScenes()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("farm1", LoadSceneMode.Single);
            asyncOperation.completed += (AsyncOperation obj) =>
            {
                UpdatePos();
                InitPrefab();
            };

        }

        /// <summary>
        /// 加载农场预制
        /// </summary>
        public void InitPrefab()
        {
            var winObj = Resources.Load<GameObject>(ResPath.FarmPath);
            Object.Instantiate(winObj, _farmpos);
            winObj = Resources.Load<GameObject>(ResPath.WarehousePath);
            Object.Instantiate(winObj, _warehousepos);
            winObj = Resources.Load<GameObject>(ResPath.ShopPath);
            Object.Instantiate(winObj, _shoppos);
        }
    }
}

