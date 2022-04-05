using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @details type 으로 Scene 관리하는 최상위 부모
 * @see Define.cs / enum Scene
 */
public abstract class BaseScene : MonoBehaviour
{
    // 누구나 얻고, 바꾸는건 자식만
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    void Start()
    {
        
    }


    protected virtual void Init()
    {

    }


    public abstract void Clear();
}
