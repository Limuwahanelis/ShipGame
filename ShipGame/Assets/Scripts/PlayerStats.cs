using UnityEngine;

public static class PlayerStats 
{
    public static int lootperSecond=5;
    public static int totalCrewOnBoard;
    public static int loot;

    public static void ResetStats()
    {
        loot = 0;
    }
}
