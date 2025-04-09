using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EvolutionsManager evolutionsManager;
    [SerializeField]
    private TerritoryManager territoryManager;
    [SerializeField]
    private EraManager eraManager;
    [SerializeField]
    private GamePlayPanel gamePlayPanel;
    private HostilityManager hostilityManager;
    public List<DifficultySetting> difficulties;

    private static GameManager instance;

    private float period;
    private Tribe tribe;
    private TimeSpan sessionTime;
    private bool isPaused = false;
    private bool isPlaying = false;
    private DifficultySetting currentDifficulty;
    private Dictionary<string, GameEvent> happenedEvents;

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
    public HostilityManager HostilityManager => hostilityManager;
    public Tribe Tribe => tribe;
    public int GetSessionSeconds => (int)sessionTime.TotalSeconds;

    public bool HasEventHappened(string eventId)
    {
        return happenedEvents.ContainsKey(eventId);
    }

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
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (!isPaused)
            {
                var seconds = sessionTime.Seconds;
                sessionTime += TimeSpan.FromSeconds(Time.deltaTime);
                period += Time.deltaTime;
                if (sessionTime.Seconds != seconds)
                {
                    tribe.UpdateTribe();
                    hostilityManager.UpdateHostility();
                    UpdateGameState();
                    gamePlayPanel.UpdateViews();
                }
            }
        }
    }

    public void GameOver()
    {

    }

    private void UpdateGameState()
    {

    }
}
