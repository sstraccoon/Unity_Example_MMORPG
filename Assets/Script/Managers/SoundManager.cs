using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���带 ���� ��, ����, ����, �÷��̱��� ����
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    // ������ �ֻ��� ��ü�� �����.
    // Define���� ������ sound enum�� ������ ������ �̸���� gameobject�� ����� AudioSource�� �ٿ� root ������ �־� �ش�.
    // _audioSources �迭�� �Ӽ��� �־��ش�. (���� �� �� �ֵ���)
    // bgm�� loop �Ӽ��� ���ش�.
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

    // _audioSource �迭���� clip(mp3)�� ����ش�.
    // ��� ���� (�� �̰� ������ �ƴұ�??
    // _audioClip�� ����ش�.
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    // �ϳ��� �Լ����� �ٸ� �Լ��� �����ؼ� �ø��� �ʰ� �ҷ��� �ذ��ϴ� ����� ��õ
    // ��
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

    // ����� ��������� Ȯ�� (ĳ��)
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
