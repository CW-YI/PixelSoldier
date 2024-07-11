using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayer : MonoBehaviour
{
    int PlatformLayer, enemyLayer;
    // Start is called before the first frame update
    void Start()
    {
        PlatformLayer = LayerMask.NameToLayer("Platform");
        enemyLayer = LayerMask.NameToLayer("enemy");
    }
    

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(enemyLayer, PlatformLayer, true);
    }
}
