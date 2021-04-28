using UnityEngine;


public class TopPrint : MonoBehaviour
{

    [Space(5)]
    [SerializeField] private GameObject panelPlayerStatsPrefab;
    [SerializeField] private GameObject panelGroupList;


    private void Start()
    {
        SaveScore.LoadDataScore();
        PrintTopList();
    }

    public void buttonClearData()
    {
        SaveScore.ClearData();
        PrintTopList();
    }

    private void ClearPanelGroupList()
    {
        for (int i = 0; i < panelGroupList.transform.childCount; i++)
        {
            Destroy(panelGroupList.transform.GetChild(i).gameObject);
        }
    }
    public void PrintTopList()
    {
        ClearPanelGroupList();

        if(SaveScore.playerIndex == 0) { return; }
        for (int i = 0; i < SaveScore.playerIndex; i++)
        {
            var buf = Instantiate(panelPlayerStatsPrefab,panelGroupList.transform);
            var parametrs = buf.GetComponent<PanelPlayerStats>();
            parametrs.SetParametors(""+(i+1),""+SaveScore.Score[i],SaveScore.Name[i]);
        }
    }

    

   
}
