using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Retrieve : MonoBehaviour
{
    [SerializeField] public string address;
    public jsonDataClass jsnData;
    public GameObject loadingScreen;
    public GameObject thumbsCanvas;

    private Transform bossParent;

    void Start()
    {
        loadingScreen.SetActive(true);
        bossParent = GameObject.Find("BOSS").transform;
        StartCoroutine(GetFiles());
    }

    IEnumerator GetFiles()
    {
        UnityWebRequest _www = UnityWebRequest.Get(address);
        yield return _www.SendWebRequest();

        if (_www.isNetworkError || _www.isHttpError) {
            Debug.Log(_www.error);
        }

        else {
            //Debug.Log(_www.downloadHandler.text);
            ProcessJson(_www.downloadHandler.data);
        }
    }

    private void ProcessJson(byte[] where)
    {
        string wholeData = System.Text.Encoding.UTF8.GetString(where, 3, where.Length - 3);
        jsnData = JsonUtility.FromJson<jsonDataClass>(wholeData);
        
        foreach (ModelEntity item in jsnData.models)
        {
            PlaceModel(item);
        }
        loadingScreen.SetActive(false);
    }

    private void PlaceModel(ModelEntity obj)
    {
        GameObject instance = Instantiate(Resources.Load(obj.name, typeof(GameObject))) as GameObject;
        instance.name = obj.name;

        Vector3 spawnPos = new Vector3(obj.position[0], obj.position[1], obj.position[2]);
        instance.transform.position = spawnPos;

        Quaternion spawnRot = Quaternion.Euler(obj.rotation[0], obj.rotation[1], obj.rotation[2]);
        instance.transform.rotation = spawnRot;

        Vector3 spawnSca = new Vector3(obj.scale[0], obj.scale[1], obj.scale[2]);
        instance.transform.localScale = spawnSca;

        instance.transform.SetParent(bossParent);

        thumbsCanvas.GetComponent<ListManager>().SetupItem(instance.transform);
    }

    public void SaveJson()
    {
        jsonDataClass save = new jsonDataClass();
        int tempAmount = bossParent.childCount;
        save.models = new List<ModelEntity>(tempAmount);

        for (int i = 0; i < tempAmount; i++)
        {
            Transform original = bossParent.GetChild(i);
            save.models.Add(new ModelEntity());
            save.models[i].name = original.gameObject.name;

            save.models[i].position = new List<float>();
            save.models[i].position.Add(original.position.x);
            save.models[i].position.Add(original.position.y);
            save.models[i].position.Add(original.position.z);

            save.models[i].rotation = new List<float>();
            save.models[i].rotation.Add(original.rotation.eulerAngles.x);
            save.models[i].rotation.Add(original.rotation.eulerAngles.y);
            save.models[i].rotation.Add(original.rotation.eulerAngles.z);

            save.models[i].scale = new List<float>();
            save.models[i].scale.Add(original.localScale.x);
            save.models[i].scale.Add(original.localScale.y);
            save.models[i].scale.Add(original.localScale.z);
        }
        string json = JsonUtility.ToJson(save, true);
        Debug.Log("JSON result: " + json);
        File.WriteAllText(Application.dataPath + "/SaveIvanMotta.json", json, System.Text.Encoding.UTF8);
    }
}
