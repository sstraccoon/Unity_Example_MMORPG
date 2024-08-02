using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    void Start()
    {
        base.Init();

        SceneType = Define.Scene.Login;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Game");
        }
    }
    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }
}
