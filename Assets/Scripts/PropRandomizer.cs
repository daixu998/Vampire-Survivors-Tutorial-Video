using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rad = Random.Range(0, propPrefabs.Count);
            GameObject prop =  Instantiate(propPrefabs[rad], sp.transform.position, Quaternion.identity);   
            prop.transform.SetParent(sp.transform);

        }
        
    }
}
