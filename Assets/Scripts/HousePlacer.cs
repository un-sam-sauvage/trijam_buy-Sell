using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousePlacer : MonoBehaviour
{
    public BlockToPlace houseToPlace;

    private Camera _cam;

    public bool isHouseSelected;
    [SerializeField] private GridManager gridManager;
    private RaycastHit _hit;
    [SerializeField]
    private Vector3 hitPoint;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private GameObject houseSpawnParticle;

    [SerializeField] private int costForSpawnHouses = 10;

    public Material [] materialsRoof;
    public Material [] materialsWall;
    public Material [] materialsGround;
    public event Action onPlayerLoose;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHouseSelected)
        {
            if(houseToPlace == null){
                Color teamColor = new Color();
                GameObject house = Instantiate(GetComponent<HandleClickOnImage>().obj, new Vector3(10,10,10) ,Quaternion.identity);
                Material [] materials = house.GetComponentInChildren<Renderer>().materials;
                int index = UnityEngine.Random.Range(0,materialsRoof.Length);
                for(int i = 0; i< materials.Length;i++){
                    if(materials[i].name.Contains("M_roof")){
                        materials[i].color = materialsRoof[index].color;
                        teamColor = materials[i].color;
                        }
                    else if(materials[i].name.Contains("M_wall")){
                        materials[i].color = materialsWall[UnityEngine.Random.Range(0,materialsWall.Length)].color;
                    }
                    if(i > 0 && i < house.GetComponentsInChildren<Renderer>().Length){
                        house.GetComponentsInChildren<Renderer>()[i].material.color = materialsGround[index].color;
                    }
                }
                houseToPlace = house.GetComponent<BlockToPlace>();
                houseToPlace.teamColor = teamColor;
            }
            else{
                CheckHousePlacing();
                PlaceHouse();
            }

        }
        
    }
    private bool CheckHousePlacing()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit, Mathf.Infinity, groundMask))
        {
            hitPoint = new Vector3(Mathf.Floor(_hit.point.x) + 1, 0, Mathf.Floor(_hit.point.z) + 1f);
            houseToPlace.gameObject.transform.position = hitPoint;
            return true;
        }

        return false;
    }

    private void PlaceHouse()
    {
        if (Input.GetMouseButtonDown(0) && CheckHousePlacing())
        {
            Vector3 houseToPlacePos = new Vector3 (houseToPlace.transform.position.x -.5f,houseToPlace.transform.position.y,houseToPlace.transform.position.z-.5f);
            var gridManagerGrid = gridManager.grid;
            for (int i = 0; i < gridManager.gridSize; i++)
            {
                for (int j = 0; j < gridManager.gridSize; j++)
                {
                    if( houseToPlace && gridManagerGrid[i,j].pos == houseToPlacePos){
                        gridManagerGrid[i, j].obj = houseToPlace.gameObject;
                        houseToPlace.IsPlaced(i,j);
                        houseToPlace.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
                        GameObject spawnParticle = Instantiate(houseSpawnParticle, houseToPlace.transform.position,
                            houseSpawnParticle.transform.rotation);
                        Destroy(spawnParticle, 2f);
                        CalculatedCost();
                        isHouseSelected = false;
                        houseToPlace = null;
                    }
                }
            }
        }
    }

    private void PlayerLoose()
    {
        onPlayerLoose?.Invoke();
    }
    private void CalculatedCost()
    {
       GameManager.Instance.SetUpPlayerMoney(-costForSpawnHouses);
       if (GameManager.Instance.basePlayerMoney <= 0)
       {
           PlayerLoose();
       }
    }
}
