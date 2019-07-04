using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour {
    public Transform target;
    public GameObject NPCA, NPCB;
    public GameObject cloneNPCA,cloneNPCB;
    float jarakNpcA,jarakNpcB;

    public Transform spawnEnemyA, spawnEnemyB;

    Grid grid;

    Main_menu fromMenu;
    public float speedMusuh;

	void Awake() {
		grid = GetComponent<Grid> ();
	}
    private void Start()
    {
        fromMenu = GameObject.Find("ScriptManaggerMenu").GetComponent<Main_menu>();
        speedMusuh =  fromMenu.speedNpc;//0.3f;
        if (cloneNPCA == null)
        {
            spawningEnemies();
        }

        if (cloneNPCB == null)
        {
            spawningEnemiesB();
        }
    }

    void Update() {
        if(cloneNPCA != null)
        {
            jarakNpcA = Vector3.Distance(cloneNPCA.transform.position, target.position);            
        }
        else
        {
            StartCoroutine("spawn");
        }
        if (cloneNPCB != null)
        {            
            jarakNpcB = Vector3.Distance(cloneNPCB.transform.position, target.position);
        }
        else
        {
            StartCoroutine("spawnB");
        }
        

        if (jarakNpcA < jarakNpcB && cloneNPCA != null)
		FindPath (cloneNPCA.transform.position, target.position);
       
        if (jarakNpcB < jarakNpcA && cloneNPCB != null)
            FindPath(cloneNPCB.transform.position, target.position);
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(2);
        spawningEnemies();
    }
    void spawningEnemies()
    {        
        cloneNPCA = Instantiate(NPCA, spawnEnemyA.transform.position, NPCA.transform.rotation) as GameObject;
        StopCoroutine("spawn");
    }
    IEnumerator spawnB()
    {
        yield return new WaitForSeconds(2);
        spawningEnemiesB();
    }
    void spawningEnemiesB()
    {
        // Debug.Log(i);
        cloneNPCB = Instantiate(NPCB, spawnEnemyB.transform.position, NPCB.transform.rotation) as GameObject;
        StopCoroutine("spawnB");
    }

    void FindPath(Vector3 startPos, Vector3 targetPos) {
		NodeB startNode = grid.NodeFromWorldPointB(startPos);
		NodeB targetNode = grid.NodeFromWorldPointB(targetPos);

		List<NodeB> openSet = new List<NodeB>();
		HashSet<NodeB> closedSet = new HashSet<NodeB>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			NodeB node = openSet[0];
			for (int i = 1; i < openSet.Count; i ++) {
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode) {
				RetracePath(startNode,targetNode);
				return;
			}

			foreach (NodeB neighbour in grid.GetNeighboursB(node)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(NodeB startNode, NodeB endNode) {
		List<NodeB> path = new List<NodeB>();
		NodeB currentNode = endNode;

		while (currentNode != startNode) {
            if (path != null)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
		}
		path.Reverse();

		grid.path = path;
        if(path.Count >0)           
            MovementTarget(path);
    }
    void MovementTarget(List<NodeB> path)
    {
        Debug.Log(path.Count);
        if (path != null)
        {
            if (jarakNpcA < jarakNpcB)
            {
                cloneNPCA.transform.position = Vector3.MoveTowards(cloneNPCA.transform.position
                , new Vector3(path[0].worldPosition.x, cloneNPCA.transform.position.y, path[0].worldPosition.z)
                , speedMusuh *Time.deltaTime);
                cloneNPCA.transform.LookAt(target);
            }                
            if (jarakNpcB < jarakNpcA)
            {
                cloneNPCB.transform.position = Vector3.MoveTowards(cloneNPCB.transform.position
                , new Vector3(path[0].worldPosition.x, cloneNPCB.transform.position.y, path[0].worldPosition.z)
                , speedMusuh * Time.deltaTime);
                cloneNPCB.transform.LookAt(target);
            }          
                
        }
        else
        {
            return;
        }
    }

        int GetDistance(NodeB nodeA, NodeB nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}
