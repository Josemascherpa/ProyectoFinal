using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenScript : MonoBehaviour
{
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject X2;
    [SerializeField] private GameObject Z;
    [SerializeField] private GameObject Z2;
    public float speedRotation;
    // Start is called before the first frame update
    void Start()
    {
        speedRotation = Random.Range(1f, 9f);
        Z = GameObject.FindGameObjectWithTag("Zlvl3");
        Z2 = GameObject.FindGameObjectWithTag("Z2lvl3");
        X = GameObject.FindGameObjectWithTag("Xlvl3");
        X2 = GameObject.FindGameObjectWithTag("X2lvl3");
    }
    private void Update()
    {
        transform.eulerAngles += new Vector3(0, speedRotation, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("tree"))
        {
            transform.position = new Vector3(Random.Range(X.transform.position.x, X2.transform.position.x), X.transform.position.y, Random.Range(Z.transform.position.z, Z2.transform.position.z));
        }
    }
}
