using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manager ���հ���
 */
public class Managers : MonoBehaviour
{
    static Managers s_instance; // �ϳ���
    static Managers Instance { get{ init();  return s_instance; } } // Singleton �Ŵ���
    
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    // Start is called before the first frame update
    void Start()
    {
        init();
        //Instance = this; // Instance ä���ֱ�. �Ź� �ڱ��ڽ��� ����� ��� -> �ּ�
    }

    // Update is called once per frame
    void Update()
    {
        Input.OnUpdate();
    }
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
        }
        
    }
}
