using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUIRotate : MonoBehaviour
{
    private void LateUpdate()
    {
        //�@�J�����Ɠ��������ɐݒ�
        transform.rotation = Camera.main.transform.rotation;
    }
}
