using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    /*[HideInInspector]*/ public float speed;
    public float force;
    //[SerializeField] private int a;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        //rb.linearVelocity = Vector3.right * Time.deltaTime * speed;
        //rb.linearVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * speed;
        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * Time.deltaTime * force, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        //if(!collision.gameObject.name.Equals("Plane"))
        //    Debug.Log(collision.gameObject.name);
        if (!collision.gameObject.tag.Equals("Floor"))
            Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
    }
}
