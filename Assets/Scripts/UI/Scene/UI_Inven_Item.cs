using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    // _name �� ���� �ʱ�ȭ? > UI_Inven.cs. ���� Init()�� ���� ����ȴ�. �� �׷����� �𸣰�����.
    string _name;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // ����ִ� �� 11��(������ prefab�� �־��µ� ������ ��), ���Ŀ� �����0~7�� �� ���´�.
        //Debug.Log($"test- ItemNameText: {Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text}");

        // ������ ������ �α� �߻�
        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventType) => { Debug.Log($"������ Ŭ��! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
    
}
