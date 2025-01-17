using UnityEngine;

public class Generic : MonoBehaviour
{
    public void SwitchGameObjectState(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
