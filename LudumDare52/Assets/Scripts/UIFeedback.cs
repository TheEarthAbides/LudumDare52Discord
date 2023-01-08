using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIFeedback : MonoBehaviour
{

    public void Spawn(string _type, Vector3 _pos)
    {
        transform.position = _pos;
        if(_type == "miss")
        {
            transform.GetChild(3).gameObject.SetActive(true);
            //Debug.LogError(transform.GetChild(3));
            //Debug.LogError(transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>());
            transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().alpha = 1;
            transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>().DOFade(0, 1.0f).OnComplete(()=> { transform.GetChild(3).gameObject.SetActive(false); });
        }
        if (_type == "bad")
        {
            transform.GetChild(2).gameObject.SetActive(true);
            //Debug.LogError(transform.GetChild(3));
            //Debug.LogError(transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>());
            transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().alpha = 1;
            transform.GetChild(2).GetChild(0).GetComponent<TMPro.TMP_Text>().DOFade(0, 1.0f).OnComplete(() => { transform.GetChild(2).gameObject.SetActive(false); });
        }
        if (_type == "good")
        {
            transform.GetChild(1).gameObject.SetActive(true);
            //Debug.LogError(transform.GetChild(3));
            //Debug.LogError(transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>());
            transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().alpha = 1;
            transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().DOFade(0, 1.0f).OnComplete(() => { transform.GetChild(1).gameObject.SetActive(false); });
        }
        if (_type == "perfect")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            //Debug.LogError(transform.GetChild(3));
            //Debug.LogError(transform.GetChild(3).GetChild(0).GetComponent<TMPro.TMP_Text>());
            transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().alpha = 1;
            transform.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_Text>().DOFade(0, 1.0f).OnComplete(() => { transform.GetChild(0).gameObject.SetActive(false); });
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
