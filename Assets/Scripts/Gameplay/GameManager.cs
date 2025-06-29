using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    OUT,
    INGAME,
    PAUSED,
    GAMEOVER
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EvolutionsManager evolutionsManager;
    [SerializeField]
    private TerritoryManager territoryManager;
    [SerializeField]
    private EraManager eraManager;
    [SerializeField]
    private EventsManager eventsManager;
    [SerializeField]
    private GamePlayPanel gamePlayPanel;
    private EffectsManager effectsManager;
    private HostilityManager hostilityManager;
    [SerializeField]
    private List<DifficultySetting> difficulties;

    private static GameManager instance;

    private GameState state = GameState.OUT;
    private float period;
    private Tribe tribe;
    private TimeSpan sessionTime;
    private bool isPaused = false;
    private bool isPlaying = false;
    private DifficultySetting currentDifficulty;

    public static GameManager Instance
    {
        get
        {
            CheckInstance();
            return instance;
        }
    }

    public EvolutionsManager EvolutionsManager => evolutionsManager;
    public TerritoryManager TerritoryManager => territoryManager;
    public EraManager EraManager => eraManager;
    public EventsManager EventsManager => eventsManager;
    public HostilityManager HostilityManager => hostilityManager;
    public EffectsManager EffectsManager => effectsManager;
    public Tribe Tribe => tribe;
    public int GetSessionSeconds => (int)sessionTime.TotalSeconds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance.Equals(this))
        {
            Destroy(gameObject);
        }
    }

    private static void CheckInstance()
    {
        if (instance == null)
        {
            var obj = FindObjectOfType<GameManager>();
            if (obj != null)
            {
                var gm = obj.GetComponent<GameManager>();
                if (gm != null)
                    instance = gm;
            }
        }
    }

    private void Start()
    {
        currentDifficulty = difficulties[PlayerPrefs.GetInt("Difficulty", 0)];
        territoryManager.Setup();
        eraManager.Setup();
    }

    public void StartGame()
    {
        tribe = new Tribe(currentDifficulty);
        sessionTime = TimeSpan.Zero;
        hostilityManager.Setup(currentDifficulty);
        eraManager.PrepareEvents();
        territoryManager.GenerateMap();
        period = 0;
        state = GameState.INGAME;
    }

    private void Update()
    {
        if (state == GameState.INGAME)
        {
            var seconds = sessionTime.Seconds;
            sessionTime += TimeSpan.FromSeconds(Time.deltaTime);
            period += Time.deltaTime;
            if (sessionTime.Seconds != seconds)
            {
                tribe.UpdateTribe();
                effectsManager.UpdateEffects();
                hostilityManager.UpdateHostility();
                gamePlayPanel.UpdateViews();
                CheckGameOver();
            }
        }
    }

    public void PauseGame()
    {
        state = GameState.PAUSED;
    }

    public void ResumeGame()
    {
        state = GameState.INGAME;
    }

    private void CheckGameOver()
    {

    }

    public void GameOver()
    {
        state = GameState.GAMEOVER;
    }
}
