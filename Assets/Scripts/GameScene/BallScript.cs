using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private GameObject terrain;
    private GameObject[] collectChicken;
    private GameObject fox;
    private bool positioned = false;
    private void Start()
    {
        fox = GameObject.FindGameObjectWithTag("fox");
        terrain = GameObject.FindGameObjectWithTag("terrain");

    }
    private void Update()
    {
        if (!positioned)
        {
            Ray();
        }

        if (this.transform.position.y < terrain.transform.position.y)
        {
            collectChicken = GameObject.FindGameObjectsWithTag("collectFood");
            for (int i = 0; i < collectChicken.Length; i++)
            {
                Destroy(collectChicken[i].gameObject);
            }
            Destroy(this.gameObject);
            CanvasScript.boolBall = true;
        }

    }

    void Ray()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData, 10000f))
        {

            if (hitData.collider.gameObject.CompareTag("fox") || hitData.collider.gameObject.CompareTag("collectFood") || !hitData.collider.gameObject.CompareTag("terrain"))
            {
                this.transform.position = new Vector3(Random.Range(fox.transform.position.x - 0.3f, fox.transform.position.x + 0.3f), fox.transform.position.y + 1f, Random.Range(fox.transform.position.z - 0.3f, fox.transform.position.z + 0.3f));

            }
            else
            {
                positioned = true;
            }


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("collectFood"))
        {
            //Singleton.chicken += 1;
            Destroy(collision.collider.gameObject);
        }
    }

}
