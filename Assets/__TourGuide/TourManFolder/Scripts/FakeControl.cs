using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeControl : BasePlayerControl
{

    private GameObject _followObject;

    private Quaternion _newRotate;


    protected override void Start()
    {
        base.Start();
        anim.SetInteger("AnimStatus", 1);
        _followObject = transform.parent.GetChild(0).gameObject;
        speed = 5;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //distance = Vector3.Distance(transform.position, followObject.transform.position);

        transform.LookAt(_followObject.transform);


        _newRotate = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
        transform.localRotation = _newRotate;
    }
}
