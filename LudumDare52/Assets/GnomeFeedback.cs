using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GnomeFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPositive()
    {
        int child = Random.Range(0, 3);
        transform.GetChild(child).gameObject.SetActive(true);
        transform.GetChild(child).GetChild(0).GetComponent<TMPro.TMP_Text>().alpha = 1;
        transform.GetChild(child).GetChild(0).GetComponent<TMPro.TMP_Text>().DOFade(0, 1.0f).OnComplete(() => { transform.GetChild(child).gameObject.SetActive(false); }).SetDelay(0.5f);
    }

    public void ShowNegative()
    {
        int child = Random.Range(3, 5);
        transform.GetChild(child).gameObject.SetActive(true);
        transform.GetChild(child).GetChild(0).GetComponent<TMPro.TMP_Text>().alpha = 1;
        transform.GetChild(child).GetChild(0).GetComponent<TMPro.TMP_Text>().DOFade(0, 1.0f).OnComplete(() => { transform.GetChild(child).gameObject.SetActive(false); }).SetDelay(0.5f);
    }
}
