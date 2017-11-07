using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerEngine;
    [SerializeField]
    Health PlayerEngineHealth;
    public int Round;
    //demo
    float timer = 60;
    //float timer = 45;
    public GameObject OverlordPrefab;
    public GameObject OverlordTargetShip;
    //public int chanceToSpawn = 35;
    int chanceToSpawn = 100;
    int chanceToSpawnRoll;
    public List<OverlordAI> SpawnedOverlords = new List<OverlordAI>();
    public List<Transform> OverlordSpawns = new List<Transform>();

    //temp
    [SerializeField]
    Player_Inventory playerInvenP1;

    [SerializeField]
    Player_Inventory playerInvenP2;

    //[SerializeField]
    int OverlordsToSpawnCount = 1;
    int OverlordsSpawnedCount;
    [SerializeField]
    int OverlordsDead;

    [SerializeField]
    float ChanceToSpawnCrates;
    [SerializeField]
    Transform[] crateSpawns;
    [SerializeField]
    GameObject CratePrefab;

    public void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        Round++;
        OverlordsSpawnedCount = 0;
        OverlordsDead = 0;
        SpawnedOverlords.Clear();



        if (Round == 1)
        {
            //timer = 60;
            //chanceToSpawn = 35;
            //OverlordsToSpawnCount = 1;
            SpawnCrates();
        }

        if (Round == 2)
        {
            timer = 55;
            chanceToSpawn = 45;
            OverlordsToSpawnCount = 2;
        }

        if (Round == 3)
        {
            timer = 50;
            chanceToSpawn = 50;
            OverlordsToSpawnCount = 2;
        }

        if (Round == 4)
        {
            timer = 45;
            chanceToSpawn = 55;
            OverlordsToSpawnCount = 3;
        }

        if (Round == 5)
        {
            timer = 40;
            chanceToSpawn = 100;
            OverlordsToSpawnCount = 3;
        }

        //temp for demo
        if (Round == 6)
        {
            Application.Quit();
        }

        StartCoroutine(SpawnOverlords());
    }

    public IEnumerator SpawnOverlords()
    {
        foreach (Transform x in OverlordSpawns)
        {
            //demo
            //chanceToSpawn = 35; //+ (generation * 2);
            //chanceToSpawn = 150;
            chanceToSpawnRoll = Random.Range(0, 100);

            if (chanceToSpawnRoll < chanceToSpawn && OverlordsSpawnedCount < OverlordsToSpawnCount)
            {
                GameObject tempOLGO =
                    Instantiate(OverlordPrefab, x.transform.position, x.transform.rotation);
                OverlordAI tempOL = tempOLGO.GetComponent<OverlordAI>();
                tempOL.TargetShip = OverlordTargetShip;
                tempOL.PlayerEngine = PlayerEngine;
                SpawnedOverlords.Add(tempOL);
                OverlordsSpawnedCount++;
                //generation++;
            }
        }

        yield return new WaitForSeconds(timer);

        if (OverlordsSpawnedCount < OverlordsToSpawnCount)
        {
            StartCoroutine(SpawnOverlords());
        }

        if (OverlordsSpawnedCount >= OverlordsToSpawnCount)
        {
            StartCoroutine(WaitingForEndOfRound());
        }
    }

    IEnumerator WaitingForEndOfRound()
    {
        OverlordsDead = 0;

        foreach (OverlordAI x in SpawnedOverlords)
        {
            if (x.IsDead == true)
            {
                OverlordsDead++;
            }
        }

        // yield return new WaitForSeconds(5);
        yield return new WaitForSeconds(2);

        if (OverlordsDead == OverlordsToSpawnCount)
        {
            //Debug.Log("Round Over");
            if (playerInvenP1 != null)
            {
                playerInvenP1.wincan.SetActive(true);
                playerInvenP1.Respawn();
            }

            if (playerInvenP2 != null)
            {
                playerInvenP2.wincan.SetActive(true);
                playerInvenP2.Respawn();
            }

            foreach (OverlordAI x in SpawnedOverlords)
            {
                Destroy(x.gameObject);
            }

            HordeBoat[] tempArr = FindObjectsOfType<HordeBoat>();

            foreach (HordeBoat x in tempArr)
            {
                Destroy(x.gameObject);
            }

            yield return new WaitForSeconds(2.5f);

            if (playerInvenP1 != null)
            {
                playerInvenP1.wincan.SetActive(false);
                playerInvenP1.losecan.SetActive(false);

            }

            if (playerInvenP2 != null)
            {
                playerInvenP2.wincan.SetActive(false);
                playerInvenP2.losecan.SetActive(false);
            }

            yield return new WaitForSeconds(60f);

            StartRound();
        }

        else
        {
            StartCoroutine(WaitingForEndOfRound());
        }
    }

    //void FixedUpdate()
    //{
    //    if (PlayerEngineHealth.isDestroyed)
    //    {
    //        if (playerInvenP1 != null)
    //        {
    //            playerInvenP1.losecan.SetActive(true);
    //            //playerInvenP1.Respawn();
    //        }

    //        if (playerInvenP2 != null)
    //        {
    //            playerInvenP2.losecan.SetActive(true);
    //            //playerInvenP2.Respawn();
    //        }
    //    }
    //}

    void SpawnCrates()
    {
        foreach (Transform x in crateSpawns)
        {
            float CrateSpawnRoll = Random.Range(0, 100);
            
            if (ChanceToSpawnCrates > CrateSpawnRoll)
            {
                GameObject tempCrate = Instantiate(CratePrefab, x.position, x.rotation);
                tempCrate.transform.Rotate( new Vector3 (0, Random.Range(0, 360), 0));
            }
        }
    }
}
