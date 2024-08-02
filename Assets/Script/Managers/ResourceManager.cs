using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ڿ� (������, ��������Ʈ, �ؽ�Ʈ, ���� ������Ʈ ��) �� ���� (����, ����)�ϴ� �༮.
public class ResourceManager
{
    // ���ϵ���(T)�� �޾Ƽ� � �͵� ���� �� �ִ� (T�� Object �����̾�߸� �Ѵ�, �� ����Ƽ�� ������Ʈ ���� ��ü�� �� ��)
    public T Load<T>(string path) where T : Object
    {
        // ����Ƽ�� ������Ʈ�� �������� �����Լ� T ž�̱� ������ � ���̵� �����´�.
        return Resources.Load<T>(path);
    }

    // ���� �Լ�
    public GameObject Instantiate(string path, Transform parent = null)
    {
        // �ش� �ּҿ� �ִ� ���� ������Ʈ�� �Ҿ�ͼ� ����
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        // ȣ�� 
        if (prefab == null)
        {
            Debug.Log("$Failed to load prefab : {prefab}");
            return null;
        }

        // ���� �� �̸� ���� (Clone ����)
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
