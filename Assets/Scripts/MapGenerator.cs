using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

class Cube
{
    public GameObject theCube;
    public float creationTime;

    public Cube(GameObject t, float ct) 
    {
        theCube = t;
        creationTime = ct;
    }
}


public class MapGenerator : MonoBehaviour
{
    public GameObject player; 
   [SerializeField] public GameObject[] cubesPrefs = new GameObject[5];

    public int cubeSize; 
    public int countCubesX; 
    public int countCubesZ;
    Vector3 startPosition; 

    Hashtable cubes = new Hashtable();
    private GameObject [] ghosts = new GameObject[30];

    public GameObject ghost;
    private int ghostCount=30;

  

    IEnumerator TransformPos(GameObject[] ghosts)
    {

        for (int i = 0; i < ghostCount; i++)
        {
            if (ghosts[i] != null) { 
            Rigidbody gh = ghosts[i].GetComponent<Rigidbody>();

                if (Vector3.Distance(player.transform.position, gh.transform.position) > 100)
                {
                    Vector3 pos = player.transform.position;
                    Vector3 newRadius = new Vector3(Random.Range(pos.x + 70, pos.x - 70), 7f, 
                        Random.Range(pos.z + 70, pos.z - 70));
                    ghosts[i].transform.position = newRadius; 
            }
        }
     }
        yield return new WaitForEndOfFrame();
    }


    void Start()
    {
        this.gameObject.transform.position = Vector3.zero;
        startPosition = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        for (int x = -countCubesX; x < countCubesX; x++)
        {
            for (int z = -countCubesZ; z < countCubesZ; z++)
            {
                Vector3 position = new Vector3((x * cubeSize + startPosition.x), 0, (z * cubeSize + startPosition.z));
                GameObject t = (GameObject)Instantiate(cubesPrefs[Random.Range(0, 5)], position, Quaternion.identity);

                string cubeName = "Cube_" + ((int)(position.x)).ToString() + "_" + ((int)(position.z)).ToString();
                t.name = cubeName;
                Cube cube = new Cube(t, updateTime);
                cubes.Add(cubeName, cube);
            }
        }

      
        for (int i=0; i< ghostCount; i++)
        {
            Vector3 pos = player.transform.position;
            Vector3 newGhostPos = new Vector3(Random.Range(pos.x + 80, pos.x - 80), 7f,
                Random.Range(pos.z + 70, pos.z - 70));

            GameObject newGhost = Instantiate(ghost, newGhostPos, Quaternion.identity); 

            ghosts[i] = newGhost;
            
        }

    }


    void Update()
    {
        int xMove = (int)(player.transform.position.x - startPosition.x);  
        int zMove = (int)(player.transform.position.z - startPosition.z);

        if (Mathf.Abs(xMove) >= cubeSize || Mathf.Abs(zMove) >= cubeSize)
        {
            float updateTime = Time.realtimeSinceStartup; 

            int playerX = (int)(Mathf.Floor(player.transform.position.x / cubeSize) * cubeSize);
            int playerZ = (int)(Mathf.Floor(player.transform.position.z / cubeSize) * cubeSize);

            int currentX = (int)player.transform.position.x;
            int currentZ = (int)player.transform.position.z;

  

            for (int x = -countCubesX; x < countCubesX; x++)
            {
                for (int z = -countCubesZ; z < countCubesZ; z++)
                {
                    StartCoroutine(TransformPos(ghosts));
                    Vector3 position = new Vector3((x * cubeSize + playerX), 0, (z * cubeSize + playerZ));
                        string cubeName = "Cube_" + ((int)(position.x)).ToString() + "_" + ((int)(position.z)).ToString(); 

                        if (!cubes.ContainsKey(cubeName)) 
                        {
                            GameObject c = (GameObject)Instantiate(cubesPrefs[Random.Range(0, 5)], position, Quaternion.identity);
                            c.name = cubeName;
                            Cube cube = new Cube(c, updateTime);
                            cubes.Add(cubeName, cube);
                        }
                        else
                        {
                            (cubes[cubeName] as Cube).creationTime = updateTime;
                        }
                }
            }


            Hashtable newTerrain = new Hashtable(); 

               foreach (Cube tls in cubes.Values)
               {
                   if (tls.creationTime != updateTime) 
                   {
                       Destroy(tls.theCube);
                   }
                   else
                   {
                       newTerrain.Add(tls.theCube.name, tls);
                   }
               }
               
               cubes = newTerrain;    
            startPosition = player.transform.position;
          

        }
    }


    


}
    


    

