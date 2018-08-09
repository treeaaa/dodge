using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickMove : MonoBehaviour {
	int brickCenter;		//中心點
	int[] brickAround = new int[6];		//六個圍繞的地磚
	float sqrt3 = Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2)-parameter.hexagonLength);
	void Start () {
		brickCenter = 0;						//在hexagon200x200List 預設0是中心   1~6=周圍六點
		for (int i = 1; i < 7; i++) {
			brickAround [i-1] = i;
		}
	}
	void Update () {
		for (int i = 0; i < brickAround.Length; i++) {
			if (Vector3.Distance (parameter.playerXY, brickCreate.hexagon200x200List [brickAround [i]].transform.position) < sqrt3/2) {
				Vector3 coorDinateA,coorDinateB,coorDinateCenter;	//座標A為原本為中心的座標  座標B為新中心的座標
				float slope;			//斜率
				float constantB;		//常數b
				coorDinateA = brickCreate.hexagon200x200List[brickCenter].transform.position;		//座標A為原本為中心的座標
				brickCenter = brickCreate.hexagon200x200List.IndexOf (brickCreate.hexagon200x200List [brickAround [i]]);		//取得索引值
				coorDinateB = brickCreate.hexagon200x200List[brickCenter].transform.position;		//座標B為新中心的座標
				coorDinateCenter =(coorDinateA +coorDinateB)/2;
				slope = (-1)/((coorDinateB.y-coorDinateA.y) / (coorDinateB.x - coorDinateA.x)) ;		//算出斜率  垂直的那條 兩條垂直線  斜率相乘=-1
				constantB = coorDinateCenter.y - slope*coorDinateCenter.x ;


				int brickAroundIndex =0;
				for(int j= 0;j< brickCreate.hexagon200x200List.Count;j++){
					float abDistance = Vector3.Distance (brickCreate.hexagon200x200List [brickCenter].transform.position, brickCreate.hexagon200x200List [j].transform.position);
					if(brickCenter!=j){								//不能抓自己
						if(	1.5f<abDistance&&abDistance<2.5f){		//新的周圍六個邊
							brickAround[brickAroundIndex] =brickCreate.hexagon200x200List.IndexOf (brickCreate.hexagon200x200List[j]);		//取得索引值
							brickAroundIndex++;

						}
						if(abDistance>parameter.hexagonBrickLayer* Mathf.Sqrt (Mathf.Pow(2*parameter.hexagonLength,2)-parameter.hexagonLength)+0.1){	// 層數為5 實際產生6層
							brickCreate.hexagon200x200List[j].transform.position = GetProjectionPoint(brickCreate.hexagon200x200List[j].transform.position,slope, -1f,constantB);									//不需要動的最大為parameter.hexagonBrickLayer*根號3
						}																																	//+0.1為增加判斷範圍 但是不超過最大那一層
					}
				}
			}
		}
	}
	protected Vector3 GetProjectionPoint(Vector3 originalXY, float a,float b,float c){		//取得對對稱點    ax+by+c=0
		Vector3 projectionPoint = new Vector3(0,0,0);
		projectionPoint.x = originalXY.x - 2 * a * (a * originalXY.x + b * originalXY.y + c) / (Mathf.Pow (a, 2) + Mathf.Pow (b, 2));
		projectionPoint.y = originalXY.y - 2 * b * (a * originalXY.x + b * originalXY.y + c) / (Mathf.Pow (a, 2) + Mathf.Pow (b, 2));
		return projectionPoint;
	}
}
