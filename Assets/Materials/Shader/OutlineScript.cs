using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private List<Renderer> outlineRenderers;

    void Start()
    {
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            outlineRenderers = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
            foreach(var outlineRenderer in outlineRenderers){
                outlineRenderer.enabled = true;
            }
        }
    }
    List<Renderer> CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        List<Renderer> renderers = new List<Renderer>();
        foreach(BlockChildren childrenToClone in GetComponentsInChildren<BlockChildren>()){
            GameObject objToClone = childrenToClone.gameObject;
            Vector3 clonePos = objToClone.transform.TransformPoint(Vector3.zero);
            Vector3 clonePosCorrected = new Vector3(clonePos.x,.25f , clonePos.z );
            GameObject outlineObject = Instantiate(objToClone, clonePosCorrected,  objToClone.transform.rotation,  objToClone.transform);
            Renderer rend = outlineObject.GetComponent<Renderer>();

            rend.material = outlineMat;
            rend.material.SetColor("_OutlineColor", color);
            rend.material.SetFloat("_Scale", scaleFactor);
            rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            outlineObject.GetComponent<BlockChildren>().enabled = false;
            outlineObject.GetComponent<Collider>().enabled = false;
            rend.enabled = false;
            renderers.Add(rend);
        }       

        return renderers;
    }
}
