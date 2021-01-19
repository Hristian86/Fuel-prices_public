// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#button").click(function () {
    $([document.documentElement, document.body]).animate({
        scrollTop: $("#elementtoScrollToID").offset().top
    }, 2000);
});

const error = document.getElementById('errorField');
const upVote = async (postId) => {
    var json = {
        postId: postId,
        isUpVote: true
    };

    var token = $("#votesForm input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/api/votes",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            $("#votesCount").html(data.votesCount);

            let up = document.getElementById("upVoteIcont");
            up.setAttribute("class", "fa fa-thumbs-up text-warning hover-button-type");

            let down = document.getElementById("downVoteIcont");

            down.setAttribute("class", "fa fa-thumbs-down text-primary hover-button-type");
            error.innerHTML = "";
        },
        error: function (err) {
            error.innerHTML = "Must be logged";
        }
    })

}
const downVote = (postId) => {
    var json = {
        postId: postId,
        isUpVote: false
    };

    var token = $("#votesForm input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: "/api/votes",
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: { 'X-CSRF-TOKEN': token },
        success: function (data) {
            //    $("#votesCount").html("0");
            //} else {
            $("#votesCount").html(data.votesCount);

            document.getElementById("upVoteIcont").setAttribute("class", "fa fa-thumbs-up text-primary hover-button-type");

            document.getElementById("downVoteIcont").setAttribute("class", "fa fa-thumbs-down text-warning hover-button-type");
            error.innerHTML = "";
        },
        error: function (err) {
            error.innerHTML = "Must be logged";
        }
    })
}


//const submitEmail = (e) => {
//    e.preventDefault();
//    const name = e.target.name.value;
//    const email = e.target.email.value;
//    const subject = e.target.subject.value;
//    const content = e.target.content.value;

//    console.log(name);
//    console.log(email);
//    console.log(subject);
//    console.log(content);
//}


var sendBool = false;

const form = document.getElementById("contact-form-prev");
const errorSuccess = document.getElementById("errorSuccess");
const buttonSuccess = document.getElementById("buttonSuccess");
const loading = document.getElementById("loading");

form.addEventListener("submit", function (e) {
    formValid(e);
});

const formValid = async (e) => {
    e.preventDefault();

    buttonSuccess.style.display = "none";
    loading.style.display = "flex";

    const name = e.target.name.value;
    const email = e.target.email.value;
    const subject = e.target.subject.value;
    const content = e.target.content.value;
    const allErrors = ["Name", "Email", "Subject", "Content"];

    for (var a = 0; a < allErrors.length; a++) {
        document.getElementById("error" + allErrors[a]).innerHTML = "";
    }

    // Send email function.
    if (!sendBool) {

        const data = await sendEmail(name, email, subject, content);

        //console.log(await data);

        if (await data.message.length > 0 && (await data.statusCode >= 200 && await data.statusCode < 400)) {

            // On success.
            errorSuccess.innerHTML = "Email send successfully.";
            sendBool = true;
            loading.style.display = "none";
            buttonSuccess.style.display = "none";

            const a = document.getElementById("fname");

            a.innerText = "";

            document.getElementById("email").innerHTML = "";
            document.getElementById("comment").innerHTML = "";
            document.getElementById("subject").innerHTML = "";



        } else if(await data.errors && (await data.statusCode < 500 && await data.statusCode >= 400)) {

            buttonSuccess.style.display = "flex";

            // On error.
            errorSuccess.innerHTML = "";
            const d = Object.entries(data["errors"]);

            // Auto fill the error fields.
            for (var i = 0; i < d.length; i++) {

                // Each error field is populated.
                document.getElementById("error" + d[i][0]).innerHTML = d[i][1];
            }

            loading.style.display = "none";
        } else if (await data.errors && await data.statusCode >= 500) {
            loading.style.display = "none";

            console.log(data.errors[0]);

            errorSuccess.setAttribute("class", "text-danger ml-2");
            errorSuccess.innerHTML = "Server error";

        } else {
            loading.style.display = "none";
            errorSuccess.setAttribute("class", "text-danger ml-2");
            errorSuccess.innerHTML = "Server error";
        }
    }
}

const sendEmail = async (name, email, subject, content) => {
    const payload = {
        name,
        email,
        subject,
        content
    }

    const data = await fetch("/api/contact", {
        "method": "POST",
        "headers": {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }, body: JSON.stringify(payload)
    }).then(data => data.json())
        .catch(err => console.log(err))

    return data;
}