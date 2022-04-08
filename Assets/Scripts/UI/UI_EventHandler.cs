using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler //, IBeginDragHandler
{
    // 리스너
    //public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;
    /**
     * <summary>UI요소 드래그 이벤트 리스너</summary>
     * 필요할 때 추가
     */
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnBeginDrag");
    //    if(OnBeginDragHandler != null)
    //    {
    //        OnBeginDragHandler.Invoke(eventData);
    //    }
    //}

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (OnDragHandler != null)
        {
            OnDragHandler.Invoke(eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnClick");
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
        }
    }
}
/**
 * 드래그로 위치 이동
 *  transform.position = eventData.position;
 */