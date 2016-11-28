

function encrypt() {
    var reader = new FileReader();
    var hash = document.getElementById("hashField").value;
    var file = document.getElementById("filePosted").value;
    var binaryString;
    reader.onload = function () {
        var arrayBuffer = this.result,
        array = new Uint8Array(arrayBuffer),
        binaryString = String.fromCharCode.apply(null, array);
        console.log(binaryString);
    }
    reader.readAsArrayBuffer(document.getElementById("filePosted").files[0]);

    var encrypted = CryptoJS.Sha1(binaryString, hash);
    console.log(encrypted);

    //var encrypted = CryptoJS.SHA1(file, encryptionString);

    //var data = new FormData($("#fileinfo")[0]);
    //var encryptedFile = new File([encrypted], file.name + '.encrypted', { type: "text/plain", lastModified: new Date() });

    //$(document.getElementById("filePosted")).replaceWith(newfileControl);
}

function decrypt() {

}


jQuery(function () {
    $('input[name="rGroup"]').on('change', function () {
        $("#fileDetails1").text("File name: " + $('input[name="rGroup"]:checked').attr("data-name"));
        $("#fileDetails2").text("File size: " + $('input[name="rGroup"]:checked').attr("data-size") + " bytes");
    });
})
