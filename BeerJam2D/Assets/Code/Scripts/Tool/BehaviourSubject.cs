using System;
using UnityEngine;

[Serializable]
public class BehaviourSubject<T>
{
    [field: SerializeField] public T LastValue {get; private set;}
    public Action<T> action = default;
    public Action actionVoid = default;
    public BehaviourSubject( T initValue = default)
    {
        LastValue = initValue;
    }
    public void Subscribe(bool condition, Action<T> callback)
    {
        if (condition) callback.Invoke(LastValue);
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
        LastValue = val;
        Invoke();
    }
    public void Invoke()
    {
        action?.Invoke(LastValue);
        actionVoid?.Invoke();
    }
}
