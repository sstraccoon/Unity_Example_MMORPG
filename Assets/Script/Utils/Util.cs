using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{

    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transfrom = FindChild<Transform>(go, name, recursive);
        if (transfrom == null)
            return null;
        return transfrom.gameObject;
    }

    // go의 자식 오브젝트의 이름으로 name을 검사해서 component를 찾는다.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        // 재귀 아닌 상태
        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        // 재귀 상태
        else
        {
            // GetComponentsInChildren<T>를 통해 모든 자식의 컴포넌트를 추출
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
}
