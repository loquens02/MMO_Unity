using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>Sound 3요소[AudioSource_실행기기, AudioClip_음원, AudioListener_귀]</summary>
 * <see cref="Define"/>
 */
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    /**
     * <summary>어느 Scene 에나 존재하는 전역 @Sound 객체</summary> 
     * <see cref="Managers"/>Init()
     */
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);   // 게임종료시까지 없어지지 않게

            // Sound 종류 별로 새 obj 만들어서 AudioSource 붙여주게끔 한다 (음원은 나중에)
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++) // Enum 에 maxCount 있어서 하나 빼줌
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }
    /**
     * <param name="path">음원파일명. 필수</param>
     */
    public void Play(string path, Define.Sound type= Define.Sound.Effect, float pitch = 1.0f)
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }

        // 배경음악
        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Debug.Log($"Audio Clip is Missing !: {path}");
                return;
            }

            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            // 다른 sound 가 실행중이었다면 끄고
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
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
            audioSource.PlayOneShot(audioClip); // 뮤직플레이어에게 음원을 틀어달라.
        }
    }

    /**
     * <see cref="https://ansohxxn.github.io/unity%20lesson%202/ch9-1/"/> 
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
