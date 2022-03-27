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
        String[] names = Enum.GetNames(type); // enum �� ��ҵ�
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // ��� ���
        _objects.Add(typeof(T), objects); // key: � ��ü ������ Bind �Ұ���. value: ��� ���

        // enum ��� �̸��� �����ϴ� ��ü ã��
        for (int i = 0; i < names.Length; i++)
        {
            //GameObject ���� Find
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else // Component Find
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true); // gameObject: �ƹ��͵� �� �ҷ��� �Ǵ� �ֻ��� ��ü
            }

            // �ƹ��͵� �� ã�� ���
            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind : [{names[i]}]");
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false) //������ �� �����ϸ� ����
        {
            return null;
        }

        // �����ϸ� �ε��� ��ġ�� ����(Object) �� T Ÿ������ ����ȯ�ؼ� ��ȯ
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
