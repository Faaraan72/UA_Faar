using System;

[Serializable]
public class SaveData
{
    public int highestLevelReached;
    public int highScore;

    public SaveData()
    {
        highestLevelReached = 0;
        highScore = 0;
    }
}
