/****************************************************
	文件：ViewUnit.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2023/03/23 4:25   	
	功能：基础表现控制
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ViewUnit : MonoBehaviour {

	//POS
	public bool IsSynPos;

	//Dir
	public bool IsSynDir;

	public Transform RoationRoot;

	LogicUnit logicUnit = null;


	public virtual void Init() {
		this.logicUnit = logicUnit;
		gameObject.name=logicUnit.unitName+"_"+gameObject.name;

		transform.position = logicUnit.LogicPos.ConvertViewVector3();
		if (RoationRoot==null) {
			RoationRoot = transform;
		}
		RoationRoot.rotation = CalcRotation(logicUnit.LogicDir.ConvertViewVector3());

    }

	private void Update() {
		if (IsSynDir) {
			UpdateDirection();

        }
		if (IsSynPos) {
			UpdatePosition();

        }
	}

	void UpdateDirection() {

	}

	void UpdatePosition() {
		ForcePosSync();

    }
	

	protected Quaternion CalcRotation(Vector3 targetDir) {
		return Quaternion.FromToRotation(Vector3.forward, targetDir);
	}

	public void ForcePosSync() {
		transform.position = logicUnit.LogicPos.ConvertViewVector3();
	}
}