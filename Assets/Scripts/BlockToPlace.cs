using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockToPlace : MonoBehaviour
{
    private int _indexI,_indexJ;
    public GridManager gridManager;
    public List<Node> neighbours;
    bool _isPlaced;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < gridManager.gridSize; i++)
        {
            for (int j = 0; j < gridManager.gridSize; j++)
            {
                if(gameObject.transform.position == gridManager.grid[i,j].pos && !_isPlaced){
                    _isPlaced = true;
                    Debug.Log("je suis placé");
                    _indexI = i;
                    _indexJ = j;
                    gridManager.grid[i,j].obj = gameObject;
                    Vector2Int[] posToCheck = new Vector2Int[] {new Vector2Int(_indexI,_indexJ-1),new Vector2Int(_indexI+1,_indexJ), new Vector2Int(_indexI,_indexJ+1),new Vector2Int(_indexI-1,_indexJ)};
                    CheckNeighbours(posToCheck);
                }
            }
        }
    }

    void CheckNeighbours(Vector2Int [] posToCheck){
        foreach(Vector2Int pos in posToCheck){
            if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
                Node node = gridManager.grid[pos.x,pos.y];
                if(node.obj != null && node.obj.tag == gameObject.tag){
                    neighbours.Add(node);
                    neighbours = addNeighbourNeighbours(node.obj.GetComponent<BlockToPlace>().neighbours,neighbours);
                    node.obj.GetComponent<BlockToPlace>().neighbours.Add(gridManager.grid[_indexI,_indexJ]);
                    node.obj.GetComponent<BlockToPlace>().neighbours = addNeighbourNeighbours(neighbours , node.obj.GetComponent<BlockToPlace>().neighbours);
                }
            }
        }
    }

    List<Node> addNeighbourNeighbours (List<Node> listToFilter , List<Node> originalList){
        List<Node> nodesToReturn = originalList;
        foreach(Node neighbourNeighbour in listToFilter){
            foreach(Node neighbour in neighbours){
                
                if(neighbourNeighbour.pos != neighbour.pos){
                    nodesToReturn.Add(neighbourNeighbour);
                }
            }
        }
        return nodesToReturn;
    }
}
