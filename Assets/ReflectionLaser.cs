using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReflectionLaser : MonoBehaviour
{
    LineRenderer laser;
    public int numReflections=10;
    public AudioClip laserFireSFX;
    public AudioClip laserChargeSFX;
    public AudioClip laserBounceSFX;
    AudioSource audioSource;
    public float laserSpeed=5f;
    //public  List<Vector3> reflectionPoints=new List<Vector3>();
    
    public bool laserActive=false;
    ContactFilter2D filter2D;
    
    public List<Vector3> pointsCache=new List<Vector3>();

    
    // Start is called before the first frame update


    public void SetColor(Color c){
        laser.startColor=c;
        laser.endColor=c;
    }
    void Start()
    {
          laser = GetComponent<LineRenderer>();
          audioSource=GetComponent<AudioSource>();
       
        
        laser.positionCount = 0;

        filter2D = new ContactFilter2D{useTriggers = true,useLayerMask = true};
        	filter2D.SetLayerMask(LayerMask.GetMask("Wall","PlayerHitbox"));


    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void SetLaserColor(Color c){
        laser.material.SetColor("_Color",c);
    }


List<Vector3> CollectReflectionPoints(Vector3 startPos,Vector2 dir,int numReflections){
        List<Vector3> reflectionPoints=new List<Vector3>();
        reflectionPoints.Add(startPos);
        Vector2 castPoint=(Vector2)startPos;
        Vector2 castDir=dir;
        
        for(int i=0;i<numReflections;i++){
          
          //  RaycastHit2D hit = Physics2D.Raycast(castPoint,castDir,1000f,LayerMask.GetMask("Wall"));

          RaycastHit2D[] hits = new RaycastHit2D[1];
          int totalObjectsHit = Physics2D.Raycast(castPoint,castDir, filter2D, hits, 100f);
         
               bool found=false;
            for (int j = 0; j < totalObjectsHit; j++){
		          RaycastHit2D hit = hits[j];
                  
                  if( (Vector2.Distance((Vector2)hit.point,(Vector2)reflectionPoints[reflectionPoints.Count-1])<0.0001f )
                  || hit.collider==null){continue;}
                  found=true;
                 
	
		      
                    reflectionPoints.Add(((Vector3)hit.point));
                      castPoint=(Vector2)hit.point;//+0.01f*hit.normal;//-castDir;
                      castDir=Vector2.Reflect(castDir,hit.normal); 
                    break;
		        
	        }
            if(!found){
                reflectionPoints.Add(castPoint+100f*castDir);
            }
          
           
            
        }
pointsCache= new List<Vector3>(reflectionPoints);
        return reflectionPoints;


}

        


    public void ShootLaser(Vector3 startPos, Vector2 dir,bool instant=false,bool usecache=false,int numReflections=2){
       laserActive=true;
        List<Vector3>reflectionPoints;

        if(usecache){
            reflectionPoints=pointsCache;
        }else{
            reflectionPoints=CollectReflectionPoints(startPos, dir,numReflections);
        }
      
        if(instant){
            InstantLaser(reflectionPoints);
        }else{
            StartCoroutine(LerpLaser(reflectionPoints));
        }
       
    }

  

    public void InstantLaser(List<Vector3> reflectionPoints){
            laser.positionCount=reflectionPoints.Count;
            laser.SetPositions(reflectionPoints.ToArray());
            //yield return new WaitForSecondsRealtime(1f);
            //StartCoroutine(RemoveLaser());
           // yield return null;
    }


    IEnumerator LerpLaser(List<Vector3> points){
       
        List<Vector3> currentPoints=new List<Vector3>();
       
        for(int i=0;i<points.Count-1;i++){
            float timer=0f;
            Vector3 currentSource=points[i];
            currentPoints.Add(currentSource);
            currentPoints.Add(currentSource);//dummy

            Vector3 currentTarget=points[i+1]; 
            float totalTime=Vector3.Distance(currentSource,currentTarget)/laserSpeed;
            while(timer<totalTime){
                timer+=Time.deltaTime;
                   Vector3 laserEndpoint=Vector3.Lerp(currentSource,currentTarget,timer/totalTime);
                   //currentPoints.RemoveAt(i+1);
                   // currentPoints.Insert(i+1,laserEndpoint);
                    currentPoints[i+1]=laserEndpoint;
                    laser.positionCount=currentPoints.Count;
                    laser.SetPositions(currentPoints.ToArray());
                    
                    yield return null;
             }

             audioSource.PlayOneShot(laserBounceSFX);
             if(i!=points.Count-1){
                 currentPoints.RemoveAt(i+1);
             }
             
          
        
        }  
        //yield return new WaitForSecondsRealtime(1f);
         // StartCoroutine(RemoveLaser());
    }

public void RemoveLaser(){
    laser.positionCount=0;
    laserActive=false;
    //StartCoroutine(RemoveLaserCoroutine());
}
    IEnumerator RemoveLaserCoroutine(){
        Vector3[]pointsCopyArray=new Vector3[laser.positionCount];
        laser.GetPositions( pointsCopyArray);
        List<Vector3>pointsCopyList= new List<Vector3>(pointsCopyArray);
        int temp=laser.positionCount;
        for(int i=0;i<temp;i++){
            pointsCopyList.RemoveAt(0);
            laser.positionCount=pointsCopyList.Count;
            laser.SetPositions(pointsCopyList.ToArray());

            yield return null;
        }
        laserActive=false;
    }

     Vector2 ChooseDirection(){
                 switch (Random.Range(0,4)){
            case 0:  
                return new Vector2(1,1);
                break;
            case 1:
                               return new Vector2(-1,1);

                break;
            case 2:
                               return new Vector2(1,-1);

                break;
            default:
                                return new Vector2(-1,-1);

                break;
            }
     
    }
}
