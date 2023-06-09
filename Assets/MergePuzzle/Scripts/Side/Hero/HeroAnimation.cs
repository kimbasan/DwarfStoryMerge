using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField] private CharacterAnimationSet heroAnimations;
    [SerializeField] private CharacterAnimation thisHero;
    private void Awake()
    {
        heroAnimations.Add(thisHero);
    }
}
