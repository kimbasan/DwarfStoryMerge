using UnityEngine;

public class HeroSounds : CharacterSounds
{
    [SerializeField] private AudioClip running;

    public void Running()
    {
        audioSource.clip= running;
        audioSource.loop= true;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
        audioSource.loop= false;
    }
}
