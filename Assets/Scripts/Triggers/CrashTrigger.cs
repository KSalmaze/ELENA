using System.Collections;
using UnityEngine;

public class CrashTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue npc;
    [SerializeField] private GameObject crashScreen;

    public void StartCounter(string time)
    {
        crashScreen.SetActive(true);
        StartCoroutine(RecoverFromCrash(float.Parse(time)));
    }

    IEnumerator RecoverFromCrash(float time)
    {
        yield return new WaitForSeconds(time);
        crashScreen.SetActive(false);
        npc.GoToNextDialogue();
    }
}
