using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Inven_Item : UI_Base
{

    // enum의 내용이 별로 없다면 GameObjects로 뭉뚱그려도 된다.
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMP_Text>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"{_name}를 클릭 했습니다."); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
