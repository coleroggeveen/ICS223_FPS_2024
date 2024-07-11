using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private SettingsPopup settingsPopup;

    private int popupsActive = 0;

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameActive(true); // game unpaused at start

        UpdateHealth(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && popupsActive == 0)
        {
            optionsPopup.Open();
        }
    }

    // update score display
    public void UpdateScore(int newScore)
    {
        scoreValue.text = newScore.ToString();
    }

    public void SetGameActive(bool active)
    {
        if(active){
            Time.timeScale = 1; // unpause the game
            Cursor.lockState = CursorLockMode.Locked; // lock cursor at center
            Cursor.visible = false; // hide cursor
            crossHair.gameObject.SetActive(true); // show the crosshair
            Messenger.Broadcast("GAME_ACTIVE");
        }
        else
        {
            Time.timeScale = 0; // pause the game
            Cursor.lockState = CursorLockMode.None; // let cursor move freely
            Cursor.visible = true; // show the cursor
            crossHair.gameObject.SetActive(false); // turn off the crosshair
            Messenger.Broadcast("GAME_INACTIVE");
        }
    }

    private void OnHealthChanged(float healthRemaining)
    {
        UpdateHealth(healthRemaining);
    }

    private void UpdateHealth(float healthPercentage)
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);
        if (healthPercentage == 0.6f)
        {
            healthColor = Color.yellow;
        }
        healthBar.color = healthColor;
        healthBar.fillAmount = healthPercentage;
    }

    private void OnPopupOpened()
    {
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }

        popupsActive += 1;
    }

    private void OnPopupClosed()
    {
        popupsActive -= 1;

        if ( popupsActive == 0 )
        {
            SetGameActive(true);
        }
    }
}
