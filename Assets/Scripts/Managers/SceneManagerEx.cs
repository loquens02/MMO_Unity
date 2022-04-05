using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @details Scene 생성/삭제 관리 총괄 [Game, Login]
 * @see Managers.cs, LoginScene.cs, 
 */
public class SceneManagerEx
{
    /**
     * @details 현재 사용중인 Scene. 다른 것들을 가져오는 선봉대 역할
     * LoginScene component 를 object type 으로 변환해서 넣은 것
     */
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    //== FindObjectOfType(typeof(BaseScene)) as BaseScene

    public void LoadScene(Define.Scene type)
    {
        CurrentScene.Clear(); // 기존에 사용중이던 Scene 정리
        SceneManager.LoadScene(GetSceneName(type));
    }

    /**
     * @details enum to string by C# reflection
     * @see JAVA에서 이거하려면 삽질해야 하는데
     */
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
}
