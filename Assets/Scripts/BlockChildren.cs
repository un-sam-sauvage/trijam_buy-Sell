using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChildren : MonoBehaviour
{
    public List<Node> parentNeighbours;
    public List<GameObject> blocGroup;
    public List<Node> AddNeighbours(List<Node> currentNeighbours,GridManager gridManager,int indexI,int indexJ){
        
        List<Node> neighbours= new List<Node>();
        Vector2Int[] posToCheck = CheckIndexes(indexI,indexJ,gridManager);
        foreach(Vector2Int pos in posToCheck){
            // Debug.Log($"je regarde à la position {pos} s'il n'y a pas une node");
            Node node = gridManager.grid[pos.x,pos.y];
            if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
                if(node.obj != null && IsInSameTeam(node.obj) && node.pos != GetComponent<MeshRenderer>().bounds.center && !blocGroup.Contains(node.obj)){
                    neighbours.Add(node);   
                }else{
                    Debug.Log($"{node.obj} {IsInSameTeam(node.obj)}");
                }
            }
        }
        
        parentNeighbours = neighbours;
        return neighbours; 
    }

    bool IsInSameTeam(GameObject otherObject){
        if(otherObject && otherObject.GetComponentInParent<BlockToPlace>().teamColor == gameObject.GetComponentInParent<BlockToPlace>().teamColor){
            return true;
        }else{
            Debug.Log(otherObject.GetComponentInParent<BlockToPlace>());
        }
        return false;
    }

    Vector2Int[] CheckIndexes(int indexI,int indexJ,GridManager gridManager){
        Vector2Int[] posToCheck = new Vector2Int[] {new Vector2Int(indexI,indexJ-1),new Vector2Int(indexI+1,indexJ), new Vector2Int(indexI,indexJ+1),new Vector2Int(indexI-1,indexJ)};
        foreach(Vector2Int pos in posToCheck){
            if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
                if(GetComponent<MeshRenderer>().bounds.center == gridManager.grid[pos.x,pos.y].pos){
                    gridManager.grid[pos.x,pos.y].obj = gameObject.GetComponentInParent<GameObject>();
                    indexI = pos.x;
                    indexJ = pos.y;
                }
            }
        }
        posToCheck = new Vector2Int[] {new Vector2Int(indexI,indexJ-1),new Vector2Int(indexI+1,indexJ), new Vector2Int(indexI,indexJ+1),new Vector2Int(indexI-1,indexJ)};
        return posToCheck;
    }
}
