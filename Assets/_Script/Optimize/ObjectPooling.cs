using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance { get; private set; }
    [SerializeField] protected List<GameObject> poolingPrefabs = new List<GameObject>();
    public Transform poolTransform;
    [SerializeField] protected int initNumber = 1;
    protected Dictionary<string, Queue<GameObject>> poolingData = new Dictionary<string, Queue<GameObject>>();
    private void Awake()
    {
        Instance = this;
        Init();
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.targetFrameRate = 60;
        }
    }



    protected void Init()
    {
        foreach (GameObject prefab in poolingPrefabs)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            poolingData.Add(prefab.tag, queue);

            for (int i = 0; i < initNumber; i++)
            {
                GameObject obj = Instantiate(prefab, poolTransform);
                poolingData[obj.tag].Enqueue(obj);
                obj.SetActive(false);
            }
        }
    }
    public void Despawn(GameObject obj)
    {
        obj.transform.SetParent(poolTransform);
        poolingData[obj.tag].Enqueue(obj);
        obj.SetActive(false);
    }
     

    public GameObject Spawn(GameObject prefab, Vector3 pos = default, Quaternion rotation = default, Transform parent = null)
    {
        GameObject obj;
        if (poolingData[prefab.tag].Count > 0)
        {

            obj = poolingData[prefab.tag].Dequeue();
            obj.transform.SetPositionAndRotation(pos, rotation);
            if (parent) obj.transform.parent = parent;
        }
        else
        {
            obj = Instantiate(prefab,pos,rotation,poolTransform);
            poolingData[prefab.tag].Enqueue(obj);
            return Spawn(prefab,pos,rotation,parent);

        }

        obj.SetActive(true);
   
      

        return obj;

    }

    public GameObject GetPoolingPrefab(string tag)
    {
        foreach (GameObject item in poolingPrefabs)
        {
            if (item.tag == tag) return item;
        }
        Debug.Log("can not find pooling prefab " + tag);
        return null;
    }
}
