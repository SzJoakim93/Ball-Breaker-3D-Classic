using UnityEngine;
using System.Collections.Generic;

public class Global
{
    public static List<Block> BlockList;
    public static int level = 10;
    public static int max_levels = 20;

    public static int TotalScores;
    public static int TotalStars;

    public static PowerUpTimeBased MegaBall;
    public static PowerUpTimeBased Wall;
    public static PowerUpTimeBased Grip;
    public static PowerUpTimeBased IncreaseSize;
    public static PowerUpTimeBased DescreaseSize;

    public static bool AppStarted = false;

    public static void LoadState()
    {
        Global.TotalScores = PlayerPrefs.GetInt("Score", 0);
        Global.TotalStars = PlayerPrefs.GetInt("Stars", 0);

        MegaBall = new PowerUpTimeBased("MegaBall", 100, 5.0f);
        Wall = new PowerUpTimeBased("Wall", 100, 5.0f);
        Grip = new PowerUpTimeBased("Grip", 100, 5.0f);
        IncreaseSize = new PowerUpTimeBased("IncreaseSize", 100, 5.0f);
        DescreaseSize = new PowerUpTimeBased("DescreaseSize", 0, 20.0f);
    }

}