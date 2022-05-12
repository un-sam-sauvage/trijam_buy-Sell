using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    void Start()
    {
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
            outlineRenderer.enabled = true;

        }
    }
    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject objToClone = GetComponentInChildren<BlockChildren>().gameObject;
        Vector3 clonePos = objToClone.GetComponent<Renderer>().bounds.center;
        Vector3 clonePosCorrected = new Vector3(clonePos.x+.5f,clonePos.y,clonePos.z - .55f);
        GameObject outlineObject = Instantiate(objToClone, clonePosCorrected,  objToClone.transform.rotation,  objToClone.transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineObject.GetComponent<BlockChildren>().enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
