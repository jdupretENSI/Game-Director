using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class GameDirector : MonoBehaviour
{
    // TODO: Maybe replace with hashset
    private readonly List<Blackboard> _blackboards = new();
    
    private float _gamePriority, _restPriority, _eatPriority, _relievePriority;
    
    public UnityEvent OnGameEvent;
    public UnityEvent OnRestEvent;
    public UnityEvent OnEatEvent;
    public UnityEvent OnRelieveEvent;

    [SerializeField] private bool _debugLogPriority = false;

    private void Update()
    {

        // Check each blackboard and see what they need atm and then tell them the best action to solve this situation

        foreach (Blackboard bb in _blackboards)
        {
            _gamePriority = InverseNormalise(bb.Entertainment, bb.MaxEntertainment) 
                * InverseNormalise(bb.DistanceToEntertainment, 40f);
            _eatPriority = InverseNormalise(bb.Stomach, bb.MaxStomach) 
                * InverseNormalise(bb.DistanceToFood, 40f);
            
            _relievePriority = Normalise(bb.Bladder, bb.MaxBladder) 
                *  InverseNormalise(bb.DistanceToBathroom, 40f);
            _restPriority = Normalise(bb.Exhaustion, bb.MaxExhaustion) 
                * InverseNormalise(bb.DistanceToBed, 40f);
            
            float highestPriority = MathF.Max(_gamePriority,
                MathF.Max(_eatPriority,
                    MathF.Max(_relievePriority, _restPriority)));

            if (_debugLogPriority)
            {
                string actionName = highestPriority switch
                {
                    _ when highestPriority == _gamePriority => "Gamerrr",
                    _ when highestPriority == _restPriority => "Eepy",
                    _ when highestPriority == _eatPriority => "Nom Nom",
                    _ when highestPriority == _relievePriority => "Pissin Myself",
                    _ => "Unknown"
                };

                Debug.Log($"{actionName} action has highest priority: {highestPriority}");
            }
            
            // Invoke the appropriate UnityEvent based on the highest priority
            if (highestPriority == _gamePriority)
            {
                OnGameEvent?.Invoke();
            }
            else if (highestPriority == _restPriority)
            {
                OnRestEvent?.Invoke();
            }
            else if (highestPriority == _eatPriority)
            {
                OnEatEvent?.Invoke();
            }
            else if (highestPriority == _relievePriority)
            {
                OnRelieveEvent?.Invoke();
            }
        }
    }
    
    private static float Normalise(float current, float max, float min = 0)
    {
        if (max <= min) return -1;
        return Mathf.Clamp01((current - min) / (max - min));
    }

    private static float InverseNormalise(float current, float max, float min = 0)
    {
        if (max <= min) return -1;
        return Mathf.Clamp01(1 - (current - min) / (max - min));
    }
    
    public void AddBlackboard(Blackboard blackboard) => _blackboards.Add(blackboard);
    public void RemoveBlackboard(Blackboard blackboard) => _blackboards.Remove(blackboard);
}