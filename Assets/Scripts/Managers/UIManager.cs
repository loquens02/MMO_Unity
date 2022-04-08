using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    // sortingOrder �� 0 ���� �����ϴ� false �� �ֶ� ���� ���Ƽ� ����. �⺻ ���� 10���� ����
    // 0~9 �� popup ���� ���� ���� ���� ���� order �� ����.
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
     * <summary>popup Ȥ�� ����UI ������ order ���뿩�� ����</summary>
     * <param name="sort">true: popup�� ������ ��� order ����, false: ���� UI�� ���</param>
     */
    public void SetCanvas(GameObject go, bool sort= true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go); // canvas ����
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;  // 2D canvas �������� ������
        
        // canvas�� ��ø�Ǿ� ���� ��, �θ� ���� sortingOrder ���� ������� ������ sortingOrder ���� ��������
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
     * <summary>(popup �̳� ����UI �� �ƴ�) ������ UI �����. </summary>
     * <param name="parent">parent ���� �θ� ������ ����. optional. ex: UI_Inven</param>
     * <param name="name">name �ٸ� ��ü�� ���ϴ� prefab. ex: UI_Inven_Item</param>
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
     * <summary>(�����̳� order�� ����) ����UI �����</summary>
     * <param name="name">Prefab/UI/Scene/UI_Button ��ü �̸�</param>
     * <returns>T Scripts/UI/Scene/UI_Button.cs ��ũ��Ʈ</returns>
     */
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        // �̸� �� �޾����� T Ÿ�԰� ���� Ÿ���� �̸��� ���
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        // Prefab �� �ִ� �� ��������. ResourceManager.cs �� �̹� prefab ������ ������ UI ���� path ����
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        // UI_Button.cs component ��������. �˾� ���ÿ� �ֱ�
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI; // Ȥ�� �ٸ����� �̿��� ���� ������

        // ������ ���� popup ��ü ���Ƴ��� �θ� (ã�ų� ����) �������ֱ�
        go.transform.SetParent(Root.transform);

        

        return sceneUI;
    }

    /**
     * <summary>PopupStack �� �ֱ�</summary>
     * <param name="name">Prefab/UI/Popup/UI_Button ��ü �̸�</param>
     * <returns>T Scripts/UI/Popup/UI_Button.cs ��ũ��Ʈ</returns>
     */
    public T ShowPopupUI<T>(string name= null) where T: UI_Popup
    {
        // �̸� �� �޾����� T Ÿ�԰� ���� Ÿ���� �̸��� ���
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        // Prefab �� �ִ� �� ��������. ResourceManager.cs �� �̹� prefab ������ ������ UI ���� path ����
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        // UI_Button.cs component ��������. �˾� ���ÿ� �ֱ�
        T popup = Util.GetOrAddComponent<T>(go); 
        _popupStack.Push(popup);

        // _order++ ? Unity ���� ���� ����� ���� ��� ó���� �� �Ǵ� ������ �ִ�.> UI_Popup.cs ���� ����

        // �ߺ� �ڵ尡 �����־��� (22.3.30)
        // ������ ���� popup ��ü ���Ƴ��� �θ� (ã�ų� ����) �������ֱ�
        go.transform.SetParent(Root.transform);


        return popup;
    }



    /**
     * <summary>�������� ������ Popup �ݱ�. ���� ����� �������� �ʴ´�.</summary>
     * ���ǿ����� public ���� ��
     */ 
    protected void ClosePopupUI()
    {
        // Stack �� �ƹ��͵� ������ Ȯ��
        if(_popupStack.Count == 0)
        {
            return;
        }

        // �������� ������ �˾��� �θ� ��ü�� ����. �� �̻� �����ϸ� �� �Ǵ� null.
        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destoy(popup.gameObject);
        popup = null;

        _order--;
    }

    /**
     * <summary>���� popup �� �߰� ������ popup �� �����Ϸ��� �õ� ����. </summary>
     * <param name="popup">�����Ϸ��� �ǵ��� �˾��� �´��� Ȯ��</param>
     */
    public void ClosePopupUI(UI_Popup popup)
    {
        if(_popupStack.Count == 0)
        {
            return;
        }

        // popup�� �������� ������ �˾��� �ƴϸ� �������� �ʴ´�.
        if (_popupStack.Peek() != popup)  // �������� ������ popup ������. ������ �ʴ´�.
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }


    /**
     * <summary>��� popup �ݱ�</summary>
     */
    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

}
