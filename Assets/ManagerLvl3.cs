using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLvl3 : MonoBehaviour
{
    [SerializeField] private GameObject Chicken;
    [SerializeField] private GameObject Ball;
    public int cantChicken;
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject Z;
    public int chickenPick=0;
    void Start()
    {
        Instantiate(Ball, new Vector3(0, 0.3f, 0),Quaternion.identity);
        for (int i=0;i<cantChicken;i++)
        {
            Instantiate(Chicken, new Vector3(Random.Range(-X.transform.position.x, X.transform.position.x), X.transform.position.y, Random.Range(-Z.transform.position.z, Z.transform.position.z)),Chicken.transform.rotation);
        }
    }
    private void Update()
    {
        if(chickenPick== cantChicken)
        {
            print("GANASTE EL NIVEL");
        }
    }


}
