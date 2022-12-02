using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    public float timeArrow;
    public float speedArrow;
    
    //HACER UNA CORRUTINA PARA LANZAR LAS FLECHAS
    void Start()
    {        
        StartCoroutine(InstantiateArrow(timeArrow));
    }
    
    IEnumerator InstantiateArrow(float timeArrow)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeArrow);
            var arrowInstance = Instantiate(arrow, this.transform.GetChild(1).gameObject.transform);
            arrowInstance.transform.SetParent(null);
            var dire = this.transform.GetChild(2).transform.position - arrowInstance.transform.position;        
            arrowInstance.GetComponent<Rigidbody>().velocity = dire.normalized * speedArrow;       
        }
    }

    
}
