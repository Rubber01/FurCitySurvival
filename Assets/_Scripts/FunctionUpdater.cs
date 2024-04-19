using UnityEngine;

public class FunctionUpdater : MonoBehaviour
{

    private static FunctionUpdater instance = null;
    public static System.Action OnUpdate;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
            Destroy(this);
    }

    void Update()
    {
        if (OnUpdate != null)
            OnUpdate();
    }
}
