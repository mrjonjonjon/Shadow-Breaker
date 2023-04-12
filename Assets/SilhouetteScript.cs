using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteScript : MonoBehaviour
{
    SpriteRenderer silhouetteSR;
    SpriteRenderer mainSR;
    // Start is called before the first frame update
    void Start()
    {
        silhouetteSR=GetComponent<SpriteRenderer>();
        mainSR=transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        silhouetteSR.sprite=mainSR.sprite;
    }
}
