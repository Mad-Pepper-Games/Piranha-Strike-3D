using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    public List<GameObject> FishList = new List<GameObject>();

    public GameObject FishPrefab;

    public int MultiplyCount;
    public int PlusCount;
    private bool plus;
    public int MinusCount;
    private int minusCounter;
    public DoorType DoorType;
    private void OnTriggerEnter(Collider other)
    {

        if(DoorType == DoorType.MultiplyDoor)
        {
            Multiply(other);
        }
        else if(DoorType == DoorType.PlusDoor)
        {
            Plus(other);
        }
        else if(DoorType == DoorType.MinusDoor)
        {
            Minus(other);
        }

    }

    public void Plus(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>() != null)
        {         
            if (plus == false)
            {
                plus = true;

                if (!FishList.Contains(other.gameObject))
                {
                    FishList.Add(other.gameObject);
                    for (int i = 0; i < PlusCount; i++)
                    {
                        GameObject asd = Instantiate(FishPrefab, transform.position, Quaternion.identity);
                        FishList.Add(asd);
                        asd.transform.parent = transform;
                        asd.transform.parent = null;
                    }
                }
            }
        }
    }

    public void Multiply(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>() != null)
        {
            if (!FishList.Contains(other.gameObject))
            {
                FishList.Add(other.gameObject);
                
                for (int i = 1; i < MultiplyCount; i++)
                {
                    GameObject asd = Instantiate(FishPrefab, transform.position, Quaternion.identity);
                    FishList.Add(asd);
                    asd.transform.parent = transform;
                    asd.transform.parent = null;
                }
            }
        }
    }

    public void Minus(Collider other)
    {
        if (other.GetComponent<IndividualMovementController>() != null)
        {
            if (!FishList.Contains(other.gameObject))
            {
                FishList.Add(other.gameObject);

                if (minusCounter < MinusCount)
                {
                    minusCounter++;
                    if (other.gameObject)
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
        }
    }

}
public enum DoorType 
{
    MinusDoor,
    PlusDoor,
    MultiplyDoor
}
