using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenPlayerFeedbackForm()
	{
		#if !UNITY_EDITOR
		openWindow("https://docs.google.com/forms/d/e/1FAIpQLSeGHOj8CQYqOfZNHRX6wHB-0iAYdJbJx-KMWYT98dACsIPZcg/viewform?usp=sf_link");
		#endif
		Debug.Log("Player Feedback Form has been opened");
	}

	public void OpenLinkTree()
	{
		#if !UNITY_EDITOR
		openWindow("https://linktr.ee/TheJubbZone");
		#endif
		Debug.Log("The Jubb Zone linktree has been opened");
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}