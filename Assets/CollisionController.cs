using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public bool inContact=false;
    Collider2D colToRemove;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    void ResolveCollisions(){
        Rigidbody2D _rigidBody=GetComponent<Rigidbody2D>();
        Collider2D _collider2D = GetComponent<Collider2D>();
        ContactFilter2D _noFilter= new ContactFilter2D{useTriggers = true,useLayerMask = true};
        _noFilter.SetLayerMask(LayerMask.GetMask("Wall"));
        Collider2D[] _collisions= new Collider2D[3];
        int numColliders = Physics2D.OverlapCollider(_collider2D, _noFilter, _collisions);
 
   for (int i = 0; i < numColliders; ++i)
   {
       Collider2D c = _collisions[i];
       ColliderDistance2D overlap = _collider2D.Distance(_collisions[i]);
 
       if (overlap.isOverlapped)
       {
           Vector2 newPosition = new Vector2(transform.position.x + (overlap.distance * overlap.normal.x), transform.position.y + (overlap.distance * overlap.normal.y));
 
           float distMoved = (new Vector2(transform.position.x, transform.position.y) - newPosition).magnitude;
 
          
 
          _rigidBody.MovePosition(newPosition);
        // transform.position=newPosition;
       }
   }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ResolveCollisions();
        
    }
    */
   
}
