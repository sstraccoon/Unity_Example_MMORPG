using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMangerEx
{
    public BaseScene CurrentScene{  get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // �� �ҷ�����
   public void LoadScene(Define.Scene type)
    {
        Managers.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    // Define���� ���� enum ���� �̸��� �������� ���ؼ� ���
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
