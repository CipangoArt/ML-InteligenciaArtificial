using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField] BoxCollider bc;
    [SerializeField] GameObject OfudaBullet;
    public List<GameObject> pooledObjects;
    public int amountToPool;

    void Awake()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(OfudaBullet);
            tmp.transform.SetParent(gameObject.transform.parent);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void SpawnBullet()
    {
        Bounds bounds = GetComponent<BoxCollider>().bounds;
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);

        GameObject ofudaBullet = GetPooledObject();
        if (ofudaBullet != null)
        {
            ofudaBullet.transform.position = bounds.center + new Vector3(offsetX, offsetY, offsetZ);
            ofudaBullet.SetActive(true);
        }

    }

     IEnumerator SpawnRoutine()
    {
        SpawnBullet();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SpawnRoutine());
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
