using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float timeDestroy = 2f;

    private void OnCollisionEnter(Collision collision)//Destruir al chocar algo
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        StartCoroutine(destroyParticle());
    }

    public IEnumerator destroyParticle()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(this.gameObject);
    }
}
