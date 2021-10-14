using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PSoundManager : MonoBehaviour
{

    private GameObject Monster;
    private GameObject[] List;
    public AudioSource MusicSource;
    public AudioSource SoundSource;
    public int SprintInt;

    public AudioClip Step1;
    public AudioClip Step2;
    public AudioClip Step3;
    public AudioClip Running;
    public AudioClip HorrorMusic;
    public AudioClip ChaseMusic;

    private bool Sprint;
    private bool Walk;
    private bool Chase;
    private bool Playing;

    private int MusicVolumeKey;
    private int SoundVolumeKey;

    private float MonsterPos;
    private float PlayerPos;
    public float Distance;

    private int RandNum;

    private float MusicVolume;
    private float SoundVolume;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(55555);

        List = GameObject.FindGameObjectsWithTag("Monster");

        for (int i = 0; i < List.Length; i++)
        {
            Monster = List[i];

            if (Monster.name == "O_Monster")
            {
                break;
            }
        }
        
        MusicVolumeKey = PlayerPrefs.GetInt("MusicVolume");
        SoundVolumeKey = PlayerPrefs.GetInt("SoundVolume");

        MusicVolume = MusicVolumeKey / 100.0f;
        SoundVolume = SoundVolumeKey / 100.0f;

        MusicSource.volume = MusicVolume;
        SoundSource.volume = SoundVolume;

        Chase = Playing = Walk = Sprint = false;

        SprintInt = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (SprintInt == 0 && SoundSource.clip == Running)
        {
            SoundSource.loop = false;
            SoundSource.Stop();
            SoundSource.clip = null;
            Sprint = false;
        }

        if ((Input.GetKey(KeyCode.LeftShift)) && (Sprint == false) && (SprintInt != 0))
        {
            SoundSource.loop = true;
            SoundSource.clip = Running;
            SoundSource.Play();
            Sprint = true;
        }

        else if (((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))) && (Sprint == false) && (Walk == false))
        {
            SoundSource.loop = false;
            Walk = true;

            StartCoroutine(Walking());
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            SoundSource.loop = false;
            Sprint = false;
        }

        if(Monster != null)
        {
            Distance = Vector3.Distance(Monster.transform.position, transform.position);
        }

        else
        {
            Distance = 100;
        }

        if ((Distance < 15) && (Chase == false))
        {
            MusicSource.clip = ChaseMusic;
            MusicSource.Play();
            Chase = true;
        }

        else if ((Chase == false) && (Playing == false))
        {
            MusicSource.clip = HorrorMusic;
            MusicSource.Play();
            Playing = true;
        }

        else if ((Chase == true) && (Distance > 15))
        {
            Playing = false;
            Chase = false;
            MusicSource.Stop();
            MusicSource.clip = null;
        }

        if ((MusicSource.volume != (MusicVolumeKey/100)) || (SoundSource.volume != (SoundVolumeKey/100)) || (MusicSource.volume == 0) || (SoundSource.volume == 0))
        {
            MusicVolumeKey = PlayerPrefs.GetInt("MusicVolume");
            SoundVolumeKey = PlayerPrefs.GetInt("SoundVolume");

            MusicVolume = MusicVolumeKey / 100.0f;
            SoundVolume = SoundVolumeKey / 100.0f;

            MusicSource.volume = MusicVolume;
            SoundSource.volume = SoundVolume;

            if(!Input.anyKey)
            {
                Walk = false;
                Sprint = false;
            }
        }
    }

    void LateUpdate()
    {
        SprintInt = gameObject.GetComponent<S_Player>().SprintLeft;
    }

    void OnEnable()
    {
        MusicVolumeKey = PlayerPrefs.GetInt("MusicVolume");
        SoundVolumeKey = PlayerPrefs.GetInt("SoundVolume");
    }

    IEnumerator Walking()
    {
        RandNum = Random.Range(1, 4);

        if (RandNum == 1)
        {
            SoundSource.clip = Step1;
        }

        else if (RandNum == 2)
        {
            SoundSource.clip = Step2;
        }

        else if (RandNum == 3)
        {
            SoundSource.clip = Step3;
        }

        SoundSource.Play();

       yield return new WaitForSecondsRealtime(0.6f);

        Walk = false;
    }
}
