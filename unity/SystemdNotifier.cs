using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using System;
using System.Runtime.InteropServices;

public class SystemdNotifier : MonoBehaviour
{
    [SerializeField]
    private float _notifyInterval = 7.5f;

    private float _timer = 0;

    public static class Systemd
    {
        [DllImport("libsystemd.so.0")]
        public static extern int sd_notify(int unset_environment, string state);
    }

    private int SDNotify(int env, string val)
    {
        int res = 0;
#if UNITY_STANDALONE_LINUX && !UNITY_EDITOR
        res = Systemd.sd_notify(env, val);
#endif
        return res;
    }

    private void Awake()
    {
        // notify systemd for READY state
        int res = SDNotify(0, "READY=1");
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _notifyInterval)
        {
            // keepalive / ping for systemd
            int res = SDNotify(0, "WATCHDOG=1");
            _timer = 0;
        }
    }
}
