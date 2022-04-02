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

    [SerializeField] private bool isHouseSelected;
    [SerializeField] private GridManager gridManager;
    private RaycastHit _hit;
    [SerializeField]
    private Vector3 hitPoint;
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
            CheckHousePlacing();
        }
        PlaceHouse();
    }

    private bool CheckHousePlacing()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out _hit))
        {
            hitPoint = new Vector3(Mathf.Floor(_hit.point.x) + 0.5f, 0.5f, Mathf.Floor(_hit.point.z) + 0.5f);
            houseToPlace.gameObject.transform.position = hitPoint;
            return true;
        }

        return false;
    }

    private void PlaceHouse()
    {
        if (Input.GetMouseButton(0) && CheckHousePlacing())
        {
            var gridManagerGrid = gridManager.grid;
            for (int i = 0; i < gridManager.gridSize; i++)
            {
                for (int j = 0; j < gridManager.gridSize; j++)
                {
                    gridManagerGrid[i, j].pos = hitPoint;
                    gridManagerGrid[i, j].obj = houseToPlace.gameObject;
                    houseToPlace.transform.position = gridManagerGrid[i, j].pos;
                    isHouseSelected = false;
                }
            }
        }
    }
}
