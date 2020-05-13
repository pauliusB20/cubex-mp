using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUndo : MonoBehaviour
{
    [SerializeField] TurretBuild turretBtn;
    [SerializeField] BarrackBuild barrackBtn;
    [SerializeField] ResearchCentreBuild researchCenterBtn;
    [SerializeField] BuildTroopsResearchCenter troopsCenterBtn;
    [SerializeField] BuildWorker buildWorkerBtn;
    [SerializeField] BuildPlayerWall buildPlayerWallBtn;
    [SerializeField] CollectorBuild collectorBuild;
    [SerializeField] GameObject clickUndo;
    private Base playerbase;
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<Base>() == null)
        {
          return;
        }
        else
        {
          playerbase = FindObjectOfType<Base>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void undoClickOnBuildTurret()
    {
        turretBtn.canBuildAgain(true);
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);
    }

    public void undoClickOnBiuldBarrack()
    {
        barrackBtn.canBuildAgain(true);
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);

    }

    public void undoClickOnBuildResearchCenter()
    {
        researchCenterBtn.canBuildAgain(true);
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);
    }

    public void undoClickOnBuildTroopsResearch()
    {
        troopsCenterBtn.canBuildAgain(true);
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);
    }

    public void undoClickOnBuildDefensiveWall()
    {
        buildPlayerWallBtn.canBuildAgain(true);
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);
    }

    public void undoClickOnBuildEnergonCollector()
    {
        collectorBuild.setCollectorStructureBuilt(true); 
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);
    } 

    public void undoClickOnBuildRegWorker()
    {
        buildWorkerBtn.canBuildAgain(true);
        playerbase.setBuildingArea(false);
        clickUndo.SetActive(false);
    }
}
