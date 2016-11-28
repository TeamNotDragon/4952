function encrypt() {
    var reader = new FileReader();

    reader.onload = function (e) {
        var decrypted = reader.result;
        var key = document.getElementById("hashField").value;
        var encrypted = CryptoJS.AES.encrypt(decrypted, key);
        document.getElementById("fileData").value = encrypted;

        var filePath = document.getElementById("filePosted").value;
        var fileName = filePath.substring(filePath.lastIndexOf('\\') + 1);
        document.getElementById("fileName").value = fileName;

        document.getElementById("fileSubmit").click();
    }

    var file = document.getElementById("filePosted").files[0];
    reader.readAsText(file);
}

function download() {
    var element = document.createElement('a');

    var encrypted = document.getElementById("fileData").value;
    var key = document.getElementById("hashField").value;
    var decrypted = CryptoJS.AES.decrypt(encrypted, key).toString(CryptoJS.enc.Utf8);
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(decrypted));

    var fileName = document.getElementById("fileName").value;
    element.setAttribute('download', fileName);

    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
}