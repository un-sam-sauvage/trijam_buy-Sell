using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialTest : MonoBehaviour
{
    public GameObject obj;
    public Material materialRoof;
    public Material [] materialsWall;
    // Start is called before the first frame update
    void Start()
    {
        Material [] materials = obj.GetComponentInChildren<Renderer>().materials;
        for(int i = 0; i< materials.Length;i++){
            if(materials[i].name.Contains("M_roof")){
                materials[i].color = materialRoof.color;
            }else if(materials[i].name.Contains("M_wall")){
                materials[i].color = materialsWall[Random.Range(0,materialsWall.Length)].color;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
