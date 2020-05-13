using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerResearch
    {
        [Test]
        public void PlayerResearchSimplePasses()
        {

        }
        [UnityTest]
        public IEnumerator PlayerResearchWithEnumeratorPasses()
        {
            var researchCenter = MonoBehaviour.Instantiate(Resources.Load("Prefabs/ResearchTroopsCenterTest", typeof(GameObject))) as GameObject;
            var playerBase = MonoBehaviour.Instantiate(Resources.Load("Prefabs/PlayerBaseTest", typeof(GameObject))) as GameObject;
            if(researchCenter!=null){
                Debug.Log("Research center built");
            }
            else{
                Debug.Log("Error");
            }
            var research = researchCenter.GetComponent<ResearchTest>();
            Debug.Log("Not upgraded research level = "+research.getResearch());
            Debug.Log("Upgrading...");
            research.increaseResearch();
            yield return new WaitForSeconds(3);
            Debug.Log("Upgraded research level = "+research.getResearch());
        }
    }
}