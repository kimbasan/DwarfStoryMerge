using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clipList;
    [SerializeField] private AudioSource source;

    private void Awake()
    {
        var clip = clipList[Random.Range(0, clipList.Count)];
        source.clip = clip;
        source.Play();
    }
}
