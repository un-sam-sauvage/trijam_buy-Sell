using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSize;
    public float offset = 4.5f;
    public Node [,] grid ;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    
    private void CreateGrid(){
        grid = new Node [gridSize,gridSize];
        for(int i = 0 ; i < gridSize; i++){
           for(int j = 0 ; j < gridSize ; j++){
               grid[i,j] = new Node(new Vector3(i-offset,.5f,j-offset));
           }
        }
    }

    void OnDrawGizmos(){
        for(int i = 0 ; i < gridSize; i++){
            for (int j = 0; j < gridSize; j++)
            {
                Gizmos.DrawSphere(new Vector3(i-offset,0,j-offset),.1f);
            }
        }
        
    }
}
[System.Serializable]
public class Node {
    public Vector3 pos;
    public GameObject obj;
    public bool isVisited;
    public Node (Vector3 pos){
        this.pos = pos;
    }

}
