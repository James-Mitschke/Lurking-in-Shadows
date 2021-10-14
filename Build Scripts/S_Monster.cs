using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class S_Monster : MonoBehaviour
{
    private GameObject[] List;
    private int ListLength;
    private GameObject Player;
    private Vector3 Target;
    private float TempDistance;
    private NavMeshHit NewTarget;
    private Vector3 TempTarget;
    private GameObject C_Monster;
    private AudioSource PlayerNoise;
    int Mask;
    private RaycastHit other;
    public AudioClip Sprinting;
    private bool Initialised;
    private NavMeshAgent Monster;

    // Start is called before the first frame update
    void Start()
    {
        Initialised = false;
        StartCoroutine(init());
    }

    // Update is called once per frame
    void Update()
    {
        if (Initialised == true)
        {
            Physics.Raycast(C_Monster.transform.position, C_Monster.transform.forward, out other, 100.0f, Mask);
        
            if(other.transform.tag == "Player")
            {
                Target = Player.transform.position;
                Monster.SetDestination(Target);

                if (Monster.hasPath == false)
                {
                    TempDistance = Vector3.Distance(transform.position, Player.transform.position);

                    if (TempDistance > 0 && TempDistance < 10)
                    {
                        TempTarget = transform.forward * (TempDistance - 1);
                        NavMesh.SamplePosition(TempTarget, out NewTarget, 5.0f, NavMesh.AllAreas);
                        Monster.SetDestination(NewTarget.position);
                    }
                }
            }

            else if (PlayerNoise.isPlaying == true)
            {
                if (PlayerNoise.clip == Sprinting)
                {
                    if(Vector3.Distance(Player.transform.position, transform.position) < 11f)
                    {
                        Target = Player.transform.position;
                        Monster.SetDestination(Target);
                        
                        SoundCheck();
                    }
                }

                else if (PlayerNoise.clip != null)
                {
                    if(Vector3.Distance(Player.transform.position, transform.position) < 6.5f)
                    {
                        Target = Player.transform.position;
                        Monster.SetDestination(Target);

                        SoundCheck();
                    }
                }
            }

            else if (transform.position == Target || Monster.hasPath == false)
            {
                Target = List[Random.Range(0, List.Length)].transform.position;
                Monster.SetDestination(Target);
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, C_Monster.transform.rotation, 1.0f);
        }
    }

    void Init()
    {
        Monster = GetComponent<NavMeshAgent>();

        Mask = (1 << 8) | (1 << 9) | (1 << 11) | (1 << 25);

        Random.InitState(System.DateTime.Now.Millisecond);

        List = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < List.Length; i++)
        {
            Player = List[i];

            if (Player.name == "O_Player")
            {
                break;
            }
        }

        PlayerNoise = GameObject.FindGameObjectWithTag("Sound").GetComponent<AudioSource>();

        List = GameObject.FindGameObjectsWithTag("Monster");

        for (int i = 0; i< List.Length; i++)
        {
            C_Monster = List[i];

            if (C_Monster.name == "C_Monster")
            {
                break;
            }
        }    


        List = GameObject.FindGameObjectsWithTag("MonsterSearch");
        Initialised = true;
    }

    void SoundCheck()
    {
        if (Monster.hasPath == false)
        {
            TempDistance = 1000;

            for (int i = 0; i < List.Length; i++)
            {
                if (Vector3.Distance(Player.transform.position, List[i].transform.position) < TempDistance)
                {
                    TempTarget = List[i].transform.position;
                }
            }

            if (Vector3.Distance(transform.position, TempTarget) < 5f)
            {
                TempDistance = Vector3.Distance(transform.position, Player.transform.position);

                if (TempDistance > 0 && TempDistance < 10)
                {
                    TempTarget = transform.forward * (TempDistance - 1);
                    NavMesh.SamplePosition(TempTarget, out NewTarget, 5.0f, NavMesh.AllAreas);
                    Monster.SetDestination(NewTarget.position);
                }
            }

            else
            {
                Monster.SetDestination(TempTarget);
            }
        }
    }

    IEnumerator init()
    {
        yield return new WaitForSeconds(1.0f);

        Init();
    }
}
