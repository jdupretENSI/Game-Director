using UnityEngine;

public class Blackboard
{
    private YvanController _controller;
    private Transform _entertainmentStation;
    private Transform _bathroomStation;
    private Transform _hungerStation;
    private Transform _sleepStation;

    public Blackboard(YvanController controller, Transform entertainmentStation, Transform bathroomStation, 
        Transform hungerStation, Transform sleepStation)
    {
        _controller = controller;
        _entertainmentStation = entertainmentStation;
        _bathroomStation = bathroomStation;
        _hungerStation = hungerStation;
        _sleepStation = sleepStation;
    }

    public float Entertainment => _controller.Entertainment;
    public float MaxEntertainment => _controller.MaxEntertainment;
    public float Bladder => _controller.Bladder;
    public float MaxBladder => _controller.MaxBladder;
    public float Stomach => _controller.Stomach;
    public float MaxStomach => _controller.MaxStomach;
    public float Exhaustion => _controller.Exhaustion;
    public float MaxExhaustion => _controller.MaxExhaustion;
    
    public float DistanceToEntertainment => Vector3.Distance(_controller.transform.position, _entertainmentStation.position);
    public float DistanceToBathroom => Vector3.Distance(_controller.transform.position, _bathroomStation.position);
    public float DistanceToFood => Vector3.Distance(_controller.transform.position, _hungerStation.position);
    public float DistanceToBed => Vector3.Distance(_controller.transform.position, _sleepStation.position);
}