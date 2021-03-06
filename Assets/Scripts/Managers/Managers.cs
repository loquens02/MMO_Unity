using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>Manager 통합관리</summary> 
 */
public class Managers : MonoBehaviour
{
    static Managers s_instance; // 하나만
    static Managers Instance { get{ init();  return s_instance; } } // Singleton 매니저
    
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    
    public static UIManager UI { get { return Instance._ui; } }

    void Start()
    {
        init();
        //Instance = this; // Instance 채워주기. 매번 자기자신을 덮어쓰면 곤란 -> 주석
    }

    void Update()
    {
        Input.OnUpdate();
    }
    /**
     * <summary>각 Manager Init() (Init 이 있는 경우에만)</summary>  
     * <remarks>여기서 Instance 를 부르면 무한 재귀.</remarks>
     */
    static void init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            // Unity 에 @Managers 라는 객체가 없으면 만들어준다.
            if(go == null)
            {
                go = new GameObject { name = "@Managers" }; // empty GameObject set name
                go.AddComponent<Managers>(); // 빈 오브젝트에 Managers 스크립트 추가
            }

            DontDestroyOnLoad(go); // 왠만해서는 삭제 안 되도록
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
        }
        
    }
}
