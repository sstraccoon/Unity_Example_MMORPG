using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자원 (프리팹, 스프라이트, 텍스트, 게임 오브젝트 등) 을 관리 (생성, 삭제)하는 녀석.
public class ResourceManager
{
    // 와일드형(T)로 받아서 어떤 것도 받을 수 있다 (T는 Object 형식이어야만 한다, 즉 유니티의 오브젝트 하위 객체면 다 됨)
    public T Load<T>(string path) where T : Object
    {
        // 유니티의 오브젝트를 가져오는 내장함수 T 탑이기 때문에 어떤 것이든 가져온다.
        return Resources.Load<T>(path);
    }

    // 생성 함수
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 해당 주소에 있는 게임 오브젝트를 불어와서 만듬
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        // 호출 
        if (prefab == null)
        {
            Debug.Log("$Failed to load prefab : {prefab}");
            return null;
        }

        // 생성 후 이름 수정 (Clone 제거)
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
