using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class move : MonoBehaviour {
    NavMeshAgent agent;
    ClickOn click;
    [SerializeField] GameObject unitPosition;
    void Start () {
        agent = GetComponent<NavMeshAgent> ();
        agent.Warp(transform.position);
        click = GetComponent<ClickOn> ();
        FindObjectOfType<GameSession>().addTroopAmount(1);
    }
    void Update () {
        unitMove ();
    }
    private void unitMove () {
        if (!click.GetSelected ()) {
            return;
        }
        if (Input.GetMouseButtonDown (0)) {
            if(!EventSystem.current.IsPointerOverGameObject()) {
            RaycastHit hit;
            if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity)) {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Ground") || hit.transform.gameObject.layer == LayerMask.NameToLayer ("LvlMap")) {
                    agent.destination = hit.point;
                } 
                else 
                {
                    return;
                }
            }
        }
        }
    }
    public Vector3 getUnitPosition () {
        return unitPosition.transform.position;
    }

}