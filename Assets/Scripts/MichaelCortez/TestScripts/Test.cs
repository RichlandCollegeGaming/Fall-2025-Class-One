using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
       
        //Destroy(gameObject,3f);

            rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            //Destroy(gameObject);

            //rb.AddForce(Vector3.up * 500);

            rb.linearVelocity = Vector3.forward * 100f;
    }



    private void OnMouseDown()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }




}
