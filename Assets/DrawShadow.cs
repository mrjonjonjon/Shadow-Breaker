using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShadow : MonoBehaviour
{
SpriteRenderer _spriteRenderer;
Physics playerPhys;
SpriteRenderer playerSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerPhys =transform.parent.GetComponent<Physics>();
        playerSpriteRenderer=transform.parent.Find("sprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition =new Vector3(0f,playerPhys.zfloor,-playerPhys.zfloor-0.05f);
        //_spriteRenderer.sprite = playerSpriteRenderer.sprite;

       
    }
}
