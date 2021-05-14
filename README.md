# UnityAsSystemdService
Running unity application as Systemd service with sd_notify to make sure watchdog catches "not responding" states.

## Usage
1. Add the script unity/SystemdNotifier.cs to unity project, and add it to a scene.

2. Build the app.

3. In Linux: copy and modify the service from linux/unity_app.service to /etc/systemd/user/ (dont forget to change the ExecStart path)

4. systemctl --user enable unity_app.service

### Notes
If the .service file is modified:

systemctl --user daemon-reload

systemctl --user restart unity_app.service