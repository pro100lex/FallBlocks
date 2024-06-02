mergeInto(LibraryManager.library, {
	ShowAdv : function(){
		ysdk.adv.showFullscreenAdv({
    		callbacks: {
        	onClose: function(wasShown) {
        		console.log("--------------- closed --------------")
          // some action after close
        },
        	onError: function(error) {
          // some action on error
        }
    	}
		})
	},

	GetLang : function(){
		var lang = ysdk.environment.i18n.lang;
		var bufferSize = lengthBytesUTF8(lang) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(lang, buffer, bufferSize);
		return buffer;
	}
});
