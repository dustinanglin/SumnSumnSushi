using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveandLoad : MonoBehaviour
{
    private List<GameObject> sushis;
    private List<GameObject> saucebottles;
    private VendingManager vendingManager;

    private SaveObject saveObject;
    //sushis
    private GameObject tuna, yellowfish, salmon, roeboat, shrimp;
    //sauces
    private GameObject hot, xeno, tron, trek, monster, scifi, digital, target;

    // Start is called before the first frame update
    void Awake()
    {
        saveObject = new SaveObject();

        sushis = new List<GameObject>();
        saucebottles = new List<GameObject>();

        tuna = GameObject.Find("Tuna");
        yellowfish = GameObject.Find("Yellowfish");
        salmon = GameObject.Find("Salmon");
        roeboat = GameObject.Find("RoeBoat");
        shrimp = GameObject.Find("Shrimp");

        hot = GameObject.Find("HotSauce");
        xeno = GameObject.Find("XenoSauce");
        tron = GameObject.Find("TronSauce");
        trek = GameObject.Find("TrekSauce");
        monster = GameObject.Find("MonsterSauce");
        scifi = GameObject.Find("SciFiSauce");
        digital = GameObject.Find("DigitalSauce");
        target = GameObject.Find("TargetSauce");

        vendingManager = GameObject.Find("Screen").GetComponent<VendingManager>();
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

    public void AddSushi(GameObject go, string type, string sauce)
    {
        int instanceID = go.GetInstanceID();

        if (!saveObject.sushiObjects.ContainsKey(instanceID))
        {
            Sushi sushi = new Sushi();
            sushi.position = new SerializeVector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            sushi.rotation = new SerializeQuaternion(go.transform.rotation.x, go.transform.rotation.y, go.transform.rotation.z, go.transform.rotation.w);
            sushi.sushiType = type;
            sushi.sauceType = sauce;
            saveObject.sushiObjects.Add(instanceID, sushi);
            sushis.Add(go);
            Debug.Log("Sushi Added to SaveObject");
        }
    }

    public void AddSauce(GameObject go, string sauce)
    {
        int instanceID = go.GetInstanceID();

        if (!saveObject.sauceObjects.ContainsKey(instanceID))
        {
            Saucebottle saucebottle = new Saucebottle();
            saucebottle.position = new SerializeVector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
            saucebottle.rotation = new SerializeQuaternion(go.transform.rotation.x, go.transform.rotation.y, go.transform.rotation.z, go.transform.rotation.w);
            saucebottle.sauceType = sauce;
            saveObject.sauceObjects.Add(instanceID, saucebottle);
            saucebottles.Add(go);
            Debug.Log("Sauce Added to SaveObject");
        }
    }

    public bool UnlockSauce(string sauceName)
    {
        bool isUnlocked = saveObject.sauces.Contains(sauceName);

        if (!isUnlocked)
        {
            saveObject.sauces.Add(sauceName);
            vendingManager.UnlockSauce(sauceName);
        }

        return !isUnlocked;
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

    public void RemoveSauce(GameObject go)
    {
        int instanceID = go.GetInstanceID();
        if (saveObject.sauceObjects.ContainsKey(instanceID))
        {
            saveObject.sauceObjects.Remove(instanceID);
            saucebottles.Remove(go);
            Debug.Log("Sauce Removed from SaveObject");
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
            sushi.sauceType = go.GetComponent<Saucable>().sauce_type;
            saveObject.sushiObjects.Add(go.GetInstanceID(), sushi);
        }

        saveObject.sauceObjects.Clear();

        foreach(GameObject go in saucebottles)
        {
            Saucebottle saucebottle = new Saucebottle();
            saucebottle.position = go.transform.position;
            saucebottle.rotation = go.transform.rotation;
            saucebottle.sauceType = go.GetComponent<SauceType>().sauce_type;
            saveObject.sauceObjects.Add(go.GetInstanceID(), saucebottle);
        }
    }

    public void SaveObjects()
    {
        UpdateObjects();

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
            saucebottles.Clear();

            foreach (int index in saveObject.sushiObjects.Keys)
            {
                Sushi sushi = (Sushi)saveObject.sushiObjects[index];
                Debug.Log(sushi);

                Vector3 newP = new Vector3(sushi.position.x, sushi.position.y, sushi.position.z);
                Quaternion newQ = new Quaternion(sushi.rotation.x, sushi.rotation.y, sushi.rotation.z, sushi.rotation.w);

                GameObject go = CreateSushi(newP, newQ, sushi.sushiType);
                SauceObject(go, sushi.sauceType);
                sushis.Add(go);
            }

            foreach (int index in saveObject.sauceObjects.Keys)
            {
                Saucebottle saucebottle = (Saucebottle)saveObject.sauceObjects[index];

                Vector3 newP = saucebottle.position;
                Quaternion newQ = saucebottle.rotation;

                GameObject go = CreateSauce(newP, newQ, saucebottle.sauceType);
                saucebottles.Add(go);
            }

            GameObject dishTemp = GameObject.Find("SauceDish");
            dishTemp.transform.position = saveObject.dish.position;
            dishTemp.transform.rotation = saveObject.dish.rotation;
            SauceObject(dishTemp, saveObject.dish.sauceType);

            //clear hash list and rebuild because new game objects have new Instance IDs
            RehydrateHashTable();

            //enable all unlocked sauces
            UnlockSauces();
        }
    }

    private void UnlockSauces()
    {
        foreach(string sauce in saveObject.sauces)
        {
            vendingManager.UnlockSauce(sauce);
        }
    }

    private GameObject CreateSauce(Vector3 pos, Quaternion rot, string sauceType)
    {
        GameObject newSauce;

        switch (sauceType)
        {
            case "HotSauce":
                newSauce = Instantiate(hot, pos, rot);
                break;

            case "XenoSauce":
                newSauce = Instantiate(xeno, pos, rot);
                break;

            case "TronSauce":
                newSauce = Instantiate(tron, pos, rot);
                break;

            case "TrekSauce":
                newSauce = Instantiate(trek, pos, rot);
                break;

            case "MonsterSauce":
                newSauce = Instantiate(monster, pos, rot);
                break;

            case "SciFiSauce":
                newSauce = Instantiate(scifi, pos, rot);
                break;

            case "DigitalSauce":
                newSauce = Instantiate(digital, pos, rot);
                break;

            case "TargetSauce":
                newSauce = Instantiate(target, pos, rot);
                break;

            default:
                newSauce = Instantiate(hot, pos, rot);
                break;
        }

        return newSauce;
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

    private void SauceObject(GameObject go, string sauce)
    {
        switch (sauce)
        {
            case "HotSauce":
                hot.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "XenoSauce":
                xeno.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "TronSauce":
                tron.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "TrekSauce":
                trek.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "MonsterSauce":
                monster.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "SciFiSauce":
                scifi.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "DigitalSauce":
                digital.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            case "TargetSauce":
                target.GetComponentInChildren<CoverInSauce>().SauceSushi(go);
                break;

            default:
                break;
        }
    }

    private void UpdateObjects()
    {
        //update sushi positions
        foreach (GameObject sushi in sushis)
        {
            int instanceID = sushi.GetInstanceID();
            Sushi temp = (Sushi)saveObject.sushiObjects[instanceID];

            temp.position = sushi.transform.position;
            temp.rotation = sushi.transform.rotation;
            temp.sauceType = sushi.GetComponent<Saucable>().sauce_type;

            saveObject.sushiObjects[instanceID] = temp;
        }

        //update sauce positions
        foreach (GameObject saucebottle in saucebottles)
        {
            int instanceID = saucebottle.GetInstanceID();
            Saucebottle temp = (Saucebottle)saveObject.sauceObjects[instanceID];

            temp.position = saucebottle.transform.position;
            temp.rotation = saucebottle.transform.rotation;
            temp.sauceType = saucebottle.GetComponent<SauceType>().sauce_type;

            saveObject.sauceObjects[instanceID] = temp;
        }

        //update sauce dish
        GameObject dishTemp = GameObject.Find("SauceDish");

        saveObject.dish.position = dishTemp.transform.position;
        saveObject.dish.rotation = dishTemp.transform.rotation;
        saveObject.dish.sauceType = dishTemp.GetComponentInChildren<Saucable>().sauce_type;
    }
}
