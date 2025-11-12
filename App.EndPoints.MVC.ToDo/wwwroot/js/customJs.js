function OpenAvatarInput() {
    $("#UserAvatar").click();
}

function UploadUserAvatar(url) {
    var avatarInput = document.getElementById("UserAvatar");

    if (avatarInput.files.length) {
        var file = avatarInput.files[0];
        var formData = new FormData();
        formData.append("userAvatar", file);
        $.ajax({
            url: url,
            type: "post",
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () { },
            success: function (response) {
                if (response.status === "Success") {
                    location.reload();
                } else {
                    console.log("Error");
                }
            },
            error: function () { }
        });
    }
}

