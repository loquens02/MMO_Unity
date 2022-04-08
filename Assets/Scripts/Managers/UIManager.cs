using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // sortingOrder 가 0 에서 시작하니 false 인 애랑 값이 같아서 문제. 기본 값을 10으로 하자
    // 0~9 는 popup 보다 먼저 띄우고 싶은 예약 order 로 삼자.
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    /**
     * <summary>popup 혹은 고정UI 생성시 order 적용여부 관리</summary>
     * <param name="sort">true: popup을 생성할 경우 order 증가, false: 고정 UI인 경우</param>
     */
    public void SetCanvas(GameObject go, bool sort= true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go); // canvas 추출
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;  // 2D canvas 기준으로 렌더링
        
        // canvas가 중첩되어 있을 때, 부모가 갖는 sortingOrder 값과 관계없이 고유의 sortingOrder 값을 가지도록
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }


    /**
     * <summary>(popup 이나 고정UI 가 아닌) 별도의 UI 만들기. </summary>
     * <param name="parent">parent 붙일 부모가 있으면 지정. optional. ex: UI_Inven</param>
     * <param name="name">name 다른 개체에 속하는 prefab. ex: UI_Inven_Item</param>
     * <see cref="UI_Inven" langword="Init"/>
     */
    public T MakeSubItem<T>(Transform parent= null, string name= null) where T: UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if(parent != null)
        {
            go.transform.SetParent(parent);
        }

        return Util.GetOrAddComponent<T>(go);
    }


    /**
     * <summary>(계층이나 order가 없는) 고정UI 만들기</summary>
     * <param name="name">Prefab/UI/Scene/UI_Button 개체 이름</param>
     * <returns>T Scripts/UI/Scene/UI_Button.cs 스크립트</returns>
     */
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        // 이름 안 받았으면 T 타입과 같은 타입의 이름을 사용
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        // Prefab 에 있는 것 꺼내오기. ResourceManager.cs 에 이미 prefab 폴더가 있으니 UI 부터 path 지정
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        // UI_Button.cs component 가져오기. 팝업 스택에 넣기
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI; // 혹시 다른데서 이용할 수도 있으니

        // 생성한 여러 popup 개체 몰아넣을 부모를 (찾거나 만들어서) 지정해주기
        go.transform.SetParent(Root.transform);

        

        return sceneUI;
    }

    /**
     * <summary>PopupStack 에 넣기</summary>
     * <param name="name">Prefab/UI/Popup/UI_Button 개체 이름</param>
     * <returns>T Scripts/UI/Popup/UI_Button.cs 스크립트</returns>
     */
    public T ShowPopupUI<T>(string name= null) where T: UI_Popup
    {
        // 이름 안 받았으면 T 타입과 같은 타입의 이름을 사용
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        // Prefab 에 있는 것 꺼내오기. ResourceManager.cs 에 이미 prefab 폴더가 있으니 UI 부터 path 지정
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        // UI_Button.cs component 가져오기. 팝업 스택에 넣기
        T popup = Util.GetOrAddComponent<T>(go); 
        _popupStack.Push(popup);

        // _order++ ? Unity 에서 직접 끝어다 놓는 경우 처리가 안 되는 문제가 있다.> UI_Popup.cs 에서 관리

        // 중복 코드가 남아있었다 (22.3.30)
        // 생성한 여러 popup 개체 몰아넣을 부모를 (찾거나 만들어서) 지정해주기
        go.transform.SetParent(Root.transform);


        return popup;
    }



    /**
     * <summary>마지막에 열었던 Popup 닫기. 직접 사용을 권장하지 않는다.</summary>
     * 강의에서는 public 으로 둠
     */ 
    protected void ClosePopupUI()
    {
        // Stack 에 아무것도 없는지 확인
        if(_popupStack.Count == 0)
        {
            return;
        }

        // 마지막에 열었던 팝업의 부모 개체를 삭제. 더 이상 접근하면 안 되니 null.
        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destoy(popup.gameObject);
        popup = null;

        _order--;
    }

    /**
     * <summary>여러 popup 중 중간 순서의 popup 을 삭제하려는 시도 방지. </summary>
     * <param name="popup">삭제하려고 의도한 팝업이 맞는지 확인</param>
     */
    public void ClosePopupUI(UI_Popup popup)
    {
        if(_popupStack.Count == 0)
        {
            return;
        }

        // popup이 마지막에 생성한 팝업이 아니면 삭제하지 않는다.
        if (_popupStack.Peek() != popup)  // 마지막에 생성한 popup 엿보기. 꺼내지 않는다.
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }


    /**
     * <summary>모든 popup 닫기</summary>
     */
    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

}
