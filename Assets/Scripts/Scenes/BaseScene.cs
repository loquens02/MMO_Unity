using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * <summary>type ���� Scene �����ϴ� �ֻ��� �θ�</summary>
 * <see cref="Define" langword="enum Scene"/>
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
     * <summary>EventSystem ����üũ �� ��������</summary>
     * <see langword="Scene Manager #1 12:56"/>
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
