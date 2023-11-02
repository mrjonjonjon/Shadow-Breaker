using System.Collections;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public Light attachedLight; // Reference to the attached light component
    public float baseIntensity = 1f; // Normal light intensity
    public float lightningIntensity = 5f; // Intensity during lightning
    public float minWaitTime = 5f; // Minimum wait time before next lightning
    public float maxWaitTime = 15f; // Maximum wait time before next lightning
    public float lightningDuration = 0.1f; // Duration of the lightning flash

    void Start()
    {

        // Ensure the light starts with base intensity
        attachedLight.intensity = baseIntensity;
        
        // Start the lightning coroutine
        StartCoroutine(LightningEffect());
    }

    IEnumerator LightningEffect()
    {
        while(true)
        {
            // Wait for a random amount of time
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            int num_times=Random.Range(1,3);
            for(int i=0;i<num_times;i++){
                    // Set intensity to lightning value
                    attachedLight.intensity = lightningIntensity;

                    // Wait for a split second
                    yield return new WaitForSeconds(lightningDuration/num_times);
                              // Reset intensity to base value
                    attachedLight.intensity = baseIntensity;

                    yield return new WaitForSeconds(lightningDuration/num_times);
                    
            }
          

            // Reset intensity to base value
           // attachedLight.intensity = baseIntensity;
        }
    }
}
