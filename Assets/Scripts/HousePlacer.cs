using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
                GameObject house = Instantiate(GetComponent<HandleClickOnImage>().obj, new Vector3(10,10,10) ,Quaternion.identity);
                houseToPlace = house.GetComponent<BlockToPlace>();
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
            hitPoint = new Vector3(Mathf.Floor(_hit.point.x) + 0.5f, 0, Mathf.Floor(_hit.point.z) + 0.5f);
            houseToPlace.gameObject.transform.position = hitPoint;
            return true;
        }

        return false;
    }

    private void PlaceHouse()
    {
        if (Input.GetMouseButtonDown(0) && CheckHousePlacing())
        {
            var gridManagerGrid = gridManager.grid;
            for (int i = 0; i < gridManager.gridSize; i++)
            {
                for (int j = 0; j < gridManager.gridSize; j++)
                {
                    if( houseToPlace && gridManagerGrid[i,j].pos == houseToPlace.transform.position){
                        gridManagerGrid[i, j].obj = houseToPlace.gameObject;
                        houseToPlace.transform.position = gridManagerGrid[i, j].pos;
                        houseToPlace.IsPlaced(i,j);
                        houseToPlace.gameObject.GetComponent<BoxCollider>().enabled = true;
                        isHouseSelected = false;
                        houseToPlace = null;
                    }
                    
                }
            }
        }
    }
}
