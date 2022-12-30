using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetFox : MonoBehaviour
{
    private GameObject marcador;   
    private void Start()
    {
        marcador = this.transform.GetChild(0).gameObject;
        
    }
    private void Update()
    {
        this.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {      
        this.transform.SetParent(other.gameObject.transform);        
        if (other.CompareTag("fox"))
        {
            this.transform.position = other.gameObject.transform.position;
            marcador.SetActive(false);
            
        }
    }
}
