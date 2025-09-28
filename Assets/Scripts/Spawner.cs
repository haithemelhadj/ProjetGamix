using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(player, new Vector3(-5, 0.5f, 1), player.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
