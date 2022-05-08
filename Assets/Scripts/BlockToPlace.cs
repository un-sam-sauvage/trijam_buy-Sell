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
            children.transform.position = new Vector3 (children.transform.position.x, children.transform.position.y -.1f,children.transform.position.z);
            foreach(Transform obj in children.GetComponentInParent<BlockToPlace>().GetComponentsInChildren<Transform>()){
                if(!obj.GetComponent<BlockChildren>() && obj.transform.position.y == 0 && !obj.GetComponent<BlockToPlace>()){
                    Debug.Log("je réduis la position");
                    obj.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y - .1f, obj.transform.position.z);
                }else{
                    Debug.Log("je n'ai pas trouvé d'objet " + obj.transform.position);
                }
            }
            for (int x = 0; x < gridManager.gridSize; x++)
            {   
                for (int y = 0; y < gridManager.gridSize; y++)
                {
                    if(gridManager.grid[x,y].pos == children.GetComponent<MeshRenderer>().bounds.center){
                        gridManager.grid[x,y].obj = children.gameObject;
                        children.indexI = x;
                        children.indexJ = y;
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
            neighbours.AddRange(children.AddNeighbours(neighbours,gridManager));
        }
        neighbours = ClearNeighbours();
        foreach(Node neighbour in neighbours){
            neighbour.obj.GetComponentInParent<BlockToPlace>().neighbours.Add(gridManager.grid[indexI,indexJ]);
        }
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
