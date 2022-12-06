using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetFox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        this.transform.SetParent(other.gameObject.transform);        
        if (other.CompareTag("fox"))
        {
            this.transform.position = other.gameObject.transform.position;
        }
    }
}
