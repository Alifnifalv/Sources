function BrowserNotifier() {
    // request permission on page load
    document.addEventListener('DOMContentLoaded', function () {
        if (Notification.permission !== "granted")
            Notification.requestPermission();
    });

    this.notifyMe = function(hosturl, message, username, title) {
        if (!Notification) {
            return;
        }

        if (title == undefined) {
            title = username + ' says.';
        }

        if (Notification.permission !== "granted")
            Notification.requestPermission();
        else {
            var notification = new Notification(title, {
                icon: hosturl + '/Images/blinkLogo.gif',
                body: message,
            });

            notification.onclick = function () {
                
            };

        }
    }
}