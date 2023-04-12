using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookshotScript : MonoBehaviour
{
    
public bool isPulling=false;
public LineRenderer _lineRenderer;
public float _maxDist=5f;
public Physics _phys;
public Physics _source;
public Physics _dest;
public float pullTime=0.1f;
public GameObject grappleObject;
public float _hookRadius=100f;

public ContactFilter2D cf;
public void Start(){
   _phys = transform.parent.GetComponent<Physics>();
   _lineRenderer = GetComponent<LineRenderer>();
}

private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 centerPos = transform.position;
        Gizmos.DrawWireSphere(centerPos, _hookRadius);
    }



public void ShowGrappleUI(){


                
            List<RaycastHit2D> results=new List<RaycastHit2D>();
            int numResults = Physics2D.CircleCast((Vector2)transform.position, _hookRadius,Vector2.zero, cf, results,Mathf.Infinity);
            Vector2 inputVector = MovementController.instance._currentInputState.inputVector.normalized;
            if(inputVector==Vector2.zero)return;
            GameObject bestPoint = null;
            float maxScore=-Mathf.Infinity;

            foreach(RaycastHit2D hit in results){
                hit.transform.Find("sprite/arrow").GetComponent<SpriteRenderer>().enabled=false;

                Vector2 toPoint = new Vector2(hit.point.x-transform.position.x,hit.point.y-transform.position.y).normalized; 
                float score = Vector2.Dot(inputVector,toPoint);
                if(score>maxScore){
                    maxScore=score;
                    bestPoint = hit.transform.gameObject;
                   // bestPoint.transform.Find("sprite/arrow").GetComponent<SpriteRenderer>().enabled=false;
                }
            }
            if(bestPoint==null){
                return;
            }else{
                //print(bestPoint.name);
                 bestPoint.transform.Find("sprite/arrow").GetComponent<SpriteRenderer>().enabled=true;

            }
        
}
public void Shoot(){

    bool foundPoint = SetHookPoint();
    if(foundPoint){
         StartCoroutine(PullToPointOverTimeCoroutine(pullTime));
    }
   

}
public void RemovePoints(){
    _lineRenderer.positionCount=0;
}

public void Update(){

    ShowGrappleUI();
    if(isPulling){
        KeepConnected();
    }
}
public void KeepConnected(){ 

    Vector3[] positions = new Vector3[2];
     positions[0]=new Vector3(_source.transform.position.x,_source.transform.position.y+_source.zpos,-_source.zpos);
     positions[1]=new Vector3(_dest.transform.position.x, _dest.transform.position.y + _dest.zpos, -_dest.zpos);
     _lineRenderer.positionCount=2;
     _lineRenderer.SetPositions(positions);

}



    public bool SetHookPoint(){


                
                 List<RaycastHit2D> results=new List<RaycastHit2D>();

            int numResults = Physics2D.CircleCast((Vector2)transform.position, _hookRadius,Vector2.zero, cf, results,Mathf.Infinity);
           // Collider2D[] allPoints = Physics2D.OverlapCircleAll((Vector2)transform.position, _hookRadius,LayerMask.GetMask("GrapplePoint"), float minDepth = -Mathf.Infinity, Mathf.Infinity);

            //print($"numresults {numResults}");
            Vector2 inputVector = MovementController.instance._currentInputState.inputVector.normalized;
            if(inputVector==Vector2.zero)return false;
            GameObject bestPoint = null;
            float maxScore=-Mathf.Infinity;
            foreach(RaycastHit2D hit in results){
               
                Vector2 toPoint = new Vector2(hit.point.x-transform.position.x,hit.point.y-transform.position.y).normalized; 
                print("iv,tp"+inputVector+","+toPoint);
                float score = Vector2.Dot(inputVector,toPoint);
                print("SCORE IS :"+score);
                if(score>maxScore){
                    maxScore=score;
                    bestPoint = hit.transform.gameObject;
                   // bestPoint.transform.Find("sprite/arrow").GetComponent<SpriteRenderer>().enabled=false;
                }
            }
            if(bestPoint==null){
                print("NO GRAPPLE POINT FOUND");
                return false;
            }else{
                print(bestPoint.name);
            }
        
        // List<GrapplePointScript> allGrapplePoints=new List<GrapplePointScript>(FindObjectsOfType<GrapplePointScript>());

         GameObject grapplePoint = bestPoint;// allGrapplePoints[0].gameObject;
        // grappleObject.transform.Find("sprite/arrow").GetComponent<SpriteRenderer>().enabled=true;
         Physics p = grapplePoint.GetComponent<Physics>();
         PhysicsManager.instance.RegisterEntity(p);
         /*
         grapplePoint.transform.position = _phys.transform.position + new Vector3(
                                                                                MovementController.instance._currentInputState.inputVector.x,
                                                                                MovementController.instance._currentInputState.inputVector.y,
                                                                                0).normalized * _maxDist;

         p.zpos=_maxDist+_phys.zpos;
         */
         _source = _phys;
         _dest =p;
         return true;
        
    }

    public IEnumerator PullToPointOverTimeCoroutine(float t){

        isPulling=true;
        _phys.gravityScale=0f;
        _phys.lockGravityScale=true;
        _phys.fixXvel=true;
        _phys.fixYvel=true;
        _phys.fixZvel=true;
        MovementController.instance.canMove=false;
        _phys.xvel=0f;
        _phys.yvel=0f;
        _phys.zvel=0f;

        float curtime=0f;
        
       
        float numMovements = t/Time.fixedDeltaTime;
       // Vector3 perMovement = toPoint/numMovements;
    ;

        //yield return StartCoroutine(ConnectPointsCoroutine(a,b));

        while(Vector3.Distance(new Vector3(_source.transform.position.x,_source.transform.position.y,_phys.zpos),new Vector3(_dest.transform.position.x,_dest.transform.position.y,_dest.zpos))>0.1f){
                        Vector3 toPoint = - new Vector3(_phys.transform.position.x,_phys.transform.position.y,-_phys.zpos);
                        float interpolationFactor = curtime/t;//(curtime/t)*(curtime/t);
                       // Vector3 newPos = Vector3.MoveTowards(new Vector3(_source.transform.position.x,_source.transform.position.y,_phys.zpos),new Vector3(_dest.transform.position.x,_dest.transform.position.y,_dest.zpos),0.05f);
                        Vector3 newPos = Vector3.Lerp(
                                        
                                                            new Vector3(_source.transform.position.x,_source.transform.position.y,_phys.zpos),
                                                            new Vector3(_dest.transform.position.x,_dest.transform.position.y,_dest.zpos),
                                                            interpolationFactor
                                                            
                                                      );
                        _phys.transform.position = new Vector3(newPos.x,newPos.y,0f);
                

                        _phys.zpos=newPos.z;

                       // PhysicsManager.instance.ResolveAllCollisions();
                        curtime +=Time.fixedDeltaTime;
                        if(Vector3.Distance(new Vector3(_source.transform.position.x,_source.transform.position.y,_phys.zpos),new Vector3(_dest.transform.position.x,_dest.transform.position.y,_dest.zpos))<0.1f)break;
                        yield return new WaitForFixedUpdate();
        }
        _phys.lockGravityScale=false;
         _phys.fixXvel=false;
        _phys.fixYvel=false;
        _phys.fixZvel=false;
        MovementController.instance.canMove = true;
        isPulling=false;
        _phys.gravityScale=0f;//p.originalGravityScale;
        _phys.zvel=0f;
        RemovePoints();
        

        
    }
 

 
}
