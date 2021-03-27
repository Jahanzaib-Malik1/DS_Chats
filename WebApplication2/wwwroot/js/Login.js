

$("a").click(function (event) {
    event.preventDefault();
});


$('#signupBtn').on('click', (event) => {
    event.preventDefault();
    var fileUpload = $("#image").get(0);
    var files = fileUpload.files;
    
    
    var formData = new FormData();
    formData.append("Image", files[0]);
    formData.append("Email", $('#Email').val());
    formData.append("Password", $('#Password').val());
    formData.append("UserName", $('#Name').val());
    $.ajax(
        {
            method: 'POST',
            url: '/Home/SignUp',
            data: formData,
            processData: false,
            contentType: false,
        }).then(resp => {
            if (resp.status) {
                alert("Signed Up Successfully ")
                location.reload()
            } else {
                alert("SomethingWent Wrong")
            }

        })

});
$('#loginBtn').on('click', (event) => {
    //var fileUpload = $("#image").get(0);
    //var files = fileUpload.files;
    event.preventDefault();
    var formData = new FormData();
    //formData.append("Image", files[0]);
    formData.append("Email", $('#LEmail').val());
    formData.append("Password", $('#LPassword').val());
    debugger;
    $.ajax(
        {
            method: 'POST',
            url: '/Home/Login',
            data: formData,
            processData: false,
            contentType: false,
            success: resp => {
              
                    if (resp.status) {
                        console.log(resp)
                        localStorage.setItem("UserId", resp.data.userId);
                        localStorage.setItem("UserName", resp.data.userName);
                        //debugger;
                        window.location.replace("/Home/Index")
                    } else {
                        alert("Email Or Password Wrong");
                    }


            },
            error: err => { console.log(err) }
        })

});