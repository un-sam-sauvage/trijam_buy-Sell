using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChildren : MonoBehaviour
{
    public List<Node> parentNeighbours;
    public List<GameObject> blocGroup;
    public int indexI,indexJ;
    public List<Node> AddNeighbours(List<Node> currentNeighbours,GridManager gridManager){
        
        List<Node> neighbours= new List<Node>();
        Vector2Int[] posToCheck = new Vector2Int[] {new Vector2Int(indexI,indexJ-1),new Vector2Int(indexI+1,indexJ), new Vector2Int(indexI,indexJ+1),new Vector2Int(indexI-1,indexJ)};

        foreach(Vector2Int pos in posToCheck){
            
            if(pos.x < gridManager.gridSize && pos.y < gridManager.gridSize && pos.x >= 0 && pos.y >= 0){
                Node node = gridManager.grid[pos.x,pos.y];
                if(node.obj != null && IsInSameTeam(node.obj) && node.pos != GetComponent<MeshRenderer>().bounds.center && !blocGroup.Contains(node.obj)){
                    Node parentNode = gridManager.grid[node.obj.GetComponentInParent<BlockToPlace>().indexI,node.obj.GetComponentInParent<BlockToPlace>().indexJ];
                    neighbours.Add(parentNode);   
                }
            }
        }
        
        parentNeighbours = neighbours;
        return neighbours; 
    }

    bool IsInSameTeam(GameObject otherObject){
        if(otherObject && otherObject.GetComponentInParent<BlockToPlace>().teamColor == gameObject.GetComponentInParent<BlockToPlace>().teamColor){
            return true;
        }
        return false;
    }
}
