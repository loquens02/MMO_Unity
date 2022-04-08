using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * <summary>Scene ����/���� ���� �Ѱ� [Game, Login]</summary>
 * <see cref="Managers"/>
 * <see cref="LoginScene"/>
 */
public class SceneManagerEx
{
    /**
     * <summary>���� ������� Scene. �ٸ� �͵��� �������� ������ ����</summary>
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
     * <summary>enum to string by C# reflection</summary>
     */
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }
}
