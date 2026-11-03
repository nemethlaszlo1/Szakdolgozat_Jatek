using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public enum QuestState { NotStarted, InProgress, Completed }

    public QuestState currentState = QuestState.NotStarted;
    public int requiredKills = 5;
    public int rewardXP = 200;

    private bool playerInRange = false;
    private GameManager gm;

    void Start()
    {
        gm = Object.FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (gm == null) return;

        switch (currentState)
        {
            case QuestState.NotStarted:
                Debug.Log("Kérlek, ölj meg " + requiredKills + " szörnyet!");
                gm.isQuestActive = true;
                gm.questKillCount = 0;
                currentState = QuestState.InProgress;
                break;

            case QuestState.InProgress:
                if (gm.questKillCount >= requiredKills)
                {
                    Debug.Log("Köszönöm a segítséget!");
                    gm.AddXP(rewardXP);
                    gm.isQuestActive = false;
                    currentState = QuestState.Completed;
                }
                else
                {
                    Debug.Log("Még hiányzik " + (requiredKills - gm.questKillCount) + " szörny!");
                }
                break;

            case QuestState.Completed:
                Debug.Log("Már segítettél, köszönöm!");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}