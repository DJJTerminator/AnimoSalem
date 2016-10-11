using UnityEngine;
using System.Collections;

//[RequireComponent( typeof( Light ) )]
/* for lightining
 * Min Range - 100
 * Max Range - 130
 * Min Intense - 2
 * Mx Intense - 8
 * Min Wait - 10
 * Max Wait - 25
 * 
 * for a typical light
 * Min Range - 3
 * Max Range - 3.2
 * Min Intense - 14
 * Mx Intense - 15
 * Min Wait - 0
 * Max Wait - 1
 * 
 * 
 * 
*/

public class FluorescentFlicker : MonoBehaviour {

	public Light[] bulb;

	public float minRange;
	public float maxRange;

	public float minIntense;
	public float maxIntense;

	public int minWait;
	public int maxWait;

	private float waitCycle;
	private bool flicker = false;



	// Use this for initialization
	void Start ()  
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > waitCycle && flicker == false)
		{
		
			flicker = true;
			waitCycle = Random.Range (0.2f,1) + Time.time;

			//for lightning, add a wait delay or start courotine and then play thunder sound

		}

		if (Time.time > waitCycle && flicker == true)
		{
			flicker = false;
			for (int i = 0; i < bulb.Length; i++)
			{
				waitCycle = Random.Range (minWait,maxWait) + Time.time;
				bulb[i].intensity = 0;
				bulb[i].range = 0;
			}
			
		}

		if (flicker == true)
		{
			float intense = Random.Range (minIntense,maxIntense);
			float range = Random.Range (minRange,maxRange);

			for (int i = 0; i < bulb.Length; i++)
			{
				bulb[i].intensity = intense;
				bulb[i].range = range;
			}
		}



//		light.intensity = minIntense;
//		light.intensity = maxIntense;
//
//		light.range = minRange;
//		light.range = maxRange;
	
	}
}
