using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovible : MonoBehaviour
{
    [SerializeField]float speed = 1;
    private GameObject cubo;
    private Vector3 punto1;
    private Vector3 punto2;
    private Vector3 target;
    private void Start()
    {
        cubo = this.transform.GetChild(0).gameObject;
        punto1 = this.transform.GetChild(1).gameObject.transform.position;
        punto2 = this.transform.GetChild(2).gameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        punto1 = this.transform.GetChild(1).gameObject.transform.position;
        punto2 = this.transform.GetChild(2).gameObject.transform.position;
        if (cubo.transform.position == punto1)
        {
            target = punto2;
        }else if (cubo.transform.position == punto2)
        {
            target = punto1;
        }
        newTarget(target);
        
    }

    void newTarget(Vector3 target)
    {
        cubo.transform.position = Vector3.MoveTowards(this.transform.GetChild(0).transform.position, target, speed * Time.deltaTime);
    }
}
