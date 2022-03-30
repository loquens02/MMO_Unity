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

        // 코드 정리- Object 자동 생성시 이름에 'Clone' 이 안 붙었으면 좋겠다
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if(index > 0)
        {
            go.name = go.name.Substring(0, index); // inplace 유의. (Clone) 나오기 전까지만 남기기
        }

        return go; // prefab 을 생성해서 붙일 parent
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
