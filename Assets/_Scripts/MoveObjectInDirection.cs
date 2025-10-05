using UnityEngine;

public class MoveObjectInDirection : MonoBehaviour
{
    public Vector3 direction;
    

    void Update()
    {
        transform.Translate(direction * InfiniteRunnerManager.speed * Time.deltaTime);
    }
}
