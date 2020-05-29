using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPicker : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;
    AudioSource a;
    private void Start() {
        print(sounds.Length);
        a = GetComponent<AudioSource>();
        int r = (int) Mathf.Round(UnityEngine.Random.Range(0, sounds.Length));
        a.PlayOneShot(sounds[r]);
    }
}
