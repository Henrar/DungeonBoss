using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {
    public int LevelNumber;
    public int PlayerScore;

    public int ScorePerPaladin;
    public int ScorePerMage;

    private int MaxNumberOfPaladinsPerLevel;
    private int MaxNumberOfWizzardsPerLevel;
    private int CurrentNumberOfPaladins;
    private int CurrentNumberOfMages;
    private int numberOfKilledPaladinsPerLevel;
    private int numberOfKilledWizzardsPerLevel;

    private bool isLevelStart;
    private bool isPlayerDead;

    public float paladinSpawnTime = 3.0f;
    public float wizzardSpawnTime = 4.5f;

    public GameObject paladinPrefab;
    public GameObject wizzardPrefab;

    private GUIStyle guiStyle = new GUIStyle();

    void Awake() {
        LevelNumber = 1;
        PlayerScore = 0;
        CurrentNumberOfPaladins = 0;
        CurrentNumberOfMages = 0;
        StartCoroutine(RemoveLabel());
        InvokeRepeating("SpawnPaladin", paladinSpawnTime, paladinSpawnTime);
        InvokeRepeating("SpawnWizzard", wizzardSpawnTime, wizzardSpawnTime);
        isPlayerDead = false;
        numberOfKilledPaladinsPerLevel = 0;
        numberOfKilledWizzardsPerLevel = 0;
        MaxNumberOfPaladinsPerLevel = 1;
        MaxNumberOfWizzardsPerLevel = 1;
    }

    void OnGUI() {
        if (isLevelStart && !isPlayerDead) {
            guiStyle.fontSize = 40;
            guiStyle.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, Screen.width, Screen.height), "Level " + LevelNumber, guiStyle);
        }
        if (isPlayerDead) {
            guiStyle.fontSize = 60;
            guiStyle.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, Screen.width, Screen.height), "YOU DIED!", guiStyle);
            Time.timeScale = 0;
        }
    }

	void Update() {
        CurrentNumberOfPaladins = GameObject.FindGameObjectsWithTag("Paladin").Length;
        CurrentNumberOfMages = GameObject.FindGameObjectsWithTag("Wizzard").Length;;
        if (numberOfKilledPaladinsPerLevel == MaxNumberOfPaladinsPerLevel && numberOfKilledWizzardsPerLevel == MaxNumberOfWizzardsPerLevel) {
            LevelNumber = LevelNumber + 1;
            startLevel();
        }
    }

    public void addScoreForKilledPaladin() {
        this.PlayerScore = this.PlayerScore + ScorePerPaladin;
        CurrentNumberOfPaladins = CurrentNumberOfPaladins - 1;
        numberOfKilledPaladinsPerLevel = numberOfKilledPaladinsPerLevel + 1;
    }

    public void addScoreForKilledMage() {
        this.PlayerScore = this.PlayerScore + ScorePerMage;
        CurrentNumberOfMages = CurrentNumberOfMages - 1;
        numberOfKilledWizzardsPerLevel = numberOfKilledWizzardsPerLevel + 1;
    }

    private void startLevel() {
        MaxNumberOfPaladinsPerLevel = LevelNumber;
        MaxNumberOfWizzardsPerLevel = LevelNumber;
        CurrentNumberOfPaladins = 0;
        CurrentNumberOfMages = 0;
        numberOfKilledPaladinsPerLevel = 0;
        numberOfKilledWizzardsPerLevel = 0;
        StartCoroutine(RemoveLabel());
    }

    IEnumerator RemoveLabel() {
        isLevelStart = true;
        yield return new WaitForSeconds(5);
        isLevelStart = false;
    }

    private void SpawnPaladin() {
        if (CurrentNumberOfPaladins < MaxNumberOfPaladinsPerLevel && numberOfKilledPaladinsPerLevel < MaxNumberOfPaladinsPerLevel) {
            GameObject newPaladin = (GameObject)Instantiate(paladinPrefab, this.transform.position, Quaternion.identity);
            CurrentNumberOfPaladins = CurrentNumberOfPaladins + 1;
        }
    }

    private void SpawnWizzard() {
        if (CurrentNumberOfMages < MaxNumberOfWizzardsPerLevel && numberOfKilledWizzardsPerLevel < MaxNumberOfWizzardsPerLevel) {
            GameObject newWizzard = (GameObject)Instantiate(wizzardPrefab, this.transform.position, Quaternion.identity);
            CurrentNumberOfMages = CurrentNumberOfMages + 1;
        }
    }

    public void playerDied() {
        this.isPlayerDead = true;
    }
}

