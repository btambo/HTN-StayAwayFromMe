using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHeads : MonoBehaviour
{
    void Update()
    {
        if (PlayerMovement.Counter == 10)
        {
            Destroy(this.gameObject);
        }
    }
}
