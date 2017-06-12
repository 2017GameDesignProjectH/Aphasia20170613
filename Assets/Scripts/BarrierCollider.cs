using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierCollider : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        other.SendMessage("Hit");
    }
}
