-INFO:
WEBGL NATIVE FILE BROWSER
CURRENT VERSION 1.3
POWERED BY FROSTWEEP GAMES
PROGRAMMER ARTEM SHYRIAIEV
LAST UPDATE JANUARY 07 2021

-PATCHLIST:
VERSION 1.0 - IMPLEMENTED WEBGL NATIVE FILE BROWSER
VERSION 1.1 - IMPROVED FILE BROWSER POPUP; ADDED FILTER BY EXTENSION
VERSION 1.2 - RESTRUCTURE. CODE UPDATE. ADDED FILE SAVE API (NOT SUPPORTED YET)
VERSION 1.3 - ADDED WEBGL TEMPALTE FOR UNITY 2020.x+

-CONTACTS:
SKYPE SATTELITE101
EMAIL ASSETS@FROSTWEEPGAMES.COM
OFFICIAL WEBSITE WWW.FROSTWEEPGAMES.COM
YOUTUBE https://www.youtube.com/user/FrostweepGames/
DISCORD https://discord.gg/TZdhnWy

-WARNING
for correct working of the plugin you need to add this code in your index.html page where uses webgl native file browser:


 <!-- BEGIN WEBGL FILE BROWSER LIB -->
    <form id="fileBrowserPopup" style="display: none;">
        <img src="TemplateData/2x2.png" style="position: absolute; width: 100%; height: 100%;" />
        <img src="TemplateData/White-Button.png" style="position: absolute;  top: 35%; left: 38%; width: 25%; height: 30%;" />

        <label for="fileToUpload">
            <img src="TemplateData/upload_button.png" style="position: absolute; top: 45%; left: 42.8%; width: 16%; height: 10%;" />
        </label>
        <input type="File" name="fileToUpload" id="fileToUpload" style="width: 0px; height: 0px;" onchange="sendfile(event);return false;" />

        <label for="closePopup">
            <img src="TemplateData/close_button.png" style="position: absolute; top: 57%; left: 48.0%; width: 5%; height: 5%;" />
        </label>
        <input type="button" name="closePopup" id="closePopup" style="width: 0px; height: 0px;" onclick="hidePopup()" />
    </form>

    <script type='text/javascript'>
        function sendfile(e) {
            var files = e.target.files;
            for (var i = 0, f; f = files[i]; i++) {
                var reader = new FileReader();
                reader.onload = (function (file) {
                    return function (e) {
                        gameInstance.SendMessage('FileBrowserDialogLib', 'ApplyFileName', escape(file.name));

                        (window.filedata = window.filedata ? window.filedata : {})[file.name] = e.target.result;
                        gameInstance.SendMessage("FileBrowserDialogLib", "FileUpload", file.name);
                    }
                })(f);
                reader.readAsArrayBuffer(f);
            }
        }
        function hidePopup() {
            gameInstance.SendMessage("FileBrowserDialogLib", "CloseFilePopupDialog");
        }
    </script>

    <script type='text/javascript'>
        function FilterOfTypes(types) {
            document.getElementsByName("fileToUpload")[0].accept = types;
        }
    </script>

    <!-- END WEBGL FILE BROWSER LIB -->

	
for Unity version >= 2020 you have to add changes like in this screenshot for your WEbGL template index.html: http://joxi.ru/brRjNbjHO4plqr
	
	