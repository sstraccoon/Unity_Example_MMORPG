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

    // go�� �ڽ� ������Ʈ�� �̸����� name�� �˻��ؼ� component�� ã�´�.
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        // ��� �ƴ� ����
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
        // ��� ����
        else
        {
            // GetComponentsInChildren<T>�� ���� ��� �ڽ��� ������Ʈ�� ����
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
}
