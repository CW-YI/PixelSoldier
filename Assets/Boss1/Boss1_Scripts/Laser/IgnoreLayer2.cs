using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayer2 : MonoBehaviour
{
    // Start is called before the first frame update
    int PlatformLayer;

    void Start()
    {
        PlatformLayer = LayerMask.NameToLayer("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(PlatformLayer, PlatformLayer, true);
    }
}
