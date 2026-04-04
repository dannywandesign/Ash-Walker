public static class GameSession
{
    public static bool[] WorldUnlocked = new bool[] { true, false, false, false, false };

    public static void UnlockWorld(int index)
    {
        if (index < WorldUnlocked.Length)
        {
            WorldUnlocked[index] = true;
        }
    }
}