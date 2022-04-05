using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/**
 * @details type 으로 Scene 관리하는 최상위 부모
 * @see Define.cs / enum Scene
 */
public abstract class BaseScene : MonoBehaviour
{
    // 누구나 얻고, 바꾸는건 자식만
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    // Reset- Awake- OnEnable- Start- FixedUpdate(case by case)- ……… - LateUpdate ….- OnDestroy
    void Awake()
    {
        Init();
    }


    /**
     * @details EventSystem 유무체크 후 가져오기
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
