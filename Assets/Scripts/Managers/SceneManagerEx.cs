using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @details Scene ����/���� ���� �Ѱ� [Game, Login]
 * @see Managers.cs, LoginScene.cs, 
 */
public class SceneManagerEx
{
    /**
     * @details ���� ������� Scene. �ٸ� �͵��� �������� ������ ����
     * LoginScene component �� object type ���� ��ȯ�ؼ� ���� ��
     */
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    //== FindObjectOfType(typeof(BaseScene)) as BaseScene

    public void LoadScene(Define.Scene type)
    {
        CurrentScene.Clear(); // ������ ������̴� Scene ����
        SceneManager.LoadScene(GetSceneName(type));
    }

    /**
     * @details enum to string by C# reflection
     * @see JAVA���� �̰��Ϸ��� �����ؾ� �ϴµ�
     */
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
}
