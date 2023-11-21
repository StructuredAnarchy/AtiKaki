using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    bool started = false;
    public GameObject fal;
    public GameObject egyes;
    public GameObject kettes;
    public GameObject MobPrefab1;
    public GameObject MobPrefab2;
    
    public float Tavolsag;
    public float SpawningTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {


            var a = Mathf.Abs(GlobalTarolo.Instance.Player.transform.position.x - transform.position.x);
            Debug.Log(a);
            if (a < Tavolsag)
            { fal.SetActive(true); started = true; StartCoroutine(Spawner()); }
        }    


    }
    private IEnumerator Spawner()
    {
        bool egy = true;
        while (true) { 
            float rnd = Random.Range(egyes.transform.position.x,kettes.transform.position.x);
            Instantiate(MobPrefab2, new Vector3(rnd, egyes.transform.position.y, egyes.transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(SpawningTime == 0 ? 4 : SpawningTime);
        }
    }

    void OnDestroy()
    {
        GlobalTarolo.Instance.Finish();
        GlobalTarolo.Instance.Gyoztel();
    }
}
