using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractriveObject : MonoBehaviour
{
    public abstract  void Interaction();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
            Interaction();
    }
}