using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Gem : InteractriveObject
{
    public override void Interaction()
    {
        gameObject.SetActive(false);
        Player.singleton.OnGemTake();
    }
}
