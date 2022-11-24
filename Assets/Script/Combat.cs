using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Combat : MonoBehaviour
{
    // Start is called before the first frame update
    public Material graphmat;
    public float curr_stamina;
    public float max_stamina;
    public TextMeshProUGUI txt;
    void Start()
    {
        graphmat.SetFloat("_segmentCount", max_stamina);
        graphmat.SetFloat("_RemovedSegment", curr_stamina);
    }

    // Update is called once per frame
    public void onPressAttack()
    {
        graphmat.SetFloat("_RemovedSegment", ++curr_stamina);
        float no = max_stamina - curr_stamina;
        txt.text = no.ToString();
    }
    public void onPressDefend()
    {
        graphmat.SetFloat("_RemovedSegment", ++curr_stamina);
        float no = max_stamina - curr_stamina;
        txt.text = no.ToString();
    }
    public void onPressSpecial_1()
    {
        
    }
    public void onPressSpecial_2()
    {
        
    }
}
