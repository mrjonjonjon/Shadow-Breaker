using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PhysicsLight : MonoBehaviour
{
    public CircleCollider2D _collider2D;
    public List<Vector3> points;
    public List<Vector3> lightPoints;
        [Range(0f, 10f)]

    public float zpos;
    public PolygonCollider2D _polygonCollider;

        public LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        _collider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DrawLight();
        zpos=transform.parent.GetComponent<Physics>().zpos;
       // transform.localPosition=Vector2.up*zpos;
    }

    public void DrawLight(){
            List<RaycastHit2D> results=new List<RaycastHit2D>();
                ContactFilter2D cf=new ContactFilter2D();
            int count=0;
            int numhits=Physics2D.CircleCast(_collider2D.gameObject.transform.position,_collider2D.radius, Vector2.zero,cf.NoFilter(),results,Mathf.Infinity); 
            points.Clear();

            foreach(RaycastHit2D hit in results){   
                Collider2D col=hit.collider;
                if(GameObject.ReferenceEquals(_collider2D.transform.gameObject, col.transform.gameObject))continue;
                if(col.gameObject.tag!="PhysicsEntity")continue;
                if(col.transform.parent.gameObject.tag=="Player")continue;

                if(zpos<col.transform.parent.GetComponent<Physics>().zpos 
                || zpos>col.transform.parent.GetComponent<Physics>().zpos+col.transform.parent.GetComponent<Physics>().height)continue;

                Vector3 point= new Vector3(hit.collider.bounds.min.x,hit.collider.bounds.min.y,0);
                Vector3 point2= new Vector3(hit.collider.bounds.min.x,hit.collider.bounds.max.y,0);
                Vector3 point3= new Vector3(hit.collider.bounds.max.x,hit.collider.bounds.min.y,0);
                Vector3 point4= new Vector3(hit.collider.bounds.max.x,hit.collider.bounds.max.y,0);

                points.Add(point);
                points.Add(point2);
                points.Add(point3);
                points.Add(point4);
               

            }

                lightPoints.Clear();
                Vector3 centroid=Vector3.zero;
                lightPoints.Add((Vector3)transform.position+ Vector3.up*zpos);
                centroid+=(Vector3)transform.position+ Vector3.up*zpos;
                                       // lightPoints.Add((Vector3)transform.position);

                List<RaycastHit2D> results2=new List<RaycastHit2D>();
                ContactFilter2D cf2=new ContactFilter2D();
                cf2.useLayerMask=true;
                cf2.useTriggers=true;
                 LayerMask layerMask;
                 layerMask=LayerMask.GetMask("Obstacles");
                cf2.SetLayerMask(layerMask);
                foreach(Vector3 pt in points){

                  // RaycastHit2D hit =  Physics2D.Raycast(transform.position, (Vector2)(pt-transform.position),Mathf.Infinity, LayerMask.GetMask("Obstacles"),-Mathf.Infinity,Mathf.Infinity);
                   Physics2D.Raycast(transform.position, (Vector2)(pt-transform.position), cf2, results2, Mathf.Infinity);
                    results2.Sort(delegate(RaycastHit2D r1, RaycastHit2D r2) {
                    return (r1.fraction).CompareTo(r2.fraction);
                });
                        foreach(RaycastHit2D hit2 in results2){
                             if(hit2.collider==null){continue;}
                             if(hit2.collider.gameObject.tag!="PhysicsEntity"){continue;}
                             if(hit2.collider.transform.parent.GetComponent<Physics>().zpos + hit2.collider.transform.parent.GetComponent<Physics>().height<=zpos){continue;}
                             if(GameObject.ReferenceEquals(_collider2D.transform.gameObject, hit2.collider.transform.gameObject))continue;
                             if(hit2.collider.transform.parent.gameObject.tag=="Player")continue;


                              lightPoints.Add((Vector3)hit2.point + Vector3.up*zpos);
                              centroid+=(Vector3)hit2.point + Vector3.up*zpos;
                              break;
                        }
                        
                  
                  

                }
                centroid/=lightPoints.Count;
                lightPoints.Sort(delegate(Vector3 v1, Vector3 v2) {
                    if(v1==transform.position+Vector3.up*zpos ){
                        return (0).CompareTo(1);
                    }else if(v2==transform.position + Vector3.up*zpos){
                        return (1).CompareTo(0);
                    }
                    return Mathf.Atan2(v1.x-transform.position.x, v1.y-transform.position.y-zpos).CompareTo(Mathf.Atan2(v2.x-transform.position.x, v2.y-transform.position.y-zpos));
                });
                
        lr.positionCount=lightPoints.Count;
        lr.SetPositions(lightPoints.ToArray());
       // _polygonCollider.SetPath(0,lightPoints.ToArray());
        //_polygonCollider.offset=(Vector2)_polygonCollider.transform.position;
        
                


    }
}
