using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private List<Actions> _actions;
    private List<Blackboard> _blackboards;

    private float Normalise( float current, float max, float min = 0)
    {
        return (current - min) / max;
    }
}

public class Blackboard
{
    private readonly GameObject _self; // Whose blackboard is this
    private Transform _healthStation { get; }
    private Transform _staminaStation { get; }
    private Transform _hungerStation { get; }
    private float _health { get; set; } // Their stats
    private float _maxHealth { get; set; } // Their stats
    private float _stamina { get; set; } // Their stats
    private float _maxStamina { get; set; } // Their stats
    private float _hunger { get; set; } // Their stats
    private float _maxHunger { get; set; } // Their stats
    private float _distanceToHealth => Vector3.Distance(_self.transform.position, _healthStation.position);
    private float _distanceToStamina => Vector3.Distance(_self.transform.position, _staminaStation.position);
    private float _distanceToHunger => Vector3.Distance(_self.transform.position, _hungerStation.position);
    
    
    public Blackboard(GameObject self, Transform healthStation, Transform staminaStation, Transform hungerStation, float health, float maxHealth, float stamina, float maxStamina, float hunger, float maxHunger)
    {
        _self = self;
        _healthStation = healthStation;
        _staminaStation = staminaStation;
        _hungerStation = hungerStation;
        _health = health;
        _maxHealth = maxHealth;
        _stamina = stamina;
        _maxStamina = maxStamina;
        _hunger = hunger;
        _maxHunger = maxHunger;
    }
}

public class Actions
{
}
