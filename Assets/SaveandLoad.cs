using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveandLoad : MonoBehaviour
{
    private List<GameObject> sushis;
    private SaveObject saveObject;
    private GameObject tuna, yellowfish, salmon, roeboat, shrimp;

    // Start is called before the first frame update
    void Awake()
    {
        sushis = new List<GameObject>();
        saveObject = new SaveObject();

        tuna = GameObject.Find("Tuna");
        yellowfish = GameObject.Find("Yellowfish");
        salmon = GameObject.Find("Salmon");
        roeboat = GameObject.Find("RoeBoat");
        shrimp = GameObject.Find("Shrimp");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Saving");
            SaveObjects();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            Debug.Log("Loading");
            LoadObjects();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearSave();
        }
    }

    public void ClearSave()
    {
        if (File.Exists(Application.dataPath + "/Savegame.save"))
            File.Delete(Application.dataPath + "/Savegame.save");

        Debug.Log("Save Cleared");
    }

    public void AddSushi(GameObject go, string type)
    {
        int instanceID = go.GetInstanceID();

        if (!saveObject.sushiObjects.ContainsKey(instanceID))
        {
            Sushi sushi = new Sushi();
            sushi.position = new SerializeVector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            sushi.rotation = new SerializeQuaternion(go.transform.rotation.x, go.transform.rotation.y, go.transform.rotation.z, go.transform.rotation.w);
            sushi.sushiType = type;
            saveObject.sushiObjects.Add(instanceID, sushi);
            sushis.Add(go);
            Debug.Log("Sushi Added to SaveObject");
        }
    }

    public void RemoveSushi(GameObject go)
    {
        int instanceID = go.GetInstanceID();
        if (saveObject.sushiObjects.ContainsKey(instanceID))
        {
            saveObject.sushiObjects.Remove(instanceID);
            sushis.Remove(go);
            Debug.Log("Sushi Removed from SaveObject");
        }
    }

    private void RehydrateHashTable()
    {
        saveObject.sushiObjects.Clear();

        foreach(GameObject go in sushis)
        {
            Sushi sushi = new Sushi();
            sushi.position = new SerializeVector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            sushi.rotation = new SerializeQuaternion(go.transform.rotation.x, go.transform.rotation.y, go.transform.rotation.z, go.transform.rotation.w);
            sushi.sushiType = go.name;
            saveObject.sushiObjects.Add(go.GetInstanceID(), sushi);
        }
    }

    public void SaveObjects()
    {
        UpdateObjectLocations();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Savegame.save");
        bf.Serialize(file, saveObject);
        file.Close();
        Debug.Log("Save Complete");
    }

    public void LoadObjects()
    {
        if (File.Exists(Application.dataPath + "/Savegame.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Savegame.save", FileMode.Open);
            saveObject = (SaveObject)bf.Deserialize(file);
            file.Close();

            sushis.Clear();

            foreach (int index in saveObject.sushiObjects.Keys)
            {
                Sushi sushi = (Sushi)saveObject.sushiObjects[index];
                Debug.Log(sushi);

                Vector3 newP = new Vector3(sushi.position.x, sushi.position.y, sushi.position.z);
                Quaternion newQ = new Quaternion(sushi.rotation.x, sushi.rotation.y, sushi.rotation.z, sushi.rotation.w);

                GameObject go = CreateSushi(newP, newQ, sushi.sushiType);
                sushis.Add(go);
            }

            //clear hash list and rebuild because new game objects have new Instance IDs
            RehydrateHashTable();
        }
    }

    private GameObject CreateSushi(Vector3 pos, Quaternion rot, string type)
    {
        GameObject newshi;
        type = type.Replace("(Clone)", "");

        switch (type)
        {
            case "Tuna":
                newshi = Instantiate(tuna, pos, rot);
                break;

            case "Yellowfish":
                newshi = Instantiate(yellowfish, pos, rot);
                break;

            case "Salmon":
                newshi = Instantiate(salmon, pos, rot);
                break;

            case "RoeBoat":
                newshi = Instantiate(roeboat, pos, rot);
                break;

            case "Shrimp":
                newshi = Instantiate(shrimp, pos, rot);
                break;

            default:
                newshi = Instantiate(tuna, pos, rot);
                break;
        }

        return newshi;
    }

    private void UpdateObjectLocations()
    {
        foreach (GameObject sushi in sushis)
        {
            int instanceID = sushi.GetInstanceID();
            Sushi temp = (Sushi)saveObject.sushiObjects[instanceID];

            temp.position.x = sushi.transform.position.x;
            temp.position.y = sushi.transform.position.y;
            temp.position.z = sushi.transform.position.z;

            temp.rotation.x = sushi.transform.rotation.x;
            temp.rotation.y = sushi.transform.rotation.y;
            temp.rotation.z = sushi.transform.rotation.z;
            temp.rotation.w = sushi.transform.rotation.w;

            saveObject.sushiObjects[instanceID] = temp;
        }
    }
}
