
/***********************************************************************************************
 *Spwan Manager for Spwaning of the shape
 *Random Spwan is simple as we have 25% for all shapes : TOdO : Different probability%
 * Object pool ti reuse instead of creating continuosly
 ************************************************************************************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : GenericSingleton<SpwanManager>
{
    public ShapeScriptableObjects[] shapearray;
    GameManager GM;
    public int SpwanInterval;
    public List<ShapeScriptableObjects> PoolList;
    public List<ShapeScriptableObjects> CurrentList;
    public void Init()
    {
       GM = GameManager.Instance;
       InitalizePool();
       EventManager.AddListener(CName.countdownEnd, CreateShape);
    }


    public void InitalizePool()
    {
        for(int i = 0; i< shapearray.Length;i++ )
        {
            PoolList.Add(shapearray[i]);
        }
    }



    public void CreateShape()
    {
        if(GM.curr_GameState == GameState.Gameplay)
        StartCoroutine(Spwan());
    }


    IEnumerator Spwan()
    {
        if (PoolList.Count < 1)
        {
            PoolList.AddRange(CurrentList);
            CurrentList.Clear();
        }
        int rnd = Random.Range(0, PoolList.Count);
        GM.curr_shape  = PoolList[rnd];
        PoolList.RemoveAt(rnd);
        CurrentList.Add(GM.curr_shape);
        EventManager.TriggerEvent(CName.newShape);
        yield return new WaitForSeconds(SpwanInterval);
        CreateShape();
    }



}
