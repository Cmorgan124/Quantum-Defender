using System.Collections;
using System.Collections.Generic;
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
          Health enemyScript = Atom.GetComponent<Health>();
          if (enemyScript != null)
          {
            enemyScript.IsInfrared = true;
          }
        }
    }
    public void SpawnAtomRinged()
    {
        GameObject Atomringed = Instantiate(atomringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Atomringed.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
          Health enemyScript = Atomringed.GetComponent<Health>();
          if (enemyScript != null)
          {
            enemyScript.IsInfrared = true;
          }
        }
    }

    public void SpawnTank()
    {
        GameObject Tank = Instantiate(tank, LevelManager.Instance.startPoint.position, Quaternion.identity);
        if (isInfrared)
        {
          Renderer _Renderer = Tank.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
          Health enemyScript = Tank.GetComponent<Health>();
          if (enemyScript != null)
          {
            enemyScript.IsInfrared = true;
          }
        } 
    }

    public void SpawnTankRinged()
    {
        GameObject Tankringed = Instantiate(tankringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Tankringed.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
          Health enemyScript = Tankringed.GetComponent<Health>();
          if (enemyScript != null)
          {
            enemyScript.IsInfrared = true;
          }
        } 
    }

    public void SpawnSpeedster()
    {
        GameObject Speedster = Instantiate(speedster, LevelManager.Instance.startPoint.position, Quaternion.identity);         
        if (isInfrared)
        {
          Renderer _Renderer = Speedster.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
          Health enemyScript = Speedster.GetComponent<Health>();
          if (enemyScript != null)
          {
            enemyScript.IsInfrared = true;
          }
        } 
    }

    public void SpawnSpeedsterRinged()
    {
        GameObject Speedsterringed = Instantiate(speedsterringed, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Renderer _Renderer = Speedsterringed.GetComponent<Renderer>();
          _Renderer.material = new Material(infraredMaterial);
          Health enemyScript = Speedsterringed.GetComponent<Health>();
          if (enemyScript != null)
          {
            enemyScript.IsInfrared = true;
          }
        } 
    }

    public void SpawnShield()
    {
        GameObject Shield = Instantiate(shield, LevelManager.Instance.startPoint.position, Quaternion.identity); 
        if (isInfrared)
        {
          Transform[] transforms = Shield.GetComponentsInChildren<Transform>(true);
          foreach(Transform _Transform in transforms)
          {
            if(_Transform == Shield.transform) continue;
            GameObject obj = _Transform.gameObject;
            Renderer _Renderer = obj.GetComponent<Renderer>();
            _Renderer.material = new Material(infraredMaterial);
            Health enemyScript = obj.GetComponent<Health>();
            if (enemyScript != null)
            {
              enemyScript.IsInfrared = true;
            }
          }
        } 
    }

}
