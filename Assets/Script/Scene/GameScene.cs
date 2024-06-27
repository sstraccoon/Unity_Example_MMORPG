using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseSence
{

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();


        // TEMP
        //for (int i = 0; i < 9; i++)
        //    Managers.UI.ShowPopupUI<UI_Button>();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

}
