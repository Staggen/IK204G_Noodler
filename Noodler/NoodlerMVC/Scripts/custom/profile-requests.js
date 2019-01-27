$(document).ready(() => {
    console.log("Script loaded: profile-requests.js");
});

$("#FriendBtn").on("click", HandleRequest);
$("#AcceptBtn").on("click", NotificationsAcceptRequest);
$("#DeclineBtn").on("click", NotificationsDeclineRequest);

function HandleRequest() {
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/Profile/Index/");
    var Id = urlArray[1];
    if ($("#FriendBtn").text() == "Cancel Friend Request") {
        // Cancel Friend Request
        $.ajax({
            url: "/Friend/CancelRequest/" + Id,
            type: "POST",
            dataType: "JSON",
            success: function (data) {
                if (data.Result) {
                    ButtonGroupNotFriends();
                }
                console.log("HandleRequest => (Cancel Request) Success");
            },
            error: () => {
                console.log("HandleRequest => (Cancel Request) Error");
            }
        });
    }
    if ($("#FriendBtn").text() == "Remove Friend") {
        // Remove Friend
        $.ajax({
            url: "/Friend/RemoveFriend/" + Id,
            type: "POST",
            dataType: "JSON",
            success: function (data) {
                if (data.Result) {
                    ButtonGroupNotFriends();
                }
                console.log("HandleRequest() => (Remove Friend) Success");
            },
            error: () => {
                console.log("HandleRequest() => (Remove Friend) Error");
            }
        });
    }
    if ($("#FriendBtn").text() == "Send Friend Request") {
        // Send Friend Request
        $.ajax({
            url: "/Friend/SendRequest/" + Id,
            type: "POST",
            dataType: "JSON",
            success: function (data) {
                if (data.Result) {
                    ButtonGroupOutgoing();
                }
                console.log("HandleRequest() => (Send Request) Success");
            },
            error: () => {
                console.log("HandleRequest() => (Send Request) Error");
            }
        });
    }
    if ($("#FriendBtn").text() == "Incoming Friend Request") {
        // This situation is solved with the other two buttons, AcceptBtn and DeclineBtn
        console.log("HandleRequest() => Incoming Friend Request");
    }
}

function NotificationsAcceptRequest() {
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/Profile/Index/");
    var Id = urlArray[1];
    $.ajax({
        url: "/Friend/AcceptRequest/" + Id,
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            if (data.Result) {
                ButtonGroupFriends();
                Update_Friends();
                Update_Content();
            }
            console.log("AcceptRequest() => Success");
        },
        error: () => {
            console.log("AcceptRequest() => Error");
        }
    });
}

function NotificationsDeclineRequest() {
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/Profile/Index/");
    var Id = urlArray[1];
    $.ajax({
        url: "/Friend/DeclineRequest/" + Id,
        type: "POST",
        dataType: "JSON",
        success: function (data) {
            if (data.Result) {
                Update_Content();
                ButtonGroupNotFriends();
            }
            console.log("DeclineRequest() => Success");
        },
        error: () => {
            console.log("DeclineRequest() => Error");
        }
    });
}

function ButtonGroupNotFriends() {
    if (!$("#RequestBtnGroup").hasClass("d-none")) {
        $("#RequestBtnGroup").addClass("d-none");
    }

    $("#FriendBtn").text("Send Friend Request");
    if ($("#FriendBtn").hasClass("btn-danger")) {
        $("#FriendBtn").removeClass("btn-danger");
    }
    if ($("#FriendBtn").hasClass("btn-warning")) {
        $("#FriendBtn").removeClass("btn-warning");
    }
    if (!$("#FriendBtn").hasClass("btn-info")) {
        $("#FriendBtn").addClass("btn-info");
    }
}

function ButtonGroupFriends() {
    if (!$("#RequestBtnGroup").hasClass("d-none")) {
        $("#RequestBtnGroup").addClass("d-none");
    }

    $("#FriendBtn").text("Remove Friend");
    if ($("#FriendBtn").hasClass("btn-info")) {
        $("#FriendBtn").removeClass("btn-info");
    }
    if ($("#FriendBtn").hasClass("btn-warning")) {
        $("#FriendBtn").removeClass("btn-warning");
    }
    if (!$("#FriendBtn").hasClass("btn-danger")) {
        $("#FriendBtn").addClass("btn-danger");
    }
}

function ButtonGroupIncoming() {
    if ($("#RequestBtnGroup").hasClass("d-none")) {
        $("#RequestBtnGroup").removeClass("d-none");
    }

    $("#FriendBtn").text("Incoming Friend Request");
    if ($("#FriendBtn").hasClass("btn-danger")) {
        $("#FriendBtn").removeClass("btn-danger");
    }
    if ($("#FriendBtn").hasClass("btn-warning")) {
        $("#FriendBtn").removeClass("btn-warning");
    }
    if (!$("#FriendBtn").hasClass("btn-info")) {
        $("#FriendBtn").addClass("btn-info");
    }
}

function ButtonGroupOutgoing() {
    if (!$("#RequestBtnGroup").hasClass("d-none")) {
        $("#RequestBtnGroup").addClass("d-none");
    }

    $("#FriendBtn").text("Cancel Friend Request");
    if ($("#FriendBtn").hasClass("btn-info")) {
        $("#FriendBtn").removeClass("btn-info");
    }
    if ($("#FriendBtn").hasClass("btn-danger")) {
        $("#FriendBtn").removeClass("btn-danger");
    }
    if (!$("#FriendBtn").hasClass("btn-warning")) {
        $("#FriendBtn").addClass("btn-warning");
    }
}