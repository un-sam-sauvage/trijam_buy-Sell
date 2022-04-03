using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockToPlace : MonoBehaviour
{
    private int _indexI,_indexJ;
    public GridManager gridManager;
    public List<Node> neighbours;

    void Start(){
        gridManager = GameObject.FindObjectOfType<GridManager>();
    }

    public void IsPlaced(int i,int j){
        Debug.Log("je suis placé");
        _indexI = i;
        _indexJ = j;
        Vector2Int[] posToCheck = new Vector2Int[] {new Vector2Int(_indexI,_indexJ-1),new Vector2Int(_indexI+1,_indexJ), new Vector2Int(_indexI,_indexJ+1),new Vector2Int(_indexI-1,_indexJ)};
        CheckNeighbours(posToCheck);
    }

    void CheckNeighbours(Vector2Int [] posToCheck){
        foreach(Vector2Int pos in posToCheck){
            if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
                Node node = gridManager.grid[pos.x,pos.y];
                if(node.obj != null && node.obj.tag == gameObject.tag && node.pos != gameObject.transform.position){
                    neighbours.Add(node);
                    node.obj.GetComponent<BlockToPlace>().neighbours.Add(gridManager.grid[_indexI,_indexJ]);
                }
            }
        }
    }
}
