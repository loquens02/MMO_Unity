using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
    }
    

    private void Update()
    {
        // TEMP - Ű���� �Է� �ϵ��ڵ�
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // SceneManager.LoadSceneAsync - ���ſ� scene �� �α��� �߿� �̸� ���ݾ� �޾Ƴ��� �� �ִ�.
            SceneManager.LoadScene("Game");
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}
