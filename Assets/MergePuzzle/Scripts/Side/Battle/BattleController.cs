using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private SideMovementRuntimeSet activeSideMovement;
    [SerializeField] private CharacterAnimationSet enemyAnimations;

    [Header("Battle")]
    [SerializeField] private BattleTrigger encounter;
    [SerializeField] private PartyController heroParty;

    [Header("Slots")]
    [SerializeField] private BattleSlot battleSlot;
    [SerializeField] private SupportSlot supportSlot;
    [SerializeField] private List<Slot> allSlots;

    [Header("Debug")]
    [SerializeField] private EnemyController currentEnemy;

    private int enemiesBeaten;

    private void Awake()
    {
        enemiesBeaten= 0;
        encounter.EnemyEncountered += Encounter_EnemyEncountered;   

        foreach(var slot in allSlots)
        {
            slot.MergeHappened += Slot_MergeHappened;
        }

        battleSlot.ItemUsed += BattleSlot_ItemUsed;
        supportSlot.SupportUsed += SupportSlot_SupportUsed;
    }

    private void SupportSlot_SupportUsed(object sender, EventArgs e)
    {
        SupportEventArgs args = (SupportEventArgs)e;
        heroParty.AddToHP(args.GetItemType(), args.GetLevel());
    }

    private void BattleSlot_ItemUsed(object sender, EventArgs e)
    {
        StartCoroutine(HeroAttacks());
    }

    private IEnumerator HeroAttacks() {
        heroParty.Attack();
        yield return new WaitForSeconds(1f);
        bool isDead = currentEnemy.TakeHit();
        if (isDead)
        {
            EndBattle();
        } else
        {
            UpdateBattleSlotRequirements();
        }
    }



    private void Slot_MergeHappened(object sender, EventArgs e)
    {
        var type = ((MergeEventArgs)e).GetMergeType();
        if (type == ItemType.Coin || type == ItemType.Food || type == ItemType.Shield) { 
            // do nothing
        } else
        {
            if (currentEnemy != null)
            {
                EnemyAttack();
            }
            else
            {
                Debug.Log("Free merge");
            }
        }
    }

    private void Encounter_EnemyEncountered(object sender, EventArgs e)
    {
        BattleEventArgs battleArgs = (BattleEventArgs)e;
        var enemy = battleArgs.GetEnemy();
        if (enemy != null)
        {
            currentEnemy = enemy;
            StartBattle(enemy);
        } else
        {
            Debug.LogError("Enemy not found!");
        }
        
    }

    private void StartBattle(EnemyController enemy)
    {
        // stop movement
        List<SideMovement> activeList = activeSideMovement.GetList();
        foreach(var sideMovement in activeList)
        {
            sideMovement.StopMoving();
        }

        // update animations on characters
        heroParty.Stop();

        // update animation on enemies
        enemy.Stop();
        foreach (var enemyAnimator in enemyAnimations.GetList())
        {
            enemyAnimator.StopRunning();
        }
        UpdateBattleSlotRequirements();
    }

    private void UpdateBattleSlotRequirements()
    {
        var itemRequired = currentEnemy.GetRequiredItem();
        battleSlot.SetRequiredItem(itemRequired.GetItemType(), itemRequired.GetLevel());
    }

    private void EnemyAttack()
    {
        StartCoroutine(EnemyAttacks());
    }

    private IEnumerator EnemyAttacks()
    {
        var damage = currentEnemy.Attack();
        yield return new WaitForSeconds(1f);
        heroParty.TakeDamage(damage);
    }

    private void EndBattle()
    {
        currentEnemy.Die();
        currentEnemy = null;
        var list = activeSideMovement.GetList();
        foreach(var sideMovement in list)
        {
            sideMovement.StartMoving();
        }

        heroParty.Run();
        enemiesBeaten++;
        if (enemiesBeaten > 5)
        {
            var difficulty = PlayerPrefs.GetInt(Constants.DIFFICULTY_MULTIPLYER);
            difficulty++;
            PlayerPrefs.SetInt(Constants.DIFFICULTY_MULTIPLYER, difficulty);
            enemiesBeaten = 0;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(Constants.DIFFICULTY_MULTIPLYER, 0);
    }
}
