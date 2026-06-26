using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] GameObject atom;
    [SerializeField] GameObject atomringed;
    [SerializeField] GameObject tank;
    [SerializeField] GameObject tankringed;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject speedster;
    [SerializeField] GameObject infared;
    [SerializeField] GameObject boss;

    public void SpawnAtom()
    {
        GameObject Atom = Instantiate(atom, LevelManager.Instance.startPoint.position, Quaternion.identity); 
    }
    public void SpawnAtomRinged()
    {
        GameObject Atomringed = Instantiate(atomringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
    }

    public void SpawnTank()
    {
        GameObject Tank = Instantiate(tank, LevelManager.Instance.startPoint.position, Quaternion.identity); 
    }

    public void SpawnTankRinged()
    {
        GameObject Tankringed = Instantiate(tankringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
    }

}
