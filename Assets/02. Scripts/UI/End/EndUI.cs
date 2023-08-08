using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{
    public Image img;

    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.enabled = false;
        StartCoroutine(UIManager.instance.EndFadeIn(img));
        StartCoroutine(ButtonOn());
    }

    private IEnumerator ButtonOn()
    {
        yield return new WaitForSeconds(1.5f);

        btn.enabled = true;
    }
    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}