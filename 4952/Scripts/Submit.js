function upload() {
    var reader = new FileReader();
    reader.onload = function (e) {
        var encrypted = CryptoJS.AES.encrypt(e.target.result, document.getElementById("hashField").value);
        var filePath = document.getElementById("filePosted").value;
        var fileName = filePath.substring(filePath.lastIndexOf('\\') + 1);
        document.getElementById("fileData").value = encrypted;
        document.getElementById("fileName").value = fileName;
        document.getElementById("uploadForm").submit();
    }
    var file = document.getElementById("filePosted").files[0];
    reader.readAsDataURL(file);
}

function download() {
    $.ajax({
        url: "/Home/DownloadFile/" + $("input[name='rGroup']:checked").val(),
        success: function (result) {
            try {
                var decrypted = CryptoJS.AES.decrypt(result.data, document.getElementById("hashField").value).toString(CryptoJS.enc.Utf8);
                var link = document.createElement("a");
                link.download = result.name;
                link.href = decrypted;
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            }
            catch (err) {
                alert("File decryption failed");
            }
        }
    });
}

function deleteFile() {
    window.location.href = "/Home/DeleteFile/" + $("input[name='rGroup']:checked").val();
}

jQuery(function () {
    $('input[name="rGroup"]').on('change', function () {
        var fileName = $('input[name="rGroup"]:checked').attr("data-name");

        if (fileName !== undefined) {
            $("#fileDetails1").text("File name: " + fileName);
            $("#fileDetails2").text("File size: " + $('input[name="rGroup"]:checked').attr("data-size") + " bytes");
        } else {
            $("#fileDetails1").text("No file selected");
            $("#fileDetails2").text("");
        }
    });
})
