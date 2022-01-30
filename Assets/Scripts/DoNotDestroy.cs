using UnityEngine;

// The "most MonoBehaviour" MonoBehaviour of all time
public class DoNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
