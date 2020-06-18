using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship", menuName = "Ship")]
public class Ship : ScriptableObject
{
    [Header("General Information")]
    public string shipName;

    // These are the sprites that will be associated with the ship.
    public Sprite portrait;
    public Sprite minimapIcon;
    public Sprite mapSprite;

    // This is a description of what the ship is.
    [TextArea(3, 10)]
    public string description;

    // This specifies the type of the ship.
    public enum ShipType { None, Vanguard, Defender, Engineer, Supporter, Carrier, Assassin, Scout, Auxiliary}
    public ShipType shipType;

    [Header("Base Stats")]
    public int health;
    public float healthRegeneration;
    public int attack;
    public float attackSpeed;
    public int armor;
    public int shield;
    public float shieldRegeneration;
    public float moveSpeed;
    public float turnSpeed;
    public float acceleration;
    public int energy;
    public float energyRegeneration;
    public float deploymentTime;
    public float range;
}
