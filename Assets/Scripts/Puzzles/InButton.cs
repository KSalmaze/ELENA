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
        text.text = ExtrairNomeFinal(_thisName);
        _rightName = rightName;
    }

    public void Clicked()
    {
        string normalizedThis = _thisName.Replace(@"\\", @"\");
        string normalizedRight = _rightName.Replace(@"\\", @"\");
        
        Debug.Log(_thisName + " " + _rightName);
        if (normalizedThis == normalizedRight)
        {
            _puzzle.goNext = true;
        }
        else
        {
            ChangeColor();
        }
    }
    
    public string ExtrairNomeFinal(string caminho)
    {
        int ultimaBarra = caminho.LastIndexOf('\\');
        if (ultimaBarra >= 0)
        {
            return caminho.Substring(ultimaBarra + 1);
        }
        return caminho;
    }

    private void ChangeColor()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
}
