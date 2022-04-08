using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @details Manager ���հ���
 */
public class Managers : MonoBehaviour
{
    static Managers s_instance; // �ϳ���
    static Managers Instance { get{ init();  return s_instance; } } // Singleton �Ŵ���
    
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
        //Instance = this; // Instance ä���ֱ�. �Ź� �ڱ��ڽ��� ����� ��� -> �ּ�
    }

    void Update()
    {
        Input.OnUpdate();
    }
    /**
     * @details �� Manager Init() (Init �� �ִ� ��쿡��)
     * ����: ���⼭ Instance �� �θ��� ���� ���.
     */
    static void init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            // Unity �� @Managers ��� ��ü�� ������ ������ش�.
            if(go == null)
            {
                go = new GameObject { name = "@Managers" }; // empty GameObject set name
                go.AddComponent<Managers>(); // �� ������Ʈ�� Managers ��ũ��Ʈ �߰�
            }

            DontDestroyOnLoad(go); // �ظ��ؼ��� ���� �� �ǵ���
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
        }
        
    }
}
