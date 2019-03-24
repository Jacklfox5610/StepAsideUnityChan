using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject coinPrefab;
    public GameObject cornPrefab;
    // public GameObject[] Prefabs;
    //スタート地点
    private int startPos = -160;
    //ゴール地点
    private int goalPos = 130;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //   UnityChanController unitychan;
    public GameObject unitychan;
    GameObject[] prefs;
    Vector3 prePos;
    //プレハブを生成する範囲
    int i;

    // Start is called before the first frame update
    void Start()
    {

        i = startPos;


    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Round((unitychan.transform.position.z + 40) % 40) == 0);
        if (Mathf.Round((unitychan.transform.position.z+40)%40)==0)
        {
            CreatePrefab(i);
        }

    }
    public void CreatePrefab(int z)
    {


        if(z<goalPos)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject cone = Instantiate(cornPrefab) as GameObject;
                    cone.transform.position = new Vector3(4 * j, cone.transform.position.y, z);
                }


            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab) as GameObject;
                        coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, z + offsetZ);
                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab) as GameObject;
                        car.transform.position = new Vector3(posRange * j, car.transform.position.y, z + offsetZ);
                    }
                }
            }


        }
        i+=40;
        
    }

/*    int PrePos(GameObject depos)
    {
        prePos.z = depos.transform.position.z;
    }*/
}
