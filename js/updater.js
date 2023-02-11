const bc = new BroadcastChannel('update-channel');
bc.onmessage = function (message) {
    if (message && message.data == "new-version-found") {
        bc.postMessage("skip-waiting");
    }
    if (message && message.data == "reload") {
        notifyNewVersion();
    }
    else if (message && message.data == "refresh") {
        window.location.reload();
    }
}
let dotnetObj;
window.Updater = {
    Initialize: function (interop) {
        dotnetObj = interop;        
    },
    Dispose: function () {

        if (dotnetObj != null) {
            dotnetObj.dispose();
        }
    }
}
function notifyNewVersion() {
    dotnetObj.invokeMethodAsync("Connection.StatusChanged", status);
}
function restartApp() {
    window.location.reload();
    localStorage.setItem("new-version", false);
}