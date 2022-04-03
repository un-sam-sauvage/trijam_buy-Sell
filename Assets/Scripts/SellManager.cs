using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    private Camera _camera;
    private static SellManager _sellManager;

    public static SellManager Instance => _sellManager;
    [SerializeField] private LayerMask houseMask;
    [SerializeField] private bool isNeighborhoodCanBeSell;
    [SerializeField] private int numberMinNeighborForSell;
    [SerializeField] private int moneyEarnedFromSelling;

    private BlockToPlace _currentCheckedHouse;
    public List<GameObject> housesList = new List<GameObject>();
    void Awake()
    {
        _sellManager = this;
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectHouses();
        }
    }

    private void SelectHouses()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, houseMask) )
        {
            CheckForNeighbor(hit);
        }
    }

    private void CheckForNeighbor(RaycastHit hit)
    {
        if (hit.collider.gameObject.GetComponent<BlockToPlace>())
        {
            _currentCheckedHouse = hit.collider.gameObject.GetComponent<BlockToPlace>();
            ClearVariables();
            
            housesList.Add(_currentCheckedHouse.gameObject);
            GameObject.FindObjectOfType<GridManager>().grid[_currentCheckedHouse.indexI,_currentCheckedHouse.indexJ].isVisited = true;
            AddNeighboursToList(_currentCheckedHouse);
            
            if (housesList.Count >= numberMinNeighborForSell)
            {
                isNeighborhoodCanBeSell = true;
            }
        }
    }
    private void AddNeighboursToList(BlockToPlace startPoint){
        foreach(Node node in startPoint.neighbours){
            if(!node.isVisited){
                node.isVisited = true;
                housesList.Add(node.obj);
                AddNeighboursToList(node.obj.GetComponent<BlockToPlace>());
            }
        }
    }

    private void ClearVariables()
    {
        housesList.Clear();
        isNeighborhoodCanBeSell = false;
        foreach(Node node in GameObject.FindObjectOfType<GridManager>().grid){
            node.isVisited = false;
        }
    }


 /// <summary>
 /// Code for button "SELL"
 /// </summary>
    public void SellNeighborhood()
    {
        if (isNeighborhoodCanBeSell)
        {
            foreach (var house  in housesList)
            {
                Destroy(house);
            }
            GameManager.Instance.SetUpPlayerMoney(moneyEarnedFromSelling);
            ClearVariables();
        }
    }
}
