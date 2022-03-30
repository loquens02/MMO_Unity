using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object // ���ø� Ŭ���� ����
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent= null)
    {
        // Prefab ��������
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab == null)
        {
            Debug.Log($"Failed to load the prefab : {path} ");
            return null;
        }

        // �ڵ� ����- Object �ڵ� ������ �̸��� 'Clone' �� �� �پ����� ���ڴ�
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if(index > 0)
        {
            go.name = go.name.Substring(0, index); // inplace ����. (Clone) ������ �������� �����
        }

        return go; // prefab �� �����ؼ� ���� parent
    }

    public void Destoy(GameObject go)
    {
        if(go == null)
        {
            return;
        }

        Object.Destroy(go);
    }

    public void Destoy(GameObject go, float sec)
    {
        if (go == null)
        {
            return;
        }

        Object.Destroy(go, sec);
    }
}
