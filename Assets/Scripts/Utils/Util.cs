using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    /**
     * <summary>component가 없다면 추가해서 반환</summary>
     */
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        // ??? 다형성 헷갈린다.
        // Transform 이랑 GameObject 는 별개네. Object 가 최상위고, GameObject 는 다른 것
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
        {
            return null;
        }
        return transform.gameObject;
    }

    /**
     * <summary>해당 개체의 자식개체를 (재귀적으로) 찾는다</summary>
     * <param name="recursive">true: 재귀적으로 찾기</param>
     * <see langword="GetComponentsInChildren" href="https://docs.unity3d.com/kr/530/ScriptReference/Component.GetComponentsInChildren.html"/>  
     * <see langword="SerializedProperty.CountInProperty (with parent)"/> 
     */
    public static T FindChild<T> (GameObject go, string name= null, bool recursive=false) where T: UnityEngine.Object
    {
        if(go== null)
        {
            return null;
        }

        // 최상위 자식 Component 만 가져오기
        if (recursive == false)
        {
            for(int i=0; i<go.transform.childCount; i++) // 자식 개수 만큼
            {
                // 첫 번째 자식만 찾기
                // Transform 은 위치방향 하는 거지만 Unity 에서 child 얻기에 가장 좋은 객체다. (하나더 있긴 한데 후처리 필요)
                Transform transform=  go.transform.GetChild(0);

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    // Transform 이 아니라 Component 가 필요하니까
                    T component= transform.GetComponent<T>();
                    return component;
                }
            }
        }
        else // 하위 자식까지 다 가져오기 == GetComponentsInChildren
        {
            // gameObject 가 가지고 있는 T type의 component 에 대해 쭉 스캔하기
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // 이름이 없어도 T type인 것을 반환하고, 원하는 이름이 있으면 같은 이름이 있을 때
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }

        }

        return null;
    }
}
