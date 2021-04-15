using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int experiencePoints;
    public int health;
    public int level;
    public int ammo;
    public string finalBoss;
    public string gunName;
    
    public string Stringify() {
        return JsonUtility.ToJson(this);
    }

    public static PlayerData Parse(string json)
    {
       return JsonUtility.FromJson<PlayerData>(json);
    }
}
