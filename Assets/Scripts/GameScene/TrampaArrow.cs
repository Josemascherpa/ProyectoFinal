using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] float timeArrow;
    [SerializeField] float speedArrow;
    private GameObject level;
    
    //HACER UNA CORRUTINA PARA LANZAR LAS FLECHAS
    void Start()
    {
        level = GameObject.FindGameObjectWithTag("level");
        StartCoroutine(InstantiateArrow(timeArrow));//corrutina para lanzar flecha cada cierto tiempo
    }
    
    IEnumerator InstantiateArrow(float timeArrow)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeArrow);
            var arrowInstance = Instantiate(arrow, this.transform.GetChild(1).gameObject.transform);
            arrowInstance.transform.SetParent(level.transform);
            var dire = this.transform.GetChild(2).transform.position - arrowInstance.transform.position;        
            arrowInstance.GetComponent<Rigidbody>().velocity = dire.normalized * speedArrow;       
        }
    }

    
}
