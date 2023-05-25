mergeInto(LibraryManager.library, {
    WatchDeviceorientation: function () {
            window.addEventListener("deviceorientation", function(event) { 
            xy = event.alpha + "," + event.beta + "," + event.gamma; 
            SendMessage('GPSManager', 'ShowRotation', xy); 
        });
    }, 
});