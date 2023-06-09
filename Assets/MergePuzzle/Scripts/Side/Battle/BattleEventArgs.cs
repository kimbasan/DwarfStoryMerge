using System;

public class BattleEventArgs : EventArgs
{
    private EnemyController enemy;

    public BattleEventArgs(EnemyController enemy)
    {
        this.enemy = enemy;
    }

    public EnemyController GetEnemy() {
        return enemy;
    }
}
