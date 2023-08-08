using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    Canvas canvas;

    public GameObject idleUI;
    public GameObject fightUI;
    public GameObject gameExitUI;

    public Preferences preferences;
    public Information info;
    public TurnInfo turnUI;
    public MonsterInfo monsterInfo;

    public Shop shop;

    public Image fade;
    
    private void Awake()
    {
        // 싱글톤
        // 자신이 없다면 자신을 생성
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        // 자신이 있다면 유지하고 다른걸 삭제
        else
        {
            Destroy(this.gameObject);
        }
        fade.gameObject.SetActive(false);
    }
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        IdleUI();
    }
    public void InformationOnOff()
    {
        info.gameObject.SetActive(!info.gameObject.activeSelf);
        if (info.gameObject.activeSelf) info.Open();
    }
    public void PreferencesOnOff()
    {
        preferences.gameObject.SetActive(!preferences.gameObject.activeSelf);
    }
    public void IdleUI()
    {
        idleUI.SetActive(true);
        info.gameObject.SetActive(false);
        shop.gameObject.SetActive(false);
        fightUI.SetActive(false);
        turnUI.gameObject.SetActive(false);
    }
    public void ShopUI()
    {
        idleUI.SetActive(false);
        info.gameObject.SetActive(false);
        shop.gameObject.SetActive(true);
        fightUI.SetActive(false);
        turnUI.gameObject.SetActive(false);
    }
    public void FightUI()
    {
        idleUI.SetActive(false);
        fightUI.SetActive(true);
    }
    public void TurnStart()
    {
        turnUI.gameObject.SetActive(true);
        turnUI.Open();
    }
    public void TurnEnd()
    {
        turnUI.gameObject.SetActive(false);
    }
    // 페이드 실행 함수
    public void Fade()
    {
        StartCoroutine(FadeOn());
    }
    public IEnumerator FadeOn()
    {
        fade.gameObject.SetActive(true);
        float fadeCount = 0f;

        while (fadeCount < 1.0f)
        {
            fadeCount += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            fade.color = new Color(0, 0, 0, fadeCount);
        }
        if (PlayerManager.instance.isFight)
        {
            CameraManager.instance.isFight = true;
            PlayerManager.instance.FightReadyMove();
            UIManager.instance.info.statusInfo.Status();
            int rand = Random.Range(0, 2);
            if(rand == 0) SoundManager.instance.PlayBGM("배틀1");
            else SoundManager.instance.PlayBGM("배틀2");
        }
        else
        {
            CameraManager.instance.isFight = false;
            // 원래 위치로 돌아가기
            PlayerManager.instance.IdleReadyMove();
            SoundManager.instance.PlayBGM("필드");
        }
        yield return new WaitForSeconds(0.3f);
        while (fadeCount > 0)
        {
            fadeCount -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            fade.color = new Color(0, 0, 0, fadeCount);
        }

        fade.gameObject.SetActive(false);
        if (PlayerManager.instance.isFight)
        {
            yield return new WaitForSeconds(0.5f);
            TurnStart();
        }
    }
    public IEnumerator EndFadeOut()
    {
        fade.gameObject.SetActive(true);
        float fadeCount = 0f;

        while (fadeCount < 1.0f)
        {
            fadeCount += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            fade.color = new Color(0, 0, 0, fadeCount);
        }

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("End");
    }

    public IEnumerator EndFadeIn(Image _img)
    {
        float fadeCount = 0f;

        while (fadeCount < 0)
        {
            fadeCount += Time.deltaTime;
            yield return new WaitForSeconds(0.001f);
            _img.color = new Color(255, 255, 255, fadeCount);
        }

        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator DamageUI(int _damage, bool _isCri, Vector2 _position)
    {
        // 데미지 폰트 생성
        var damageFont = ObjectPoolManager.instance.GetQueueDamageText();
        // 데미지 폰트 위치 선정
        damageFont.gameObject.transform.position = _position;
        if (_damage <= 0)
        {
            damageFont.color = new Color(0, 0, 255);
            damageFont.text = "MISS";
        }
        else 
        {
            if(!_isCri) damageFont.color = new Color(255, 255, 255);
            else damageFont.color = new Color(255, 0, 0);

            damageFont.text = _damage.ToString();
        }
        float time = 0;
        while(time < 0.5f)
        {
            time += 0.1f;
            damageFont.gameObject.transform.Translate(new Vector2(0, 0.6f));
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        ObjectPoolManager.instance.InsertQueueDamageText(damageFont);
    }
    public IEnumerator HealUI(int _Heal, Vector2 _position)
    {
        // 데미지 폰트 생성
        var damageFont = ObjectPoolManager.instance.GetQueueDamageText();
        // 데미지 폰트 위치 선정
        damageFont.gameObject.transform.position = _position;
        damageFont.color = new Color(0, 128, 0);
        damageFont.text = _Heal.ToString();

        float time = 0;
        while (time < 0.5f)
        {
            time += 0.1f;
            damageFont.gameObject.transform.Translate(new Vector2(0, 0.6f));
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        ObjectPoolManager.instance.InsertQueueDamageText(damageFont);
    }

    public void GameExitOn()
    {
        gameExitUI.SetActive(!gameExitUI.activeSelf);
    }

    public void GameExit()
    {
        Application.Quit();
    }
}