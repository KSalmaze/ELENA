using Puzzles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InButton : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private string _rightName;
    private string _thisName;
    private FindTheFile _puzzle;
    
    public void Initialize(FindTheFile puzzle, string bName, string rightName)
    {
        _puzzle = puzzle;
        _thisName = bName;
        text.text = _thisName;
        _rightName = rightName;
    }

    public void Clicked()
    {
        if (_thisName == _rightName)
        {
            // _puzzle.next
        }
        else
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
}
