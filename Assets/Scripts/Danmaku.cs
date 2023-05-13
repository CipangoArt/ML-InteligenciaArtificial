using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MonoBehaviour
{
    public Rigidbody rb;
   
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(0, -10, 0);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DeathPlane>(out DeathPlane death))
        {
            gameObject.SetActive(false);
        }
    }
}
