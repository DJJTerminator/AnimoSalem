using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Money : MonoBehaviour {
    [SerializeField]
    bool isCoin;
    Transform target;
    public GameObject CBTMoney;
    public GameObject icons;
    Vector3 dir = Vector3.zero;

    int value;

	// Use this for initialization
	void Start () 
    {
        target = GameObject.FindWithTag("Player").transform;
        icons = GameObject.Find("Icons");
	}
    void Update()
    {
        if (isCoin)
        {
            //getting the target position
            Vector3 targetPos = target.position;
            //moving towards the player for he or she to collect
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref dir, .2f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Player")
		{
			if (isCoin) 
			{
				GetComponent<Money> ().enabled = true;
				GetComponent<AudioSource> ().Play ();
				value = Random.Range (1, 5);
				StartCoroutine (Wait (.2f));
			} 
			else 
			{
				GetComponent<AudioSource> ().Play ();
				value = Random.Range (10, 25);
				StartCoroutine (Wait (.2f));
			}
		}
    }
    //pick up normal coin
    IEnumerator Wait(float waitTime)
    {
        if (isCoin)
        {
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Money>().enabled = false;
            DataStorage.money += value;
            InitCBT("$" + value.ToString());
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            DataStorage.money += value;
            InitCBT("$" + value.ToString());
            yield return new WaitForSeconds(3.0f);
            Destroy(gameObject);
        }
    }

    GameObject InitCBT(string text)
    {
		DataStorage.DisplayGold ();
        GameObject temp = Instantiate(CBTMoney) as GameObject;
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        temp.transform.SetParent(icons.transform.FindChild("MoneyValue"));
        tempRect.transform.localPosition = CBTMoney.transform.transform.localPosition;
        tempRect.transform.localScale = CBTMoney.transform.localScale;
        tempRect.transform.localRotation = CBTMoney.transform.localRotation;

        //Debug.Log(tempRect.transform.localPosition);

        temp.GetComponent<Text>().text = text;
        Destroy(temp.gameObject, 6);
        //temp.GetComponent<Animator>().SetTrigger("Hit");
        return temp;
    }

}
