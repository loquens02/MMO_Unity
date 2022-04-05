using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @details type ���� Scene �����ϴ� �ֻ��� �θ�
 * @see Define.cs / enum Scene
 */
public abstract class BaseScene : MonoBehaviour
{
    // ������ ���, �ٲٴ°� �ڽĸ�
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Start()
    {
        
    }


    protected virtual void Init()
    {

    }


    public abstract void Clear();
}
