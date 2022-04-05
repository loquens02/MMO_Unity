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
        // TEMP - 키보드 입력 하드코딩
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // SceneManager.LoadSceneAsync - 무거운 scene 은 로그인 중에 미리 조금씩 받아놓을 수 있다.
            SceneManager.LoadScene("Game");
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}
