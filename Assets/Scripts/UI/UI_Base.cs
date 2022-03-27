using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        String[] names = Enum.GetNames(type); // enum 속 요소들
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // 요소 목록
        _objects.Add(typeof(T), objects); // key: 어떤 객체 유형을 Bind 할건지. value: 요소 목록

        // enum 요소 이름에 대응하는 개체 찾기
        for (int i = 0; i < names.Length; i++)
        {
            //GameObject 전용 Find
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else // Component Find
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true); // gameObject: 아무것도 안 불러도 되는 최상위 개체
            }

            // 아무것도 못 찾을 경우
            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind : [{names[i]}]");
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false) //꺼내는 데 실패하면 종료
        {
            return null;
        }

        // 성공하면 인덱스 위치의 무언가(Object) 를 T 타입으로 형변환해서 반환
        return objects[idx] as T;
    }

    protected Text GetText(int idx)
    {
        return Get<Text>(idx);
    }
    protected Button GetButton(int idx)
    {
        return Get<Button>(idx);
    }
    protected Image GetImage(int idx)
    {
        return Get<Image>(idx);
    }
}
