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

    // _name 에 대한 초기화? > UI_Inven.cs. 여기 Init()이 먼저 실행된다. 왜 그런지는 모르겠지만.
    string _name;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        // 비어있는 게 11개(기존에 prefab에 있었는데 삭제한 것), 이후에 집행검0~7번 이 들어온다.
        //Debug.Log($"test- ItemNameText: {Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text}");

        // 아이템 누르면 로그 발생
        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventType) => { Debug.Log($"아이템 클릭! {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
    
}
