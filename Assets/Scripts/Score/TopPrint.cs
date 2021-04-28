using UnityEngine;


public class TopPrint : MonoBehaviour
{

    [Space(5)]
    [SerializeField] private GameObject panelPlayerStatsPrefab;
    [SerializeField] private GameObject[] panelGroupList = new GameObject[SaveScore.countLevel];
    [SerializeField] private GameObject[] panelTopList = new GameObject[SaveScore.countLevel];


    private void Awake()
    {
        SaveScore.LoadDataScore();
    }
    private void Start()
    {
        
        PrintTopList();
    }

    public void buttonClearData()
    {
        SaveScore.ClearData(0);
        PrintTopList();
    }

    public void ChangeTopList(int numActive)
    {
        if(numActive > SaveScore.countLevel || numActive < 0) { Debug.LogError("Error!");return;}
        for (int i = 0; i < SaveScore.countLevel; i++)
        {
            if(i != numActive)
            {
                panelTopList[i].SetActive(false);
            }
            else
            {
                panelTopList[i].SetActive(true);
            }
        }
    }

    private void ClearPanelGroupList()
    {
        for (int j = 0; j < SaveScore.countLevel; j++)
        {

            for (int i = 0; i < panelGroupList[j].transform.childCount; i++)
            {
                Destroy(panelGroupList[j].transform.GetChild(i).gameObject);
            } 
        }
    }
    public void PrintTopList()
    {
        ClearPanelGroupList();

        for (int j = 0; j < SaveScore.playerIndex.Length; j++)
        {

            if(!(SaveScore.playerIndex[j] == 0)) { 

            for (int i = 0; i < SaveScore.playerIndex[j]; i++)
            {
                var buf = Instantiate(panelPlayerStatsPrefab,panelGroupList[j].transform);
                var parametrs = buf.GetComponent<PanelPlayerStats>();
                parametrs.SetParametors(""+(i+1),""+SaveScore.Score[j,i],SaveScore.Name[j,i]);
            } 
            }
        }
    }

    

   
}
