using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private GameObject managerLvl3;
    private float fuerzaEmpuje = 0.8f;
    private GameObject target;
    private void Start()
    {
       
        managerLvl3 = GameObject.FindGameObjectWithTag("managerLvl3");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("tree"))//Agrego fuerza contraria al chocar con algun objeto con tagg tree
        {            
            this.transform.GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * fuerzaEmpuje * -1, ForceMode.Impulse);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("chicken"))//Recojo los pollitos y le sumo a la variable
        {
            managerLvl3.GetComponent<ManagerLvl3>().chickenPick += 1;
            Destroy(other.gameObject);
        }
            
    }

}
