using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [SerializeField] Toggle muteToggle;
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider sfxSlider;
    // [SerializeField] TMP_Text bgmVolText;
    // [SerializeField] TMP_Text sfxVolText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }

    }

    private void OnEnable() {
        
        for (int i = 0; i < audioManager.BgmList.Count; i++)
        {
            muteToggle.isOn = audioManager.BgmList[i].mute;
            bgmSlider.value = audioManager.BgmList[i].volume;
        }

        for (int i = 0; i < audioManager.SfxList.Count; i++)
        {
            muteToggle.isOn = audioManager.SfxList[i].mute;
            sfxSlider.value = audioManager.SfxList[i].volume;
        }


        // sfxSlider.value = audioManager.SfxVolume;
        // SetBgmVolText (bgmSlider.value);
        // SetSfxVolText (sfxSlider.value);
    }

    // public void SetBgmVolText(float value)
    // {
    //     bgmVolText.text = Mathf.RoundToInt (value * 100).ToString();
    // }

    //     public void SetSfxVolText(float value)
    // {
    //     sfxVolText.text = Mathf.RoundToInt (value * 100).ToString();
    // }

}
