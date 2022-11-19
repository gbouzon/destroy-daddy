using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string sceneName;
    public float[] playerPosition;
    public float[] playerRotation;
    public float health;
    public int level;
    public int experience;
    public int currency;
    public string[] puzzleSolved;
    public string currentShip;
    public float fuel;

    public PlayerData(string sceneName, float[] playerPosition, float[] playerRotation, 
    float health, int level, int experience, int currency, string[] puzzleSolved, string currentShip, float fuel) {
        this.sceneName = sceneName;
        this.playerPosition = playerPosition;
        this.playerRotation = playerRotation;
        this.health = health;
        this.level = level;
        this.experience = experience;
        this.currency = currency;
        this.puzzleSolved = puzzleSolved;
        this.currentShip = currentShip;
        this.fuel = fuel;
    }

    public void assignValues() {
        if (health == float.MaxValue) 
            PlayerExperience.startCount = 0;
        else {
            PlayerExperience.startCount = 1;
            PlayerExperience.health = health;
        }
        if (fuel == float.MaxValue) 
            ShipController.startCount = 0;
        else {
            ShipController.startCount = 1;
            ShipController.fuel = fuel;
        }
    }
    
}