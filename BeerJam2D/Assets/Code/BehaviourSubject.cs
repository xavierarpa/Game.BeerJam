using System;

public class BehaviourSubject<T>
{
    private T lastValue;
    public Action<T> action = default;
    public Action actionVoid = default;
    public BehaviourSubject( T initValue = default)
    {
        lastValue = initValue;
    }
    public void Subscribe(bool condition, Action<T> callback)
    {
        if (condition) callback.Invoke(lastValue);
        SubscribeWithoutNotify(condition, callback);
    }
    public void SubscribeWithoutNotify(bool condition, Action<T> callback){
        if (condition) action += callback;
        else action -= callback;
    }
    public void Subscribe_Void(bool condition, Action callback)
    {
        if (condition) callback.Invoke();
        SubscribeWithoutNotify_Void(condition, callback);
    }
    public void SubscribeWithoutNotify_Void(bool condition, Action callback)
    {
        if (condition) actionVoid += callback;
        else actionVoid -= callback;
    }
    public void Invoke(T val)
    {
        lastValue = val;
        Invoke();
    }
    public void Invoke()
    {
        action?.Invoke(lastValue);
        actionVoid?.Invoke();
    }
}
