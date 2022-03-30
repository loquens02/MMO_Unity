using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects)); // no GameObject

        // GridPanel 밑에 붙은 것들 날리고 시작
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destoy(child.gameObject);
        }

        // 임의로 8개 추가해서 GridPanel 밑에 넣어주기. TODO 인벤토리 정보 참조
        for(int i=0; i<8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item"); // 인벤토리 실습.
            item.transform.SetParent(gridPanel.transform);


            // (하드코딩 말고) 아이템 이름 바꾸기
            // 코드정리- item 에서 바로 GetOrAddComponent 해주기 > Extension this 
            // UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집행검{i}번");
        }
    }
}
/**
 // 오타주의! enum 요소를 바인딩해주는 거니 enum 이름을 가져와야
        Bind<GameObject>(typeof(GameObjects));

        // GridPanel 밑에 붙은 것들 날리고 시작
        // 가설1 채택. Error 삭제 로그는 남는데, 삭제를 하진 않는다. 가설1. 삭제를 하는데 다시 생성한다? 가설2. 삭제를 못 한다?
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            
            Managers.Resource.Destoy(child.gameObject);
            //Debug.Log($"test-기존 obj 삭제: {child.gameObject.name}");
        }

        // TODO 실제 인벤토리 정보 참조해서(나중에).
        // 임의로 8개 추가해서 GridPanel 밑에 넣어주기
        for(int i=0; i<8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item"); // 인벤토리 실습.
            //Error- 새로 생성만 하고, SetParent 가 동작하지 않는다.
            item.transform.SetParent(gridPanel.transform);
            //item.transform.SetParent(null); << 이걸 실행해보니 현상이 명확해졌다.

            //Debug.Log($"test- 누구를 {item.name}, 어느 부모에 Set을 해주는지: {gridPanel.transform.name}");
            // test- 누구를 UI_Inven_Item(Clone), 어느 부모에 Set을 해주는지: GridPanel > 같은 로그 8번 발생. 

            // (하드코딩 말고) 아이템 이름 바꾸기
            //UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
            // item 에서 바로 GetOrAddComponent 해주기 > Extension this 
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"집행검{i}번");
        }


 */