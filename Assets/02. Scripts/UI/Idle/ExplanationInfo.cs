using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationInfo : MonoBehaviour
{
    public GameObject curMapGold;
    public GameObject explan;

    public Text description;
    public Text skillRank;

    public void Open()
    {
        explan.SetActive(true);
        description.text = null;
        skillRank.text = null;
    }
    public void ExplanationShow(string _description, string _rank = null)
    {
        explan.SetActive(true);
        description.text = _description;
        skillRank.text = _rank;
    }
}