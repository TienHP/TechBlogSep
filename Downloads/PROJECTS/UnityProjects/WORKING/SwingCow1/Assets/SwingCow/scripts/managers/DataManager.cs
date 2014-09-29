using UnityEngine;
using System.Collections;
using TNg_Framework;

public class DataManager : MonoBehaviour {

	public static DataManager entity;
	public PlayerProperties playerProps;
	private string relativeAssetFolder = "/SwingCow/xmls/";
	public TextAsset playerXml;

	void Awake(){
		entity = this;
	}//end method

	void Start(){
		OnGameLoading();
	}//end method

	void OnGameLoading(){
		//load all xml files to data object
		LoadXmlsToObjects();
	}//end method

	void LoadXmlsToObjects(){

		//load player data
		playerProps = XMLParser.LoadInstanceAsXml(relativeAssetFolder + playerXml.name + ".xml", typeof(PlayerProperties)) as PlayerProperties;

	}//end method

}//end class
