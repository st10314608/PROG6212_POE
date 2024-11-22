$(document).ready(function () {
    $("#lecturerForm").on("submit", function (e) {
        let email = $("#Email").val();
        if (!email.includes("@")) {
            alert("Please enter a valid email.");
            e.preventDefault();
        }
    });
});
