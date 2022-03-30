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

        // GridPanel �ؿ� ���� �͵� ������ ����
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destoy(child.gameObject);
        }

        // ���Ƿ� 8�� �߰��ؼ� GridPanel �ؿ� �־��ֱ�. TODO �κ��丮 ���� ����
        for(int i=0; i<8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item"); // �κ��丮 �ǽ�.
            item.transform.SetParent(gridPanel.transform);


            // (�ϵ��ڵ� ����) ������ �̸� �ٲٱ�
            // �ڵ�����- item ���� �ٷ� GetOrAddComponent ���ֱ� > Extension this 
            // UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"�����{i}��");
        }
    }
}
/**
 // ��Ÿ����! enum ��Ҹ� ���ε����ִ� �Ŵ� enum �̸��� �����;�
        Bind<GameObject>(typeof(GameObjects));

        // GridPanel �ؿ� ���� �͵� ������ ����
        // ����1 ä��. Error ���� �α״� ���µ�, ������ ���� �ʴ´�. ����1. ������ �ϴµ� �ٽ� �����Ѵ�? ����2. ������ �� �Ѵ�?
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            
            Managers.Resource.Destoy(child.gameObject);
            //Debug.Log($"test-���� obj ����: {child.gameObject.name}");
        }

        // TODO ���� �κ��丮 ���� �����ؼ�(���߿�).
        // ���Ƿ� 8�� �߰��ؼ� GridPanel �ؿ� �־��ֱ�
        for(int i=0; i<8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item"); // �κ��丮 �ǽ�.
            //Error- ���� ������ �ϰ�, SetParent �� �������� �ʴ´�.
            item.transform.SetParent(gridPanel.transform);
            //item.transform.SetParent(null); << �̰� �����غ��� ������ ��Ȯ������.

            //Debug.Log($"test- ������ {item.name}, ��� �θ� Set�� ���ִ���: {gridPanel.transform.name}");
            // test- ������ UI_Inven_Item(Clone), ��� �θ� Set�� ���ִ���: GridPanel > ���� �α� 8�� �߻�. 

            // (�ϵ��ڵ� ����) ������ �̸� �ٲٱ�
            //UI_Inven_Item invenItem = Util.GetOrAddComponent<UI_Inven_Item>(item);
            // item ���� �ٷ� GetOrAddComponent ���ֱ� > Extension this 
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"�����{i}��");
        }


 */