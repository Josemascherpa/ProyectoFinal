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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tree"))
        {
            print("ASDASDF");
            this.transform.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * 0.3f * -1, ForceMode.Impulse);
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
