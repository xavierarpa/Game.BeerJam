using UnityEngine.Events;
using UnityEngine.UI;
public static class Utils 
{
    public static void Subscribe<T>(in this bool condition, ref T action,in T callback) where T : System.Delegate
    {
        if(condition) action = (T)System.Delegate.Combine(action, callback);
        else action = (T)System.Delegate.Remove(action, callback);
    }
    public static void Subscribe(in this bool condition, ref Button action, UnityAction callback)
    {
        if(condition) action.onClick.AddListener(callback);
        else action.onClick.RemoveListener(callback);
    }
}