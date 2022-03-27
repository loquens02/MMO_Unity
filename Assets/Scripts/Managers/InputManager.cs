using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager // : MonoBehaviour . Singleton Managers 있으니까 불필요
{
    // 일종의 delegate. [콜백+ 함수를 인자로 넘겨주기]를 자주 써서 미리 타입별로 만들어둔 것
    //public Action KeyAction = null; 
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        // Unity 2020 ver~ : null check 필요
        if (EventSystem.current == null || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        // 키보드 입력이 왔고, KeyAction 에 하나라도 구독하고 있을 때
        //if (Input.anyKey && KeyAction != null) 
        //{
        //    KeyAction.Invoke(); // 전파: 얘들아 keyAction 있었어! > 구독했던 애들이 다 듣게 됨
        //}

        if(Input.GetMouseButton(0)) // 좌클릭 눌렀을 때. 누르고만 있어도 인식
        {
            MouseAction.Invoke(Define.MouseEvent.Press);
            _pressed = true; // 눌려있다
        }
        else // 마우스 좌클릭을 떼면
        {
            if (_pressed) // 이전에 한 번이라도 좌클릭을 Down 했으면
            {
                MouseAction.Invoke(Define.MouseEvent.Click); // Click Mouse Event 발생
            }
            _pressed = false;
        }
        
    }
}
/*
** OnUpdate(){ ... 나중에 드래그나 일정 시간 이상 누르고 있는 Event 도 추가 가능

 */