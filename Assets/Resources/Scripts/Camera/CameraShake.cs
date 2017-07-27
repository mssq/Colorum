using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera mainCam;

    private float shakeAmount = 0;
    private float offsetX = 0f;
    private float offsetY = 0f;
    private Vector3 camOrigPos;
    private bool toOrigPos = false;
    private bool camShakeOn = false;

    void Awake() {
        if (mainCam == null)
            mainCam = Camera.main;
    }

    void Update() {

        if (toOrigPos) {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, camOrigPos, Time.deltaTime * 15f);
        }
    }

    public void Shake(float amount, float length) {

        if (!getCamShakeOn()) {
            setCamShakeOn(true);
            toOrigPos = true;
            camOrigPos = mainCam.transform.position;
            shakeAmount = amount;
            InvokeRepeating("BeginShake", 0, 0.01f);
            Invoke("StopShake", length);
        }
        
    }

    void BeginShake() {

        if (shakeAmount > 0) {
            Vector3 camPos = mainCam.transform.position;
            if (offsetX < 0.00f) {
                offsetX = Mathf.Abs(Random.value * shakeAmount * 2);
            } else if (offsetX > 0) {
                offsetX = (Random.value * shakeAmount * 2) * -1;
            } else {
                offsetX = Random.value * shakeAmount * 2;
            }

            if (offsetY < 0) {
                offsetY = Mathf.Abs(Random.value * shakeAmount * 2);
            } else if (offsetY > 0) {
                offsetY = (Random.value * shakeAmount * 2) * -1;
            } else {
                offsetY = Random.value * shakeAmount * 2;
            }

            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCam.transform.position = camPos;
        }
    }

    void StopShake() {
        CancelInvoke("BeginShake");
        Invoke("EnableScripts", 0.25f);
    }

    void EnableScripts() {
        toOrigPos = false;
        setCamShakeOn(false);
    }

    public void setCamShakeOn(bool status) {
        camShakeOn = status;
    }

    public bool getCamShakeOn() {
        return camShakeOn;
    }

}
