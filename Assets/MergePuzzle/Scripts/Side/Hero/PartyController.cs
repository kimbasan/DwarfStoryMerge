using System;
using System.Collections;
using UnityEngine;

public class PartyController : MonoBehaviour
{
    [SerializeField] private PlayerLeveling leveling;
    [SerializeField] private CharacterAnimationSet heroAnimations;
    [SerializeField] private Health partyHealth;

    public event EventHandler HeroDied;
    private void Awake()
    {
        var startHealth = leveling.healthLevels[PlayerPrefs.GetInt(Constants.HEALTH_LEVEL)];
        if (PlayerPrefs.GetInt(Constants.ELF_UNLOCK_BOOL) > 0)
        {
            startHealth += leveling.elfUnlockHealth;
        }
        if (PlayerPrefs.GetInt(Constants.HUMAN_UNLOCK_BOOL) > 0)
        {
            startHealth += leveling.humanUnlockHealth;
        }
        var startArmor = leveling.startingArmorLevels[PlayerPrefs.GetInt(Constants.START_ARMOR_LEVEL)];
        var maxArmor = leveling.maxArmorLevels[PlayerPrefs.GetInt(Constants.MAX_ARMOR_LEVEL)];

        partyHealth.Init(startHealth, maxArmor, startArmor);
    }

    public void Attack()
    {
        var heroes = heroAnimations.GetList();
        foreach (var hero in heroes)
        {
            hero.Attack();
        }
    }

    public void Stop()
    {
        var list = heroAnimations.GetList();
        foreach (var hero in list)
        {
            hero.StopRunning();
        }
    }

    public void Run()
    {
        var list = heroAnimations.GetList();
        foreach (var hero in list)
        {
            hero.StartRunning();
        }
    }

    public void TakeDamage(float damage)
    {
        bool isDead = partyHealth.TakeDamage(damage);
        var list = heroAnimations.GetList();
        if (isDead)
        {
            // show dead animation
            foreach (var hero in list)
            {
                hero.Die();
            }

            if (HeroDied != null)
            {
                StartCoroutine(GameOver());
            }
        } else
        {
            // show take damage animation
            
            foreach (var hero in list)
            {
                hero.TakeDamage();
            }
        }

    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        HeroDied.Invoke(this, EventArgs.Empty);
    }

    public void AddToHP(ItemType type, int level)
    {
        partyHealth.AddHP(type, level);
    }


}
