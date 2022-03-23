using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object // 템플릿 클래스 조건
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent= null)
    {
        // Prefab 가져오기
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab == null)
        {
            Debug.Log($"Failed to load the prefab : {path} ");
            return null;
        }

        return Object.Instantiate(prefab, parent); // prefab 을 생성해서 붙일 parent
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
