# README: Removing, Troubleshooting, and Blocking Snap on Ubuntu

This guide walks you through the steps to **completely remove Snap** from an Ubuntu system, **troubleshoot any issues** that may arise during the process, and **block Snap from being reinstalled** in the future.

## Table of Contents

1.  [Removing Snap](https://chatgpt.com/c/674463a0-5634-8009-9eff-a49aa8aee43a#1-removing-snap)
2.  [Troubleshooting](https://chatgpt.com/c/674463a0-5634-8009-9eff-a49aa8aee43a#2-troubleshooting)
3.  [Blocking Snap from Future Installations](https://chatgpt.com/c/674463a0-5634-8009-9eff-a49aa8aee43a#3-blocking-snap-from-future-installations)
4.  [Conclusion](https://chatgpt.com/c/674463a0-5634-8009-9eff-a49aa8aee43a#4-conclusion)

----------

## 1. Removing Snap

### Step 1: Remove `snapd` and Related Packages

To completely remove Snap and its associated services from your system, you can purge the `snapd` package and remove any residual data:

```bash
sudo apt-get purge --auto-remove snapd

```

### Step 2: Delete Snap Directories

Snap may leave behind residual directories even after `snapd` is removed. Delete these directories to fully remove Snap from the system:

```bash
sudo rm -rf /var/snap /var/lib/snapd /snap /etc/systemd/system/snapd.service /etc/systemd/system/snapd.socket

```

### Step 3: Remove Snap-related Services

Disable and stop the Snap-related system services to prevent Snap from running in the background:

```bash
sudo systemctl stop snapd.service
sudo systemctl stop snapd.socket
sudo systemctl disable snapd.service
sudo systemctl disable snapd.socket

```

### Step 4: Remove Any Remaining Snap Applications

If any Snap applications are installed (e.g., Firefox via Snap), remove them manually:

```bash
sudo rm -rf /var/snap/firefox/

```

----------

## 2. Troubleshooting

### Issue: Can't Remove Snap Due to "Device or Resource Busy"

If you encounter the following error while trying to remove Snap packages:

```
rm: cannot remove '/var/snap/firefox/common/host-hunspell': Device or resource busy

```

This could be because the Snap process is still running. To fix this:

1.  **Identify and Kill Snap-related Processes:** Find any running Snap processes using:
    
    ```bash
    ps aux | grep snap
    
    ```
    
    Kill these processes by using the `kill` command with the appropriate process ID (PID):
    
    ```bash
    sudo kill <PID>
    
    ```
    
2.  **Unmount Snap Folders (if needed):** If the error persists, you can try unmounting Snap folders:
    
    ```bash
    sudo umount /var/snap
    sudo umount /snap
    
    ```
    
3.  **Retry Removal:** After stopping the processes and unmounting the directories, retry removing the Snap folder:
    
    ```bash
    sudo rm -rf /var/snap/firefox/
    
    ```
    

### Issue: Unable to Mark `snapd` as Held

If you attempt to mark `snapd` as held using `apt-mark` and see an error like:

```
E: Can't select installed nor candidate version from package 'snapd' as it has neither of them

```

Ensure `snapd` is already removed from your system. You can try purging it again and making sure that no Snap-related processes are running.

----------

## 3. Blocking Snap from Future Installations

### Step 1: Hold `snapd` and Related Packages

After removing Snap, prevent the `snapd` package from being installed in the future by using the `apt-mark` command:

```bash
sudo apt-mark hold snapd
sudo apt-mark hold snapd.seeded

```

### Step 2: Block Snap via APT Preferences

To ensure Snap is not installed automatically through APT in the future, create a preferences file to block Snap-related packages:

1.  Create the file `/etc/apt/preferences.d/block-snapd`:
    
    ```bash
    sudo nano /etc/apt/preferences.d/block-snapd
    
    ```
    
2.  Add the following content to block `snapd`:
    
    ```
    Package: snapd
    Pin: release *
    Pin-Priority: -1
    
    ```
    
    This configuration will prevent the installation of Snap via APT by giving `snapd` a low priority.
    

### Step 3: Prevent Automatic Snap Installations

Some applications may try to install Snap packages (e.g., Chromium). To prevent this, make sure you install traditional (non-Snap) versions of apps whenever possible:

```bash
sudo apt install chromium-browser  # Install traditional Chromium, not Snap

```

### Step 4: Clean Up APT Cache

After blocking Snap, clean the APT cache to remove any leftover Snap packages or metadata:

```bash
sudo apt clean
sudo apt autoremove

```

----------

## 4. Conclusion

By following these steps, you should be able to **completely remove Snap** from your Ubuntu system, **troubleshoot any Snap-related issues** that arise during the process, and **block Snap from being reinstalled** in the future.

If you encounter any other issues or need further assistance, feel free to reach out for more support.