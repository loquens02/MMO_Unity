using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager // : MonoBehaviour . Singleton Managers �����ϱ� ���ʿ�
{
    // ������ delegate. [�ݹ�+ �Լ��� ���ڷ� �Ѱ��ֱ�]�� ���� �Ἥ �̸� Ÿ�Ժ��� ������ ��
    public Action KeyAction = null; 
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {
        
        // Ű���� �Է��� �԰�, KeyAction �� �ϳ��� �����ϰ� ���� ��
        if (Input.anyKey && KeyAction != null) 
        {
            KeyAction.Invoke(); // ����: ���� keyAction �־���! > �����ߴ� �ֵ��� �� ��� ��
        }

        if(Input.GetMouseButton(0)) // ��Ŭ�� ������ ��. ������ �־ �ν�
        {
            MouseAction.Invoke(Define.MouseEvent.Press);
            _pressed = true; // �����ִ�
        }
        else // ���콺 ��Ŭ���� ����
        {
            if (_pressed) // ������ �� ���̶� ��Ŭ���� Down ������
            {
                MouseAction.Invoke(Define.MouseEvent.Click); // Click Mouse Event �߻�
            }
            _pressed = false;
        }

        // ���߿� �巡�׳� ���� �ð� �̻� ������ �ִ� Event �� �߰� ����
    }
}
