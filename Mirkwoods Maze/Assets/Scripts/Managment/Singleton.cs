using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = (T)this;
        }

        if (!gameObject.transform.parent && this.gameObject != null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
