using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private int maximum;

    [SerializeField]
    private int minimum;

    [SerializeField]
    private int currAmount;

    [SerializeField]
    private Image mask;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        //  int currMin = currAmount - minimum;
        //  int currMax = maximum - currAmount;
        float currProcentile;

        //currProcentile = (float)currAmount / (float)maximum;
        currProcentile = (float)(currAmount - minimum) / (float)(maximum - minimum);
        mask.fillAmount = currProcentile;
    }
}
