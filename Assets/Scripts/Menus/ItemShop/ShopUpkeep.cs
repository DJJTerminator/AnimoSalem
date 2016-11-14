using UnityEngine;
using System.Collections;

public class ShopUpkeep : MonoBehaviour {
	[Header("These are the sold out images for the items")]
	[SerializeField]
	GameObject _hgAmmoImage;
	[SerializeField]
	GameObject _sgAmmoImage;
	[SerializeField]
	GameObject _mgAmmoImage;
	[SerializeField]
	GameObject _rifleAmmoImage;
	[SerializeField]
	GameObject _magnumAmmoImage;
	[SerializeField]
	GameObject _smallAidImage;
	[SerializeField]
	GameObject _medAidImage;
	[SerializeField]
	GameObject _largeAidImage;
	[SerializeField]
	GameObject _holyWaterImage;
	[SerializeField]
	GameObject _GameManager;

	float timeBetweenItems;

	// Use this for initialization
	void Start ()
	{
	}

	void Update()
	{
		if (Time.time > DataStorage.shopKeepTimer) 
		{
			RandomUpKeep ();
			DataStorage.shopKeepTimer += Random.Range (Time.time + 100 - (DataStorage.charisma * 2), Time.time + 120 - (DataStorage.charisma * 2));
		}
	}

	public int AddCharisma(int charisma)
	{
		int remainder = charisma % 4;
		charisma = DataStorage.charisma / 4;
		if (charisma % 4 == 0) 
		{
			print (charisma);
			return charisma;
		} 
		else 
		{
			print (charisma);
			charisma -= remainder;
			print (charisma);
			return charisma;
		}
	}

	public void RandomUpKeep()
	{
		int charism = AddCharisma (DataStorage.charisma);
		
		//randomizing all shop keeper's upkeep
		DataStorage.shopHandgunAmmo = Random.Range (0,4+charism);
		DataStorage.shopRifleAmmo = Random.Range (0,2+charism);
		DataStorage.shopMachinegunAmmo = Random.Range (0,4+charism);
		DataStorage.shopShotgunAmmo = Random.Range (0,2+charism);
		DataStorage.shopMagnumAmmo = Random.Range (0,2+charism);
		DataStorage.shopSmallAid = Random.Range (0,2+charism);
		DataStorage.shopMedAid = Random.Range (0,1+charism);
		DataStorage.shopLargeAid = Random.Range (0,1+charism);
		DataStorage.shopHolyWater = Random.Range (0,2+charism);

		//enabling and disabling the sold out game objects
		if (DataStorage.shopHandgunAmmo > 0)
			_hgAmmoImage.SetActive (false);
		else
			_hgAmmoImage.SetActive (true);
		
		if (DataStorage.shopShotgunAmmo > 0)
			_sgAmmoImage.SetActive (false);
		else
			_sgAmmoImage.SetActive (true);
		
		if (DataStorage.shopMachinegunAmmo > 0)
			_mgAmmoImage.SetActive (false);
		else
			_mgAmmoImage.SetActive (true);
		
		if (DataStorage.shopRifleAmmo > 0)
			_rifleAmmoImage.SetActive (false);
		else
			_rifleAmmoImage.SetActive (true);
		
		if (DataStorage.shopMagnumAmmo > 0)
			_magnumAmmoImage.SetActive (false);
		else
			_magnumAmmoImage.SetActive (true);
		
		if (DataStorage.shopSmallAid > 0)
			_smallAidImage.SetActive (false);
		else
			_smallAidImage.SetActive (true);
		
		if (DataStorage.shopMedAid > 0)
			_medAidImage.SetActive (false);
		else
			_medAidImage.SetActive (true);
		
		if (DataStorage.shopLargeAid > 0)
			_largeAidImage.SetActive(false);
		else
			_largeAidImage.SetActive(true);
		
		if (DataStorage.shopHolyWater > 0)
			_holyWaterImage.SetActive (false);
		else
			_holyWaterImage.SetActive (true);
	}

	//these functions disable the sold out images
	public void HGAmmoSoldOut()
	{
		_hgAmmoImage.SetActive (true);
	}

	public void SGAmmoSoldOut()
	{
		_sgAmmoImage.SetActive (true);
	}

	public void MGAmmoSoldOut()
	{
		_mgAmmoImage.SetActive (true);
	}

	public void RifleAmmoSoldOut()
	{
		_rifleAmmoImage.SetActive (true);
	}

	public void MagnumAmmoSoldOut()
	{
		_magnumAmmoImage.SetActive (true);
	}
	public void MedAidSoldOut()
	{
		_medAidImage.SetActive (true);
	}
	public void SmallAidoSoldOut()
	{
		_smallAidImage.SetActive (true);
	}
	public void LargeAidoSoldOut()
	{
		_largeAidImage.SetActive (true);
	}
	public void HolyWaterSoldOut()
	{
		_holyWaterImage.SetActive (true);
	}

}
