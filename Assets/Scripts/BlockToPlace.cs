using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockToPlace : MonoBehaviour
{
    [HideInInspector] public int indexI,indexJ;
    [HideInInspector] public GridManager gridManager;
    public List<Node> neighbours;
    public Color teamColor;

    void Start(){
        gridManager = GameObject.FindObjectOfType<GridManager>();
    }

    public void IsPlaced(int i,int j){
        foreach (BlockChildren children in GetComponentsInChildren<BlockChildren>()){
            for (int x = 0; x < gridManager.gridSize; x++)
            {   
                for (int y = 0; y < gridManager.gridSize; y++)
                {
                    if(gridManager.grid[x,y].pos == children.gameObject.transform.position){
                        Debug.Log("j'ajoute une node");
                        gridManager.grid[x,y].obj = children.gameObject;
                    }
                }
            }
        }
        Debug.Log("je suis placé");
        indexI = i;
        indexJ = j;
        Vector2Int[] posToCheck = new Vector2Int[] {new Vector2Int(indexI,indexJ-1),new Vector2Int(indexI+1,indexJ), new Vector2Int(indexI,indexJ+1),new Vector2Int(indexI-1,indexJ)};
        CheckNeighbours(posToCheck);
    }

    void CheckNeighbours(Vector2Int [] posToCheck){
        foreach (BlockChildren children in GetComponentsInChildren<BlockChildren>())
        {
            neighbours.AddRange(children.AddNeighbours(neighbours,gridManager,indexI,indexJ));
        }
        neighbours = ClearNeighbours();
        // foreach(Vector2Int pos in posToCheck){
        //     if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
        //         Node node = gridManager.grid[pos.x,pos.y];
        //         if(node.obj != null && IsInSameTeam(node.obj) && node.pos != gameObject.transform.position){
        //             neighbours.Add(node);
        //             node.obj.GetComponent<BlockToPlace>().neighbours.Add(gridManager.grid[indexI,indexJ]);
        //         }
        //     }
        // }
    }
    List<Node> ClearNeighbours(){
        List<Node> neighboursCleared = new List<Node>();
        foreach (Node neighbour in neighbours)
        {
            if(!neighboursCleared.Contains(neighbour)){
                neighboursCleared.Add(neighbour);
            }
        }
        return neighboursCleared;
    }
    bool IsInSameTeam(GameObject otherObject){
        if(otherObject.GetComponent<BlockToPlace>().teamColor == gameObject.GetComponentInParent<BlockToPlace>().teamColor){
            return true;
        }
        return false;
    }
}
