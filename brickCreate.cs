using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickCreate : MonoBehaviour {
	public GameObject hexagon200x200;
	Vector3 hexagonVector0,hexagonVector1,hexagonVector2,hexagonVector3,hexagonVector4,hexagonVector5;  //六邊形基本向量位移   用於拼接六邊形

	Vector3[] hexagonVectorArray = new Vector3[6];		//物件陣列  基本向量
	public static List<GameObject> hexagon200x200List = new List<GameObject>();			//地磚串  用於動態移動地磚
	void Start () {
		hexagonVector0.x = 1.5f*parameter.hexagonLength* (-1);				//左上
		hexagonVector0.y = Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2) -parameter.hexagonLength)*(0.5f*parameter.hexagonLength);

		hexagonVector1.x = 0f;
		hexagonVector1.y = Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2) -parameter.hexagonLength);		//正上

		hexagonVector2.x = 1.5f*parameter.hexagonLength;						//右上
		hexagonVector2.y =Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2) -parameter.hexagonLength) *(0.5f*parameter.hexagonLength);	

		hexagonVector3.x = 1.5f*parameter.hexagonLength;						//右下
		hexagonVector3.y = Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2) -parameter.hexagonLength) * (-0.5f*parameter.hexagonLength);

		hexagonVector4.x = 0f;						//正下
		hexagonVector4.y =Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2)-parameter.hexagonLength) * (-1);

		hexagonVector5.x = 1.5f *parameter.hexagonLength* (-1);					//左下
		hexagonVector5.y =Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2)-parameter.hexagonLength) *(-0.5f*parameter.hexagonLength);

		hexagonVectorArray[0] =hexagonVector2;//右上
		hexagonVectorArray[1] =hexagonVector3;//右下
		hexagonVectorArray[2] =hexagonVector4;//正下
		hexagonVectorArray[3] =hexagonVector5;//左下
		hexagonVectorArray[4] =hexagonVector0;//左上
		hexagonVectorArray[5] =hexagonVector1;//正上
		
		CreateHexagon (parameter.hexagonBrickLayer);		
	
		
	}


	void Update () {
		
	}
	protected void CreateHexagon(int layer){		//創造所有地磚
		int layernow = 0;
		Vector3 vectorNow = new Vector3 (0, 0, 0);
		HexagonCreateAddToList (vectorNow);		//創造一個地磚並加入地磚list
		while (layernow < layer) {
			vectorNow += hexagonVectorArray [4];							// 左上 下一層
			HexagonCreateAddToList (vectorNow);	
			layernow++;														//層+1
			for (int i = 0; i < layernow - 1; i++) {
				vectorNow += hexagonVectorArray [5];		//正上		產生層數-1的量
				HexagonCreateAddToList (vectorNow);	
			}
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < layernow; j++) {
					vectorNow += hexagonVectorArray [i];
					HexagonCreateAddToList (vectorNow);	
				}
			}
		}
	}
	protected void HexagonCreateAddToList(Vector3 vectorNow){	//創造一個地磚並加入地磚list
		GameObject a= Instantiate (hexagon200x200, vectorNow, Quaternion.identity);
		hexagon200x200List.Add (a);  //加入地磚串
	}
}
