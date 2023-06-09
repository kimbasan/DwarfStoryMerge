using Mono.Cecil;
using System.Collections.Generic;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField] private ItemFactory startingItemsFactory;

    [SerializeField] private GameObject spawnerPrefab;

    [SerializeField] List<Slot> slotList;
    private Slot[,] slots = new Slot[6,16];

    [SerializeField] private List<Slot> freeSlots = new List<Slot>();

    [SerializeField]
    private int sizeX;
    [SerializeField] 
    private int sizeY;

    private void Awake()
    {
        sizeX = slots.GetUpperBound(0) + 1;
        sizeY = slots.Length / sizeX;
        if (slotList != null) {

            PopulateArray();
        }
        // set number of free spaces
        int startField = 16;
        if (PlayerPrefs.GetInt(Constants.ELF_UNLOCK_BOOL) > 0)
        {
            startField += 6;
        }
        if (PlayerPrefs.GetInt(Constants.HUMAN_UNLOCK_BOOL) > 0)
        {
            startField += 6;
        }
        InitField(startField);
    }

    private void PopulateArray()
    {
        var listEnumerator = slotList.GetEnumerator();
        listEnumerator.MoveNext();
        for (int i = 0; i < sizeX; i++)
        {
            for (int y = 0; y < sizeY; y++)
            {
                slots[i,y] = listEnumerator.Current;
                listEnumerator.MoveNext();
            }
        }
    }

    public void InitField(int numberOfFreeSlots)
    {
        Debug.Log("Size x " + sizeX+ " y " + sizeY);
        int pointX = Random.Range(sizeX / 4, (sizeX * 3) / 4);
        int pointY = Random.Range(sizeY / 4, (sizeY * 3) / 4);

        Slot center = slots[pointX, pointY];

        int slotsToFill = numberOfFreeSlots;

        FreeUpSlot(center);
        slotsToFill--;
        Debug.Log($"x y {pointX} {pointY}");
        
        int positionDiff = 1;
        do
        {
            if (pointX - positionDiff >= 0 && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX - positionDiff, pointY]);
                slotsToFill--;
                //Debug.Log($"x y {pointX - i} {pointY}");
            }
            if (pointX + positionDiff < sizeX && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX + positionDiff, pointY]);
                slotsToFill--;
                //Debug.Log($"x y {pointX + i} {pointY}");
            }
            if (pointY + positionDiff < sizeY && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX, pointY + positionDiff]);
                slotsToFill--;
                //Debug.Log($"x y {pointX} {pointY + i}");
            }
            if (pointY - positionDiff >= 0 && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX, pointY - positionDiff]);
                slotsToFill--;
                //Debug.Log($"x y {pointX} {pointY - i}");
            }
            // diagonal
            if (pointX + positionDiff < sizeX && pointY + positionDiff < sizeY && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX + positionDiff, pointY + positionDiff]);
                slotsToFill--;
                Debug.Log($"x y {pointX + positionDiff} {pointY + positionDiff}");
            }
            if (pointX - positionDiff >= 0 && pointY - positionDiff >= 0 && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX - positionDiff, pointY - positionDiff]);
                slotsToFill--;
                //Debug.Log($"x y {pointX - i} {pointY - i}");
            }
            if (pointX + positionDiff < sizeX && pointY - positionDiff >= 0 && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX + positionDiff, pointY - positionDiff]);
                slotsToFill--;
                //Debug.Log($"x y {pointX + i} {pointY - i}");
            }
            if (pointX - positionDiff >= 0 && pointY + positionDiff < sizeY && slotsToFill > 0)
            {
                FreeUpSlot(slots[pointX - positionDiff, pointY + positionDiff]);
                slotsToFill--;
                //Debug.Log($"x y {pointX - i} {pointY + i}");
            }

            positionDiff++;
        } while (slotsToFill > 0);


        int itemsToPlace = numberOfFreeSlots - 4;

        for (int i = 0; i < itemsToPlace; i++)
        {
            var randomFreeSlot = freeSlots[Random.Range(0, freeSlots.Count)];
            if (randomFreeSlot.GetState() == SlotState.Free)
            {
                var obj = startingItemsFactory.CreateRandomItem();
                randomFreeSlot.SetItem(obj);
                freeSlots.Remove(randomFreeSlot);
            } else
            {
                i--;
            }
        }

        var randomSlotForSpawner = freeSlots[Random.Range(0, freeSlots.Count)];
        var spawnerInstance = Instantiate(spawnerPrefab);
        randomSlotForSpawner.SetItem(spawnerInstance);
    }

    private void FreeUpSlot(Slot slot)
    {
        slot.SetFree();
        freeSlots.Add(slot);
    }
}
