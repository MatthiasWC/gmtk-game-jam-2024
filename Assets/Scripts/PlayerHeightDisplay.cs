using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHeightDisplay : MonoBehaviour
{

    private GameObject PlayerObject;
    [SerializeField] private TMP_Text HeightText;

    private double init_height;
    
    private double height = 0;
    private double max_height = 0;


    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = PlayerSingleton.instance.gameObject;
        //init_height = PlayerObject.transform.position.y;


        /*ground_box = ground.GetComponent<BoxCollider2D>();

        ground_height = ground.transform.position.y +
            (float)(ground_box.offset.y + (ground_box.size.y / 2.0)) + (float)0.5;
        Debug.Log("ground height: " + ground_height.ToString());*/
        
        HeightText.text = max_height.ToString();
        init_height = PlayerObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        height = PlayerObject.transform.position.y - init_height;
        //Debug.Log("height: " + height.ToString());

        if (height > max_height)
        {
            max_height = height;
            HeightText.text = max_height.ToString("F1");
        }
    }
}
