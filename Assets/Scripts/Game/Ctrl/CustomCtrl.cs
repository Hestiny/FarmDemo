using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Farm
{
    public class CustomCtrl : MonoBehaviour
    {

        public Transform Start;
        public Transform Shop;
        public Transform End;

        Vector3 _offsetShop;
        Vector3 _offsetEnd;
        bool _isBuy = false;
        bool _isBought = false;
        float dely = 0;

        private void Update()
        {
            if (!_isBuy)
                WalkAnim();
            else
            {
                dely -= Time.deltaTime;
                if (dely <= 0)
                    _isBuy = false;
            }
        }

        /// <summary>
        /// 朝下一个标记点走
        /// </summary>
        void WalkAnim()
        {
            _offsetShop = Shop.position - transform.position;
            _offsetEnd = End.position - transform.position;
            Vector3 dir = _offsetEnd.normalized;
            Vector3 velocity = dir * 2f;
            transform.position += velocity * Time.deltaTime;
            //transform.eulerAngles = _offset.x <= 0 ? Vector3.zero : new Vector3(0f, 180f, 0);
            //买东西
            if (Vector3.SqrMagnitude(_offsetShop) < 0.2f && !_isBought)
            {
                _isBuy = true;
                _isBought = true;
                var isSell = ShopCtrl.Instance.Sell();
                if (isSell)
                {
                    dely = 1f;
                }
                DebugCtrl.Log($"isSell{isSell}", Color.green);
                //TimerCtrl.Instance.Delay(1f, () => { _isBuy = false; });
            }
            //从头开始
            if (Vector3.SqrMagnitude(_offsetEnd) < 0.2f)
            {
                transform.position = Start.position;
                _isBought = false;
            }
        }
    }

}

