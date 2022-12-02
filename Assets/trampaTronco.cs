using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampaTronco : MonoBehaviour
{
    private float speedRotation=100;
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject Xn;
    [SerializeField] private GameObject Zn;
    [SerializeField] private GameObject Z;    
    private Vector3 target;
    private float speed = 0.05f;
    private void Start()
    {
        target = new Vector3(Random.Range(X.transform.position.x, Xn.transform.position.x), this.transform.position.y, Random.Range(Z.transform.position.z, Zn.transform.position.z));
    }
    void Update()
    {        
        Rotation();
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if (collision.gameObject.CompareTag("tree") || collision.gameObject.CompareTag("NextLevel"))
        {           
            target = new Vector3(Random.Range(X.transform.position.x, Xn.transform.position.x), this.transform.position.y, Random.Range(Z.transform.position.z, Zn.transform.position.z));
        }
    }

    void Move()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, target) < 0.0000001f)
        {            
            target = new Vector3(Random.Range(X.transform.position.x, Xn.transform.position.x), this.transform.position.y, Random.Range(Z.transform.position.z, Zn.transform.position.z));
        }
    }

    void Rotation()
    {

        transform.eulerAngles += new Vector3(0, speedRotation * Time.deltaTime, 0);
    }
}
