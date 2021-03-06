using UnityEngine;
using System.Collections;

public class LoadingPanelCreator : MonoBehaviour {

	public GameObject canvasParent;
	public GameObject panelPrefab;
	private GameObject thisPanel;

	public void CreateLoadingPanel(){
		thisPanel = Instantiate(panelPrefab);
		thisPanel.transform.SetParent(canvasParent.transform, false);
	}

	public void DestroyLoadingPanel(){
		Destroy(thisPanel);
	}
}
