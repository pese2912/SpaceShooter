using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour {

    public static GameMgr instance = null;

    public Text scoreText;
    public Image hpBar;


    public List<GameObject> monsterPool = new List<GameObject>();
    [Range(5,20)]
    public int maxPool = 10;


    public Transform Player;
    public GameObject monster;
    public Transform[] points;
    public float createTime = 3.0f;

    public  bool isGameOver = false;

    private int highScore = 0;



    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        highScore = PlayerPrefs.GetInt("SCORE",0); // 로컬 데이터 불러오기.
        DisScore(0);

        for (int i = 0; i < maxPool; i++)
        {
            GameObject _monster = Instantiate(monster) as GameObject;
            _monster.name = string.Format("Monster_{0}", i.ToString("00"));
            _monster.SetActive(false);

            monsterPool.Add(_monster);
        }
	
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        //InvokeRepeating("CreateMonster", 1.0f, createTime); // 시간뒤에 메소드 반복실행.
        //
        StartCoroutine(CreateMonster());
       
	}
	

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            int idx = Random.Range(1, points.Length);


            foreach (GameObject _monster in monsterPool)
            {
                if (!_monster.activeSelf) // 비활성화
                {
                    _monster.transform.position = points[idx].position;
                    _monster.transform.LookAt(Player.position);
                    _monster.SetActive(true);
                    break;
                }
            }


            //GameObject _monster = Instantiate(monster) as GameObject;
           // _monster.transform.position = points[idx].position;
            //_monster.transform.rotation = Quaternion.LookRotation(points[0].position - _monster.transform.position); //몬스터가 중앙을 보는 방향 벡터로 회전.

           // _monster.transform.LookAt(Player.position);
        
            yield return new WaitForSeconds(createTime);
        }
    }
	

    public void DisScore(int score){
        
        highScore += score;

        PlayerPrefs.SetInt("SCORE", highScore); // 로컬 데이터 저장.
        scoreText.text = "Score : " + highScore.ToString("0000");
    }
}
