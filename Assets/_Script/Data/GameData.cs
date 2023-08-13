using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{

    public int keys;
    public int diamons;
    public int currentLevel;
    public int[] turretUnlockedLevel;

    public GameData() // default value
    {
        keys = 100;
        diamons = 10;
        currentLevel = 1;
        turretUnlockedLevel = new int[] {1,1,1,1};
    }


}
