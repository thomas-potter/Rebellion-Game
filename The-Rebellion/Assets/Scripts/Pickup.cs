using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemType;

    public void DestroyPickupObject()
    {
        Destroy(gameObject);
    }
}
