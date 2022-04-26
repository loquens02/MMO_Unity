using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    float duration = 3.0f;
    [SerializeField]
    Material teamAcolor;
    [SerializeField]
    Material teamBcolor;
    /**
     * <see href="https://docs.unity3d.com/ScriptReference/Material-color.html"/>
     * <see href="https://docs.unity3d.com/ScriptReference/LayerMask.NameToLayer.html"/>
     */
    private void OnTriggerEnter(Collider other)
    {
        Managers.Sound.Play("water-lowOnly-0.1dB-3s");

        var cubeRenderer = GetComponent<Renderer>();
        if (other.gameObject.layer== LayerMask.NameToLayer("TeamA"))
        {
            //Debug.Log("basket meet TeamA");
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            cubeRenderer.material.color = Color.Lerp(cubeRenderer.material.color, Color.red, lerp);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("TeamB"))
        {
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            cubeRenderer.material.color = Color.Lerp(cubeRenderer.material.color, Color.blue, lerp);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        Managers.Sound.Clear();
    }
}
