using DG.Tweening;
using UnityEngine;

public class Shaker : MonoBehaviour
{


    public float shake_decay = 0.002f;
	public float shake_intensity = .3f;

	public float shake_time = 0.1f;

    private Vector3 originPosition;
	private Quaternion originRotation;

    private float temp_shake_intensity = 0;
	
    void Update2 (){

		if (temp_shake_intensity > 0){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
 				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		}
	}
	
	public void Shake(){
		transform.DOShakePosition(0.1f);
		//originPosition = transform.position;
		//originRotation = transform.rotation;
		//temp_shake_intensity = shake_intensity;

	}


}
