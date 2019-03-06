using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();

    public AudioClip[] audioClipArray;
    public AudioSource audioSource;
    [NonSerialized] public bool isMusicQuit = false;
    [NonSerialized] public bool isAudioQuit = false;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        if (audioClipArray.Length > 0)
        {
            foreach (AudioClip audioClip in audioClipArray)
            {
                audioDict.Add(audioClip.name, audioClip);
            }
        }

        UpdateAudio();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAudio()
    {
        string temp = PlayerPrefs.GetString("IsMusic", "false");
        isAudioQuit = isMusicQuit = temp == "true";

        this.audioSource.playOnAwake = !isMusicQuit;
        this.audioSource.loop = !isMusicQuit;

        this.audioSource.enabled = false;
        this.audioSource.enabled = true;
    }

    public void Play(string audioName)
    {
        if (isAudioQuit) return;

        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            this.audioSource.PlayOneShot(ac);
        }
    }

    public void Play(string audioName, AudioSource audioSource)
    {
        if (isAudioQuit) return;

        AudioClip ac;
        if (audioDict.TryGetValue(audioName, out ac))
        {
            audioSource.PlayOneShot(ac);
        }
    }
}