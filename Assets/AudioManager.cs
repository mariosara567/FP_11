using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static List <AudioSource> bgmInstance;
    static List <AudioSource> sfxInstance;
    static List <AudioSource> muteInstance;
    // [SerializeField] AudioSource bgm;
    // [SerializeField] AudioSource sfx;
    [SerializeField] List <AudioSource> bgmList;
    [SerializeField] List <AudioSource> sfxList;
    
    


    // public bool IsMute { get => bgm.mute; }
    // public float BgmVolume { get => bgm.volume;}
    // public float SfxVolume { get => sfx.volume;}
    public List<AudioSource> SfxList { get => sfxList;}
    public List<AudioSource> BgmList { get => bgmList;}

    private void Start() 
    {
        // bgm.mute = PlayerPrefs.GetInt("saveMute") == 1 ? true : false;
        // bgm.volume = PlayerPrefs.GetFloat("saveBgmValue");

        
        for (int i = 0; i < bgmList.Count; i++)
        {
            BgmList[i].volume = PlayerPrefs.GetFloat("saveBgmValue");
            BgmList[i].mute = PlayerPrefs.GetInt("saveMute") == 1 ? true : false;
        }

        for (int i = 0; i < SfxList.Count; i++)
        {
            SfxList[i].volume = PlayerPrefs.GetFloat("saveSfxValue");
            sfxList[i].mute = PlayerPrefs.GetInt("saveMute") == 1 ? true : false;
        }
        

        for (int i = 0; i < bgmList.Count; i++)
        {
            if (bgmInstance[i] != null)
            {
                Destroy(bgmList[i].gameObject);
                bgmList[i] = bgmInstance[i];
            }
            else
            {
                bgmInstance[i] = bgmList[i];
                bgmList[i].transform.SetParent(null);
                DontDestroyOnLoad(bgmList[i].gameObject);
            }
        }

        for (int i = 0; i < sfxList.Count; i++)
        {
            if (sfxInstance[i] != null)
            {
                Destroy(sfxList[i].gameObject);
                sfxList[i] = sfxInstance[i];
            }
            else
            {
                sfxInstance[i] = sfxList[i];
                sfxList[i].transform.SetParent(null);
                DontDestroyOnLoad(sfxList[i].gameObject);
            }
        }


    //     if (bgmInstance != null)
    //     {
    //         Destroy(bgm.gameObject);
    //         bgm = bgmInstance;
    //     }
    //     else
    //     {
    //         bgmInstance = bgm;
    //         bgm.transform.SetParent(null);
    //         DontDestroyOnLoad(bgm.gameObject);
    //     }

    //     for (int i = 0; i < sfxList.Count; i++)
    //     {
    //         if (sfxInstance != null)
    //         {
    //             Destroy(sfxList[i].gameObject);
    //             sfxList[i] = sfxInstance;
    //         }
    //         else
    //         {
    //             sfxInstance = sfxList[i];
    //             sfxList[i].transform.SetParent(null);
    //             DontDestroyOnLoad(sfxList[i].gameObject);
    //         }
    //     }

        
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        for (int i = 0; i < BgmList.Count; i++)
        {
            if (bgmList[i].isPlaying)
            {
                bgmList[i].Stop();
            }

            bgmList[i].clip = clip;
            bgmList[i].loop = loop;
            bgmList[i].Play();

        }
    }

    public void PlaySFX(AudioClip clip)
    {
        for (int i = 0; i < SfxList.Count; i++)
        {
            if (SfxList[i].isPlaying)
            {
                SfxList[i].Stop();
            }
        
        SfxList[i].clip = clip;
        SfxList[i].Play();

        }
        
    }

    public void SetMute(bool value)
    {
        for (int i = 0; i < BgmList.Count; i++)
        {
            bgmList[i].mute = value;

            PlayerPrefs.SetInt("saveMute", BgmList[i].mute ? 1 : 0);
        }

        for (int i = 0; i < SfxList.Count; i++)
        {
            SfxList[i].mute = value;
            PlayerPrefs.SetInt("saveMute", sfxList[i].mute ? 1 : 0);
        }

        
    }

    public void SetBgmVolume(float value)
    {
        for (int i = 0; i < BgmList.Count; i++)
        {
        bgmList[i].volume = value;
        PlayerPrefs.SetFloat("saveBgmValue", BgmList[i].volume);
        }
    }

        public void SetSfxVolume(float value)
    {
        for (int i = 0; i < SfxList.Count; i++)
        {
        SfxList[i].volume = value;
         PlayerPrefs.SetFloat("saveSfxValue", SfxList[i].volume);
        // sfx.volume = sfxList[i].volume;
       
        }
        
    }
}
