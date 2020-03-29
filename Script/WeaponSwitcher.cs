using UnityEngine;

public class WeaponSwitcher : MonoBehaviour {
    public int selectedWeapon = 0;
    public Animator animate;
    public Animator animateScope;
    public bool Scope = false;
    public GameObject Weapon;
    public int zoomCam = 15;
    public Camera ScopeCam;

    public GameObject ScopeOverlay;
    
    void Start() {
        SelectWeapon();
    }

    void Update() {

        int previousSelectedWeapon = selectedWeapon;
    
        if (Input.GetAxis("Mouse ScrollWheel") > 0f){
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;  
            animate.SetTrigger("Grab");
		}

        if (Input.GetAxis("Mouse ScrollWheel") < 0f){
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;  
            animate.SetTrigger("Grab");
		}

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedWeapon = 0;
            animate.SetTrigger("Grab");
        }

       if (Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedWeapon = 1;
            animate.SetTrigger("Grab");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedWeapon = 2;
            animate.SetTrigger("Grab");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            selectedWeapon = 3;
            animate.SetTrigger("Grab");
        }

        SelectWeapon();

        if (selectedWeapon == 1 && Input.GetButtonDown("Fire2")){
            Scope = !Scope;
            animateScope.SetBool("Aiming", Scope);
            ScopeCam.fieldOfView = zoomCam;
        }

        if (selectedWeapon != 1 || !Scope) {
            Scope = false;
            ScopeCam.fieldOfView = 60;
        }
        Scoped();
    }
        
    void Scoped() {
        if(Scope){
                if (selectedWeapon == 1) { Weapon.transform.GetChild(selectedWeapon).GetComponentInChildren<MeshRenderer>().enabled = false;}
                else { Weapon.transform.GetChild(selectedWeapon).GetComponentInChildren<MeshRenderer>().enabled = true;}
            }
           else { Weapon.transform.GetChild(selectedWeapon).GetComponentInChildren<MeshRenderer>().enabled = true;}
        ScopeOverlay.SetActive(Scope);
    }


    void SelectWeapon() {
        int i = 0;

        foreach (Transform weapon in transform) {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
		}
	}
}
