using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] private List<AudioClip> attack;
    [SerializeField] private List<AudioClip> damaged;
    [SerializeField] private List<AudioClip> death;

    protected AudioClip PickRandom(List<AudioClip> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public void Attack()
    {
        audioSource.clip= PickRandom(attack);

        audioSource.Play();
    }

    public void Damaged()
    {
        audioSource.clip= PickRandom(damaged);
        audioSource.Play();
    }

    public void Death()
    {
        audioSource.clip= PickRandom(death);
        audioSource.Play();
    }

}
