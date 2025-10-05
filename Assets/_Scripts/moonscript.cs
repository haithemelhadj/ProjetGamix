using UnityEngine;

public class moonscript : MonoBehaviour
{
    public Transform moon;
    public Transform earth;
    public Transform sun;

    public float earthAngle= 0.5f;
    public float earthSpeed = 0.5f;
    public float sunSpeed = 0.5f;
    public float sunAngle = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //moon.RotateAround(earth.position, new Vector3(0, moonSpeed, 0) * Time.deltaTime, moonAngle);
        //earth.RotateAround(sun.position, new Vector3(0, sunSpeed, 0) * Time.deltaTime, sunAngle);

        sun.Rotate(new Vector3(0, sunSpeed, 0), sunAngle);
        earth.Rotate(new Vector3(0, earthSpeed, 0), earthAngle);

    }
}
