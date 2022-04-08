using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @details Sound 3���[AudioSource_������, AudioClip_����, AudioListener_��]
 * @see Define.cs
 */
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    /**
     * @details ��� Scene ���� �����ϴ� ���� @Sound ��ü
     * @see Managers.cs/ Init()
     */
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);   // ��������ñ��� �������� �ʰ�

            // Sound ���� ���� �� obj ���� AudioSource �ٿ��ְԲ� �Ѵ� (������ ���߿�)
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++) // Enum �� maxCount �־ �ϳ� ����
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }
    public void Play(Define.Sound type, string path, float pitch = 1.0f)
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"Audio Clip is Missing !: {path}");
                return;
            }
        }
        else
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"Audio Clip is Missing !: {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip); // �����÷��̾�� ������ Ʋ��޶�.
        }
    }

    /**
     * @see https://ansohxxn.github.io/unity%20lesson%202/ch9-1/
     */
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }
}