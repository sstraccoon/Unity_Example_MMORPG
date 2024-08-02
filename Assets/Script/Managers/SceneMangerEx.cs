using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangerEx
{
    public BaseScene CurrentScene{  get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // 씬 불러오기
   public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    // Define에서 만든 enum 값의 이름을 가져오기 위해서 사용
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
