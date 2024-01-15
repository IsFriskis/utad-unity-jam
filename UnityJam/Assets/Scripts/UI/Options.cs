using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu;
    public AudioMixer audioMixer;
    public Slider musicSlider, soundSlider;
    public TextMeshProUGUI musicText, soundText;
    public TMP_Dropdown resolutionDropDown;
    public Toggle fullScreenToggle;
    private Resolution[] resolutions;
    [SerializeField] Button firstSelectedButton;
    [SerializeField] ButtonBehaviour buttonBehaviour;
    [Header("Audio Clips")]
    AudioSource audioSource;
    [SerializeField] AudioClip clickSound, closeMenuSound, openMenuSound;
    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    void Start(){
        firstSelectedButton.Select();
        audioSource.PlayOneShot(openMenuSound);
        float musicVolume; audioMixer.GetFloat("Music",out musicVolume);
        musicSlider.value=musicVolume;musicText.text=musicVolume.ToString("F2");
        float soundVolume; audioMixer.GetFloat("Sounds",out soundVolume);
        soundSlider.value=soundVolume;soundText.text=soundVolume.ToString("F2");
        fullScreenToggle.isOn=Screen.fullScreen;

        resolutions=Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> resolutionStrings=new List<string>();
        int currentResolution=0;
        for (int i = 0; i < resolutions.Length; i++){
            string option= resolutions[i].width+" x "+resolutions[i].height;
            resolutionStrings.Add(option);
            if(resolutions[i].width==Screen.currentResolution.width &&
               resolutions[i].height==Screen.currentResolution.height){
                currentResolution=i;
            }
        }
        resolutionDropDown.AddOptions(resolutionStrings);
        resolutionDropDown.value=currentResolution;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetMusic(float volume){
        audioSource.PlayOneShot(clickSound);
        float logVolume=Mathf.Log10(volume)*20;
        audioMixer.SetFloat("Music",logVolume);
        musicText.text=volume.ToString("F2");
    }
    public void SetSounds(float volume){
        audioSource.PlayOneShot(clickSound);
        float logVolume=Mathf.Log10(volume)*20;
        audioMixer.SetFloat("Sounds",logVolume);
        soundText.text=volume.ToString("F2");
    }
    public void SetFullScreen(bool isFullScreen){
        audioSource.PlayOneShot(clickSound);
        Screen.fullScreen=isFullScreen;
    }
    public void SetResolution(int resolutionIndex){
        audioSource.PlayOneShot(clickSound);
        Screen.SetResolution(resolutions[resolutionIndex].width,resolutions[resolutionIndex].height,Screen.fullScreen);
    } 
    public void ReturnToPauseMenu(){
        audioSource.PlayOneShot(closeMenuSound);
        optionsMenu.SetActive(false);
        if(pauseMenu != null){
            pauseMenu.SetActive(true);
            buttonBehaviour.firstSelectedButton.Select();
        }
    }
}
