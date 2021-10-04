using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticleController : MonoBehaviour
{
    public GameObject ExplosionParticle;

    private List<GameObject> individuals = new List<GameObject>();

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<IndividualMovementController>())
        {
            GameObject particle = Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            particle.transform.parent = transform;
            particle.transform.parent = null;
            //Destroy(individuals[Random.Range(0, individuals.Count)].gameObject);
            for (int i = 0; i < Mathf.Clamp(IndividualMovementManager.Instance.Individuals.Count/4,0, IndividualMovementManager.Instance.Individuals.Count); i++)
            {
                Destroy(IndividualMovementManager.Instance.Individuals[i]);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (individuals.Contains(other.gameObject)) return;
        if (!other.GetComponent<IndividualMovementController>()) return;
        individuals.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!individuals.Contains(other.gameObject)) return;
        individuals.Remove(other.gameObject);
    }
}
