using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHeightDisplay : MonoBehaviour
{

    [SerializeField] private GameObject PlayerObject;
    [SerializeField] private TMP_Text HeightText;
    [SerializeField] private GameObject ground;

    private double init_height;
    
    private double height = 0;
    private double max_height;

    private double ground_height;
    private BoxCollider2D ground_box;
    private bool hit_ground;


    // Start is called before the first frame update
    void Start()
    {
        //init_height = PlayerObject.transform.position.y;


        /*ground_box = ground.GetComponent<BoxCollider2D>();

        ground_height = ground.transform.position.y +
            (float)(ground_box.offset.y + (ground_box.size.y / 2.0)) + (float)0.5;
        Debug.Log("ground height: " + ground_height.ToString());*/
        ground_height = -4.03;

        hit_ground = false;
        
        HeightText.text = max_height.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        height = PlayerObject.transform.position.y;
        //Debug.Log("height: " + height.ToString());


        if (!hit_ground && (height <= ground_height))
        {
            hit_ground = true;
            //Debug.Log("GROUND CONTROL TO MAJOR TOM");
            max_height = height;
            init_height = height;

        }

        if (hit_ground && (height > max_height))
        {
            max_height = height;
            HeightText.text = (max_height+System.Math.Abs(init_height)).ToString("F1");
        }

        

    }
}
