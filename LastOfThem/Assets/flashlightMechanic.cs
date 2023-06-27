using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class flashlightMechanic : MonoBehaviour
{
    public bool _isOn;
    public Transform _flashlight;
    public Slider _batteryLife;
    public int _batteries;
    public TMP_Text _text;
    public int _radius;
    public LayerMask _batteryLayer;
    public RaycastHit _hit;
    public Transform _cam;
    private bool _inRange;
    public Transform _UI;
    // Start is called before the first frame update
    void Start()
    {
        _batteries = 1;
        _batteryLife.maxValue = 100;
        _batteryLife.minValue = 0;
        _batteryLife.value = _batteryLife.maxValue;
        
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = _batteries.ToString();
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_flashlight.transform.gameObject.activeInHierarchy || _batteryLife.value <= 0)
            {
                _isOn = false;
            }
            else if (!_flashlight.transform.gameObject.activeInHierarchy)
            {
                _isOn = true;
            }
        }
        
        if (_isOn)
        {
            _flashlight.transform.gameObject.SetActive(true);
            _batteryLife.value -= Time.deltaTime;
        }
        else
        {
            _flashlight.transform.gameObject.SetActive(false);
        }

        if (_batteryLife.value <= 0)
        {
            _isOn = false;
        }

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out _hit, _radius, _batteryLayer))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _batteries++;
                Destroy(_hit.transform.gameObject);
                _UI = _hit.transform.GetChild(3);
                _UI.transform.gameObject.SetActive(true);
            }
            else
            {
                _UI = null;
            }
        }
        _reloadFlash();
        


    }

    public void _reloadFlash()
    {
        if (_batteries > 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _batteries--;
                _batteryLife.value = 300;
            }
        }
    }

   
   
}
