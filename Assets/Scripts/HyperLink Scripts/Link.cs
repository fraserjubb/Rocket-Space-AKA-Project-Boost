using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void OpenPlayerFeedbackForm()
	{
		#if !UNITY_EDITOR
		openWindow("https://forms.gle/3fyyu79KLBVHLNP96");
		#endif
		Debug.Log("Player Feedback Form has been opened");
	}

	public void OpenReportingBugOrGlitchForm()
	{
		#if !UNITY_EDITOR
		openWindow("https://forms.gle/APgfNBryx2c5vfow5");
		#endif
		Debug.Log("Bug/Glitch Form has been opened");
	}

	public void OpenLinkTree()
	{
		#if !UNITY_EDITOR
		openWindow("https://linktr.ee/TheJubbZone");
		#endif
		Debug.Log("The Jubb Zone linktree has been opened");
	}

	public void OpenGamesTesterApplication()
	{
		#if !UNITY_EDITOR
		openWindow("https://forms.gle/HuyX4XWi9xKEsmmc6");
		#endif
		Debug.Log("Games Tester Form has been opened");
	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}