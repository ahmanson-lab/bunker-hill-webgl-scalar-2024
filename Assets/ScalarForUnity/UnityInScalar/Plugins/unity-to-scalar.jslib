mergeInto(LibraryManager.library, {
	
	ReturnPosition3D: function(str) {
		console.log("ReturnPosition3D "+UTF8ToString(str));
		window.parent.postMessage(UTF8ToString(str), window.location);
	}

});