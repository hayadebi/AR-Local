mergeInto(LibraryManager.library, {
  GetCurrentPosition: function () {
	navigator.geolocation.getCurrentPosition(
    function(position) {
      xy = position.coords.latitude + "," + position.coords.longitude;   
	  SendMessage('GPSManager', 'ShowLocation', xy); 
    });
  },
});