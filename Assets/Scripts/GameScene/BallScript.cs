using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private GameObject managerLvl3;

    private void Start()
    {
        managerLvl3 = GameObject.FindGameObjectWithTag("managerLvl3");
    }
    private void Update()
    {
        if (transform.position.y < -0.6f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tree"))
        {            
            this.transform.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 0.8f * -1, ForceMode.Impulse);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("chicken"))
        {
            managerLvl3.GetComponent<ManagerLvl3>().chickenPick += 1;
            Destroy(other.gameObject);
        }
            
    }

}
