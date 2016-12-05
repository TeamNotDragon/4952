window.onload = function () {
    document.getElementById("myNav").hidden = "hidden";
    var hashField = document.getElementById("hashField");
    if (sessionStorage.getItem("hashField")) {
        hashField.value = sessionStorage.getItem("hashField");
    }
    hashField.addEventListener("keyup", function () {
        sessionStorage.setItem("hashField", hashField.value);
    });
}

function upload() {
    var reader = new FileReader();
    reader.onload = function (e) {
        var encrypted = CryptoJS.AES.encrypt(e.target.result, document.getElementById("hashField").value);
        var filePath = document.getElementById("choose_file").value;
        var fileName = filePath.substring(filePath.lastIndexOf('\\') + 1);
        $.ajax({
            type: 'POST',
            url: '/Home/UploadFile/',
            data: JSON.stringify({ name: fileName, data: encrypted.toString() }),
            dataType: "json",
            contentType: "application/json",
            success: function () {
                location.reload();
            }
        });
    }
    var file = document.getElementById("choose_file").files[0];
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

function logout() {
    sessionStorage.clear();
    window.location.href = "/";
}

jQuery(function () {
    $('input[name="rGroup"]').on('change', function () {
        var fileName = $('input[name="rGroup"]:checked').attr("data-name");
        if (fileName !== undefined) {
            document.getElementById("downloadButton").className = "btn btn-success";
            document.getElementById("deleteButton").className = "btn btn-danger";
            $("#fileDetails1").text("File name: " + fileName);
            $("#fileDetails2").text("File size: " + $('input[name="rGroup"]:checked').attr("data-size") + " bytes");
        } else {
            document.getElementById("downloadButton").className = "btn";
            document.getElementById("deleteButton").className = "btn";
            $("#fileDetails1").text("No file selected");
            $("#fileDetails2").text("");
        }
    });
    $('input[name="filePosted"]').on('change', function () {
        if ($(this).val() != null) {
            document.getElementById("Upload").className = "btn btn-success btn-lg";
        } else {
            document.getElementById("Upload").className = "btn btn-lg";
        }
    });
})
