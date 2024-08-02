using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip audioClp;
    public AudioClip audioClp2;

    int i = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("box check");
        i++;

        if (i % 2 == 0)
            Managers.Sound.Play(audioClp, Define.Sound.Bgm);
        else
            Managers.Sound.Play(audioClp2, Define.Sound.Bgm);
    }
}
