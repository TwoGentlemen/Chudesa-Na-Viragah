using UnityEngine;
using UnityEngine.UI;

public class PanelPlayerStats : MonoBehaviour
{
    [SerializeField] private Text textPos;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textName;

    public void SetParametors(string pos, string score,string name)
    {
        textPos.text = pos;
        textScore.text = score;
        textName.text = name;
    }
}
