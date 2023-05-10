
using System;
using UIFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ButtonPress : MonoBehaviour, IPointerClickHandler
{
    public Action Call;

    public void OnPointerClick(PointerEventData eventData)
    {
        Call?.Invoke();
    }
}
