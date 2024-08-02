using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사운드를 관리 함, 생성, 삭제, 플레이까지 관리
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // 사운드의 최상위 객체를 만든다.
    // Define으로 설정한 sound enum의 내용을 가져와 이름대로 gameobject로 만들고 AudioSource를 붙여 root 하위에 넣어 준다.
    // _audioSources 배열에 속성을 넣어준다. (관리 할 수 있도록)
    // bgm은 loop 속성을 켜준다.
    public void Init() {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    // _audioSource 배열에서 clip(mp3)를 비워준다.
    // 재생 중지 (왜 이게 먼저가 아닐까??
    // _audioClip을 비워준다.
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    // 하나의 함수에서 다른 함수를 복사해서 늘리지 않고 불러서 해결하는 방법을 추천
    // 아
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAuidoClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    // 저장된 오디오인지 확인 (캐싱)
    AudioClip GetOrAddAuidoClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sound/") == false)
            path = $"Sound/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;

    }
}
