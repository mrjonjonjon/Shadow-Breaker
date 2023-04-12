using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPlayerZ : MonoBehaviour
{
    public SpriteRenderer _playerSpriteRenderer;

    void Update()
    {
       // _playerSprite = _playerSpriteRenderer.sprite;
       // Vector2 _playerPivot = _playerSprite.pivot;
       // float width = _playerSprite.bounds.size.x;
      //  float height = _playerSprite.bounds.size.y;
       // float ppu = _playerSprite.pixelsPerUnit;
        float distance = _playerSpriteRenderer.bounds.extents.y - _playerSpriteRenderer.sprite.pivot.y;
        _playerSpriteRenderer.transform.localPosition= -Vector3.up*distance;
        print(distance);
        
    }
}
