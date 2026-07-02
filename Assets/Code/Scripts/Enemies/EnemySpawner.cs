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
    [SerializeField] GameObject speedster;
    [SerializeField] GameObject speedsterringed;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject boss;


    [Header("Infrared Stuff")]
    private bool isInfrared = false;
    [SerializeField] Material infraredMaterial;

    public void Infrared()
    {
        isInfrared = !isInfrared;
    }

    public void SpawnAtom()
    {
        GameObject Atom = Instantiate(atom, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Atom.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
        }
    }
    public void SpawnAtomRinged()
    {
        GameObject Atomringed = Instantiate(atomringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Atomringed.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
        }
    }

    public void SpawnTank()
    {
        GameObject Tank = Instantiate(tank, LevelManager.Instance.startPoint.position, Quaternion.identity);
        if (isInfrared)
        {
          Renderer _Renderer = Tank.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
        } 
    }

    public void SpawnTankRinged()
    {
        GameObject Tankringed = Instantiate(tankringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Tankringed.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
        } 
    }

    public void SpawnSpeedster()
    {
        GameObject Speedster = Instantiate(speedster, LevelManager.Instance.startPoint.position, Quaternion.identity);         
        if (isInfrared)
        {
          Renderer _Renderer = Speedster.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
        } 
    }

    public void SpawnSpeedsterRinged()
    {
        GameObject Speedsterringed = Instantiate(speedsterringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Speedsterringed.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
        } 
    }

}
