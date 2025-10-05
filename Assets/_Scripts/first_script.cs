using UnityEngine;

public class first_script : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("awake");
    }
    void Start()
    {
        Debug.Log("start");

    }

    void Update()
    {
        Debug.Log("update");

    }
    private void FixedUpdate()
    {
        Debug.Log("fixedupadte");

    }

    private void LateUpdate()
    {
        Debug.Log("lateupdate");

    }
}
