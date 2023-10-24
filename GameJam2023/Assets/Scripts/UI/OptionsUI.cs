using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button closeButton;


    private Action onCloseButtonAction;

    private void Awake() {
        Instance = this;

        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });

    }

    private void Start() {
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;

        if (PlayerPrefs.HasKey("musicVolume")) {
            LoadVolume();
        } else {
            SetMusicVolume();
            SetSFXVolume();
        }

        Hide();
    }

    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    public void SetMusicVolume () {
        float musicVolume = musicSlider.value; // Take the value of the slider
        audioMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20); // Set music volume to audioMixer
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume () {
        float sfxVolume = sfxSlider.value; // Take the value of the slider
        audioMixer.SetFloat("Sfx", Mathf.Log10(sfxVolume) * 20); // Set music volume to audioMixer
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    private void LoadVolume () {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetMusicVolume();
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
