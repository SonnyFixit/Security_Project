using UnityEngine;
using System.Collections.Generic;


public class LightFlicker2D : MonoBehaviour {

    
    [SerializeField] 
    float amountIntensity = 3f;
    [SerializeField] 
    float amountRange = 0.6f;
    [SerializeField] 
    float amountPos = 0.3f;
    [SerializeField] 
    float speed = 1f;

  
    float startIntensity;
    float startRange;
    Vector3 startPos;

    float randomSpeed;
    Vector3 targetValue = Vector3.zero;
    Vector3 currentValue = Vector3.zero;

   
    new UnityEngine.Rendering.Universal.Light2D light;

    void Start() 
    {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        startIntensity = light.intensity;
        startRange = light.intensity;
        startPos = transform.position;
        Randomize();
    }

    void Randomize() 
    {
        targetValue = Random.insideUnitSphere;
        randomSpeed = Random.Range(10f, 40f) * speed;
    }

    void Update() {
       
        if (Vector3.Distance(currentValue, targetValue) < 0.1f) 
        {
            Randomize();
        }
        
        currentValue = Vector3.Lerp(currentValue, targetValue, Time.deltaTime * randomSpeed);

        light.intensity = startIntensity + currentValue.x * amountIntensity;
        transform.position = startPos + currentValue * amountPos;
    }
}
