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

        return Object.Instantiate(prefab, parent); // prefab �� �����ؼ� ���� parent
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
