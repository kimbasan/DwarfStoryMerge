using UnityEngine;

public class Constants {

    public static readonly Vector2 sideMovement = new Vector2(-1, 0);

    #region Layers
    public static readonly int DEAD_ENEMY_LAYER = 6;
    public static readonly int DEFAULT_LAYER = 0;
    #endregion

    #region Tags
    public static readonly string PLATFORM = "Platform";
    public static readonly string ENEMY = "Enemy";
    #endregion

    #region Animation states
    public static readonly string STANDING = "Standing";
    public static readonly string DEAD = "Dead";
    public static readonly string ATTACK = "Attack";
    public static readonly string DAMAGED = "Damaged";
    #endregion

    #region Level multiplyer
    public static readonly int ARMOR_MULTIPLYER = 10;
    public static readonly int HEALTH_MULTIPLYER = 20;
    public static readonly int COIN_MULTIPLYER = 5;
    #endregion

    #region Player Settings
    public static readonly string DIFFICULTY_MULTIPLYER = "Difficulty";
    public static readonly string TUTORIAL_BOOL = "TutorialShown";

    public static readonly string HEALTH_LEVEL = "HealthLevel";
    public static readonly string MAX_ARMOR_LEVEL = "MaxArmorLevel";
    public static readonly string START_ARMOR_LEVEL = "StartArmorLevel";
    public static readonly string CHEST_LEVEL = "ChestLevel";

    public static readonly string ELF_UNLOCK_BOOL = "ElfUnlocked";
    public static readonly string HUMAN_UNLOCK_BOOL = "HumanUnlocked";

    public static readonly string SCORE_INT = "CoinsScore";

    #endregion
}
