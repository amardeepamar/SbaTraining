// Custom client-side validation for mobile number
$("#ContactNo").submit(function (event) {
    var mobileNumber = $("#ContactNo").val();
    var regex = /^\d{10}$/;

    if (!regex.test(mobileNumber)) {
        event.preventDefault();
        alert("Please enter a valid 10-digit mobile number.");
    }
});

// Custom email validation using jQuery
$("#Email").submit(function (event) {
    var email = $("#Email").val();
    var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;

    if (!regex.test(email)) {
        event.preventDefault();  // Prevent form submission
        alert("Please enter a valid email address.");
    }
});
// Custom client-side validation for mobile number in Employee Information
$("#MobileNumber").submit(function (event) {
    var mobileNumber = $("#MobileNumber").val();
    var regex = /^\d{10}$/;

    if (!regex.test(mobileNumber)) {
        event.preventDefault();
        alert("Please enter a valid 10-digit mobile number.");
    }
});





