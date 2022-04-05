using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * @details type ���� Scene �����ϴ� �ֻ��� �θ�
 * @see Define.cs / enum Scene
 */
public abstract class BaseScene : MonoBehaviour
{
    // ������ ���, �ٲٴ°� �ڽĸ�
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    // Reset- Awake- OnEnable- Start- FixedUpdate(case by case)- ������ - LateUpdate ��.- OnDestroy
    void Awake()
    {
        Init();
    }


    /**
     * @details EventSystem ����üũ �� ��������
     * @see Scene Manager #1 12:56
     */
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if(obj == null)
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }


    public abstract void Clear();
}
