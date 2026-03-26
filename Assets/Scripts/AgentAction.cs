using System;

[Serializable]
public class AgentAction
{
    private AgentAction(string name)
    {
        Name = name;
    }
    
    public string Name { get; }

    /// <summary>
    /// Strategy Programing pattern to decouple the action from what is happening.
    /// </summary>
    private IActionStrategy _strategy;

    public bool Complete => _strategy.Complete;
    
    public void Start() => _strategy.Start();

    public void Update(float deltaTime)
    {
        // Check if the action can be performed and update the strategy
        if (_strategy.CanPerform) _strategy.Update(deltaTime);
        // Bail out if the strategy is still executing
        if (!_strategy.Complete) return;
        
    }
    
    public void Stop() => _strategy.Stop();

    public class Builder
    {
        private readonly AgentAction _agentAction;

        public Builder(string name)
        {
            _agentAction = new AgentAction(name);
        }

        public Builder WithStrategy(IActionStrategy strategy)
        {
            _agentAction._strategy = strategy;
            return this;
        }

        public AgentAction Build()
        {
            return _agentAction;
        }
    }
}
public interface IActionStrategy
{
    bool CanPerform { get; }
    bool Complete { get; }

    void Start()
    {
        //noop
    }

    void Update(float deltaTime)
    {
        //noop
    }

    void Stop()
    {
        //noop
    }
}