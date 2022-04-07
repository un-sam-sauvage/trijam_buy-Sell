using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChildren : MonoBehaviour
{
    public List<Node> parentNeighbours;
    public List<GameObject> blocGroup;
    public List<Node> AddNeighbours(List<Node> currentNeighbours,GridManager gridManager,int indexI,int indexJ){
        Debug.Log(transform.InverseTransformPoint(transform.position));
        List<Node> neighbours= new List<Node>();
        Vector2Int[] posToCheck = CheckIndexes(indexI,indexJ,gridManager);
        foreach(Vector2Int pos in posToCheck){
            Debug.Log(pos);
            Node node = gridManager.grid[pos.x,pos.y];
            if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
                if(node.obj != null && IsInSameTeam(node.obj) && node.pos != gameObject.transform.position && !blocGroup.Contains(node.obj)){
                    neighbours.Add(node);   
                }
            }
        }
        
        parentNeighbours = neighbours;
        return neighbours; 
    }

    bool IsInSameTeam(GameObject otherObject){
        if(otherObject.GetComponent<BlockToPlace>().teamColor == gameObject.GetComponentInParent<BlockToPlace>().teamColor){
            return true;
        }
        return false;
    }

    Vector2Int[] CheckIndexes(int indexI,int indexJ,GridManager gridManager){
        Vector2Int[] posToCheck = new Vector2Int[] {new Vector2Int(indexI,indexJ-1),new Vector2Int(indexI+1,indexJ), new Vector2Int(indexI,indexJ+1),new Vector2Int(indexI-1,indexJ)};
        foreach(Vector2Int pos in posToCheck){
            
                if(gameObject.transform.position == gridManager.grid[pos.x,pos.y].pos){
                    gridManager.grid[pos.x,pos.y].obj = gameObject.GetComponentInParent<GameObject>();
                    indexI = pos.x;
                    indexJ = pos.y;
                }
        }
        posToCheck = new Vector2Int[] {new Vector2Int(indexI,indexJ-1),new Vector2Int(indexI+1,indexJ), new Vector2Int(indexI,indexJ+1),new Vector2Int(indexI-1,indexJ)};
        return posToCheck;
    }
}
