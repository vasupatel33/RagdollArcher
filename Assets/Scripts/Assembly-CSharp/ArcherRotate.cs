using DG.Tweening;
using UnityEngine;

public class ArcherRotate : MonoBehaviour
{
	[SerializeField]
	private GameObject Arrow;

	[SerializeField]
	private GameObject leftLowerHand;

	private void LateUpdate()
	{
		float z = base.transform.localRotation.eulerAngles.z;
		Arrow.transform.DOLocalRotate(new Vector3(0f, 0f, z), 1f);
		Arrow.transform.position = base.transform.position;
	}
}
