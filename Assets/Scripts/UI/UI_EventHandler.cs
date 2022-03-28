using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler //, IBeginDragHandler
{
    // ������
    //public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;
    /**
     * @details UI��� �巡�� �̺�Ʈ ������
     * �ʿ��� �� �߰�
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
 * �巡�׷� ��ġ �̵�
 *  transform.position = eventData.position;
 */