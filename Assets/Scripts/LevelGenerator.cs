using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : Singleton<LevelGenerator>
{
    [SerializeField] GameObject[] cubes;
    [SerializeField] Transform parentSpawn;
    [SerializeField] Transform startSpawn;
    [SerializeField] List<Platform> platformsCurrent;
    [SerializeField] Platform platformPrefab;
    [SerializeField] int maxPlatforms;

    [SerializeField] Color[] colorsPool;

    public Sling currentSling { get; set; }
    int counter;
    public GameObject GetCube()
    {
        int rand = Random.Range(0, cubes.Length);
        return cubes[rand];
    }

    public void NewLevel()
    {
        Parallax.Instance.Restart();
        Distance.Instance.ClearDistance();

        if (parentSpawn.childCount > 0)
        {
            for (int i = 0; i < parentSpawn.childCount; i++)
            {
                Destroy(parentSpawn.GetChild(i).gameObject);
                platformsCurrent.Clear();
            }
        }

        Platform np = Instantiate(platformPrefab.gameObject, parentSpawn).GetComponent<Platform>();
        np.transform.position = startSpawn.position;
        np.Creation();
        np.name = "plat_" + counter;
        counter++;
        platformsCurrent.Add(np);
        platformsCurrent[0].FirstPlatform();
        if (platformsCurrent.Count < maxPlatforms)
            AddPlatform();
    }

    public Color ChangeColor()
    {
        int rand = Random.Range(0, colorsPool.Length);
        return colorsPool[rand];
    }

    public void AddPlatform()
    {
        Transform lastPos = platformsCurrent[platformsCurrent.Count - 1].transform;

        if (platformsCurrent.Count > maxPlatforms)
        {
            Destroy(platformsCurrent[0].gameObject);
            platformsCurrent.RemoveAt(0);
        }

        Platform np = Instantiate(platformPrefab.gameObject, parentSpawn).GetComponent<Platform>();
        
        np.transform.position = lastPos.position + new Vector3 (0,0, 14.73558f);
        np.Creation();
        np.name = "plat_" + counter;
        counter++;
        platformsCurrent.Add(np);

        if (platformsCurrent.Count < maxPlatforms)
            AddPlatform();

    }

    public void ResumeAdds()
    {
        GameObject c = GameManager.Instance.GetObject();
        c.SetActive(true);
        c.GetComponent<Cube>().SetSling();
    }

}
