    var NativeFileBrowser = {
       Alert: function(msgptr) {
         window.alert(Pointer_stringify(msgptr));
       },
       GetFileData: function(filenameptr) {
         var filename = Pointer_stringify(filenameptr);
         var filedata = window.filedata[filename];
         var ptr = (window.fileptr = window.fileptr ? window.fileptr : {})[filename] = _malloc(filedata.byteLength);
         var dataHeap = new Uint8Array(HEAPU8.buffer, ptr, filedata.byteLength);
         dataHeap.set(new Uint8Array(filedata));
         return ptr;
       },
       GetFileDataLength: function(filenameptr) {
         var filename = Pointer_stringify(filenameptr);
         return window.filedata[filename].byteLength;
       },
       FreeFileData: function(filenameptr) {
         var filename = Pointer_stringify(filenameptr);
         _free(window.fileptr[filename]);
         delete window.fileptr[filename];
         delete window.filedata[filename];

		 gameInstance.browserPopup = null;
       },
	    OpenFilePopup: function(filenameptr) {
		var filename = Pointer_stringify(filenameptr);
		FilterOfTypes(filename);
		gameInstance.browserPopup = document.getElementById("fileBrowserPopup");
		gameInstance.browserPopup.style.display = "block";
	   },
	    HideFilePopup: function() {	
		 gameInstance.browserPopup = document.getElementById("fileBrowserPopup");
		 gameInstance.browserPopup.style.display = "none";
	   },

       SaveFile: function(filename, text) {

              var element = document.createElement('a');
              element.setAttribute('href', 'data:text/plain;charset=x-user-defined,' + encodeURIComponent(Pointer_stringify(text)));
              element.setAttribute('download', Pointer_stringify(filename));

              element.style.display = 'none';
              document.body.appendChild(element);

              element.click();

              document.body.removeChild(element);
       
       }
    };
     
    mergeInto(LibraryManager.library, NativeFileBrowser);