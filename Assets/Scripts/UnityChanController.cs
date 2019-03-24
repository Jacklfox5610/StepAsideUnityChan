using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody myrigidbody;

    float fowardFroce = 800.0f;
    //左右に移動するための力（追加）
    private float turnForce = 1000.0f;
    float upForce = 500.0f;
    //左右の移動できる範囲（追加）
    private float movableRange = 3.4f;

    //動きを減速させる係数（追加）
    private float coefficient = 0.95f;

    //ゲーム終了の判定（追加）
    private bool isEnd = false;
    //スコアを表示するテキスト（追加）
    private GameObject scoreText;
    //得点（追加）
    private int score = 0;

    GameObject endText;
    //左ボタン押下の判定（追加）
    private bool isLButtonDown = false;
    //右ボタン押下の判定（追加）
    private bool isRButtonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        //フィールドの参照には、thisをつける。
        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat("Speed", 1.5f);

        this.myrigidbody = GetComponent<Rigidbody>();
        this.endText = GameObject.Find("GameResultText");
        //シーン中のscoreTextオブジェクトを取得（追加）
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        this.myrigidbody.AddForce(this.transform.forward * fowardFroce);
        if ((Input.GetKeyDown(KeyCode.RightArrow)||this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            this.myrigidbody.AddForce(this.turnForce,0,0);
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow)||this.isLButtonDown) && this.transform.position.x > -this.movableRange)
        {
            this.myrigidbody.AddForce(-this.turnForce, 0, 0);
        }
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //spaceキーの強さによって、距離がわかるのはなんでだろう？？
        if (Input.GetKeyDown(KeyCode.Space)&&this.transform.position.y<1.0f)
        {
            this.myrigidbody.AddForce(this.transform.up*this.upForce);
            this.myAnimator.SetBool("Jump", true);
        }

        if (this.isEnd)
        {
            this.fowardFroce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;


            
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Cone" || other.gameObject.tag == "Car")
        {
            this.isEnd = true;
            this.endText.GetComponent<Text>().text = "ゲーム終了";
        }

        if (other.gameObject.tag == "Coin")
        {

            Destroy(other.gameObject);
            GetComponent<ParticleSystem>().Play();
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
        }
    }
        void OnColliderEnter(Collider other)
        {
            if (other.gameObject.tag == "Goal")
            {
                this.isEnd = true;
                this.endText.GetComponent<Text>().text = "Clear!";
            }
        }

    //ジャンプボタンを押した場合の処理（追加）
    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myrigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    //左ボタンを押し続けた場合の処理（追加）
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理（追加）
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理（追加）
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理（追加）
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
