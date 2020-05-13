using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class healthTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
           /* var playeGround = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Plane", typeof(GameObject))) as GameObject;
            var worker = MonoBehaviour.Instantiate(Resources.Load("Prefabs/PlayerWorker 1", typeof(GameObject))) as GameObject;
            if(worker != null)
            { 
              Debug.Log("viska suradau!");
            }
            else
            {
              Debug.Log("neradau");
            }
            */
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            var worker = MonoBehaviour.Instantiate(Resources.Load("Prefabs/PlayerWorker 1", typeof(GameObject))) as GameObject;
            int damagePoints = 15;
            Debug.Log("Current worker Health before receiving damage : " + worker.GetComponent<WorkerTestingHealth>().getHealthOfStructureOriginal1());
            Debug.Log("Worker is attacked...");
            worker.GetComponent<WorkerTestingHealth>().decreaseHealth(damagePoints);
            Assert.True(worker.GetComponent<WorkerTestingHealth>().shot(),"Current worker is shot ");
            Debug.Log("Current worker is shot");
            Assert.AreNotEqual(worker.GetComponent<WorkerTestingHealth>().getHealth1(), worker.GetComponent<WorkerTestingHealth>().getHealthOfStructureOriginal1());
            Debug.Log("Current worker Health after damage received: " + worker.GetComponent<WorkerTestingHealth>().getHealth1());
            Debug.Log("After 15 sec regenerating will start...");
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return new WaitForSeconds(30f);
            Debug.Log("Regenerating is Finished");
            Debug.Log("Worker Health after regenerating: " + worker.GetComponent<WorkerTestingHealth>().getHealth1());
        }
    }
}
