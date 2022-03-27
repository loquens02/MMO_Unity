using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    /**
     * @details �ش� ��ü�� �ڽİ�ü�� (���������) ã�´�
     * @param[recursive] true: ��������� ã��
     * @see GetComponentsInChildren https://docs.unity3d.com/kr/530/ScriptReference/Component.GetComponentsInChildren.html
     * @see SerializedProperty.CountInProperty (with parent)
     */
    public static T FindChild<T> (GameObject go, string name= null, bool recursive=false) where T: UnityEngine.Object
    {
        if(go== null)
        {
            return null;
        }

        // �ֻ��� �ڽ� Component �� ��������
        if (recursive == false)
        {
            for(int i=0; i<go.transform.childCount; i++) // �ڽ� ���� ��ŭ
            {
                // ù ��° �ڽĸ� ã��
                // Transform �� ��ġ���� �ϴ� ������ Unity ���� child ��⿡ ���� ���� ��ü��. (�ϳ��� �ֱ� �ѵ� ��ó�� �ʿ�)
                Transform transform=  go.transform.GetChild(0);

                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    // Transform �� �ƴ϶� Component �� �ʿ��ϴϱ�
                    T component= transform.GetComponent<T>();
                    return component;
                }
            }
        }
        else // ���� �ڽı��� �� �������� == GetComponentsInChildren
        {
            // gameObject �� ������ �ִ� T type�� component �� ���� �� ��ĵ�ϱ�
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // �̸��� ��� T type�� ���� ��ȯ�ϰ�, ���ϴ� �̸��� ������ ���� �̸��� ���� ��
                if(string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }

        }

        return null;
    }
}