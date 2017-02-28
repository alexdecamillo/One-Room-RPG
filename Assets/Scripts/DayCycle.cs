using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    public string time = "Day";

    public Sprite day;
    public Sprite night;
    public AudioClip fighting;
    public AudioClip dayMusic;

    public GameObject flash;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void SetDay()
    {
        GetComponent<SpriteRenderer>().sprite = day;
        flash.SetActive(false);
        audio.clip = dayMusic;
        audio.Play();
        time = "Day";
    }

    public void SetNight()
    {
        
        GetComponent<SpriteRenderer>().sprite = night;
        flash.SetActive(true);
        audio.clip = fighting;
        audio.Play();
        time = "Night";
    }
}
