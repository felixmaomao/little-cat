using UnityEngine;
using System.Collections;
using UnityEditor;
public class ConversationAssetCreator : MonoBehaviour
{
	public static void CreateAssert(){
		CustomAssetUtility.CreateAsset<Conversation>();
	}
}

