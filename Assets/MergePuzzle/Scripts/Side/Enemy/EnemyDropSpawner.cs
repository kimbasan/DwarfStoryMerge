using UnityEngine;

public class EnemyDropSpawner : ItemSpawner
{
    public void CreateDropItem()
    {
        bool placed = CreateRandomItemAndPlace();
        if (!placed)
        {
            // no free slots for coins
            var score = PlayerPrefs.GetInt(Constants.SCORE_INT);
            PlayerPrefs.SetInt(Constants.SCORE_INT, score + 5);
        }
    }

}
