using UnityEngine;

public class PerpectiveCamera : MonoBehaviour
{
    public static PerpectiveCamera Instance;
    public float focalLength = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public float GetPerspective(float zPos)
    {
        return focalLength / Mathf.Max((focalLength * zPos), 0);
    }
}
