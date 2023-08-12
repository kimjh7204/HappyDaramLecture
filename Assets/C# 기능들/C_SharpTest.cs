using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class C_SharpTest : MonoBehaviour
{
    #region Nullable
    private int? a = null;          //Nullable type
    private Vector3? vec = null;    //Nullable type
    #endregion

    private List<int> ints = null;

    void Start()
    {
        #region ªÔ«◊ø¨ªÍ¿⁄
        int b = (vec == Vector3.up) ? 1 : 0;

        //int b;
        //if (vec == Vector3.up) b = 1;
        //else b = 0;
        #endregion

        #region Nullable
        //PreProcess (º±√≥∏Æ)

        if (a == null) a = 45;
        if (vec == null) vec = new Vector3(1f, 1f, 1f);
        if(ints is null) ints = new List<int>();
        #endregion

        #region (?.)
        int? countTemp = ints?.Count;

        //int? countTemp;
        //if (ints is null) countTemp = null;
        //else countTemp = ints.Count;
        #endregion

        #region ??
        Happy happy = new Happy();
        Daram daram = happy.daram ?? new Daram();

        //if(happy.daram == null) daram = new Daram();
        //else daram = happy.daram;

        happy ??= new Happy();
        //happy = happy ?? new Daram();

        //if(happy == null) happy = new Daram();
        #endregion

        #region Tuple
        List<int> data = new(){3,6,3,7,4,9,23,453,2,3,2};

        var result = Calculate(data);
        //(int cnt, int sum, double avg) = Calculate(data);
        int cnt, sum;
        double avg;
        (cnt, sum, avg) = Calculate(data);

        Debug.Log(result.count);
        Debug.Log(result.sum);
        Debug.Log(result.average);

        var result2 = Calculate2(data);

        Debug.Log(result2.Item1);
        Debug.Log(result2.Item2);
        Debug.Log(result2.Item3);
        #endregion

    }

    private (int count, int sum, double average) Calculate(List<int> data) //∆©«√ ∏Æ≈œ≈∏¿‘
    {
        int cnt = 0, sum = 0;
        double avg = 0;

        foreach (var i in data)
        {
            cnt++;
            sum += i;
        }

        avg = sum / cnt;

        return (cnt, sum, avg); //∆©«√ ∏Æ≈Õ∑≤
    }

    private (int, int, double) Calculate2(List<int> data) //∆©«√ ∏Æ≈œ≈∏¿‘
    {
        int cnt = 0, sum = 0;
        double avg = 0;

        foreach (var i in data)
        {
            cnt++;
            sum += i;
        }

        avg = sum / cnt;

        return (cnt, sum, avg); //∆©«√ ∏Æ≈Õ∑≤
    }
}

public class Happy
{
    public Daram daram;
}

public class Daram
{

}
