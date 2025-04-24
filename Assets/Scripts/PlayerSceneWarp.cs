using UnityEngine;

public class PlayerSceneWarp : MonoBehaviour
{

    public GameObject sceneLoader;
    public SceneLoader sceneLoaderScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneLoaderScript = sceneLoader.GetComponent<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LevelEnd")
        {
            sceneLoaderScript.LoadScene();
        }    
    }
}
