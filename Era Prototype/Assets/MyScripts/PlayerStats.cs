using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Player Stats Class", menuName = "Prototype/Player Stats Class")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public new string name = "Default Class";

    [SerializeField] public int health = 100;
    [SerializeField] public int mana = 100;

    [SerializeField] public int strength = 5;
    [SerializeField] public int intelligence = 5;
    [SerializeField] public int defense = 5;
    [SerializeField] public int luck = 5;

}
