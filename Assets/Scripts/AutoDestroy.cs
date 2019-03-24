using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{


    bool enable = false;

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {

        this.renderer=GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.renderer.isVisible & !enable)
        {
            enable = true;
        }
        if (!this.renderer.isVisible & enable)
        {
            Destroy(this.gameObject);
            Debug.Log("画面外のオブジェクト削除");
        }
    }


    /*
        void OnBecameInvisible()
        {
            Destroy(this.gameObject);
            Debug.Log("画面外のオブジェクト削除");
        }*/
}
