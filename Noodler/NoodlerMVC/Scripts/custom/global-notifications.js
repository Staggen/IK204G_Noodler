$(document).ready(() => {
    console.log("Script loaded: global-notifications.js");
    SetNumberOfNotifications();
    // There two calls are required to un-fuck the position of the notifications window.
    ShowNotificationsDiv();
    ShowNotificationsDiv();
});

$("#NotificationPopUpDiv").on("click", ".NotificationsAcceptBtn", NotificationsAcceptRequest);
$("#NotificationPopUpDiv").on("click", ".NotificationsDeclineBtn", NotificationsDeclineRequest);
$("#GlobalNotificationButton").on("click", ShowNotificationsDiv);

function ShowNotificationsDiv() {
    ToggleDisplay();

    var ref = $("#GlobalNotificationButton");
    var pop = $("#NotificationPopUpDiv");
    var popper = new Popper(ref, pop, {
        placement: "bottom",
        modifiers: {
            offset: {
                enabled: true,
                offset: "0, 10"
            }
        }
    });

    SetNumberOfNotifications();
    Update_Content();
}

function ToggleDisplay() {
    $("#NotificationPopUpDiv").toggleClass("d-none");
}

function NotificationsAcceptRequest() {
    var UserId = this.attributes[1].value;
    $.ajax({
        type: "POST",
        url: "/Friend/AcceptRequest/" + UserId,
        dataType: "JSON",
        success: () => {
            Update_Content();
            SetNumberOfNotifications();
            if ($("#NotificationNumberSpan").val() == 0) {
                ToggleDisplay();
            }
        },
        error: () => {
            alert("Error: Unable to accept friend request.");
        }
    });
}

function NotificationsDeclineRequest() {
    var UserId = this.attributes[1].value;
    $.ajax({
        type: "POST",
        url: "/Friend/DeclineRequest/" + UserId,
        dataType: "JSON",
        success: () => {
            Update_Content();
            SetNumberOfNotifications();
            if ($("#NotificationNumberSpan").val() == 0) {
                ToggleDisplay();
            }
        },
        error: () => {
            alert("Error: Unable to decline friend request.");
        }
    });
}

function Update_Content() {
    $.ajax({
        type: "GET",
        url: "/Notifications/GetFriendRequests/",
        contentType: "application/json;charset=UTF-8",
        dataType: "html",
        success: function (data) {
            $("#NotificationPopUpDiv").html(data);
        },
        error: () => {
            alert("Error: Unable to fetch and display friend requests");
        }
    });
}

function SetNumberOfNotifications() {
    $.ajax({
        type: "POST",
        url: "/Notifications/GetNumberOfNotifications/",
        dataType: "JSON",
        success: function (data) {
            $("#NotificationNumberSpan").html(data.Number); // Set number
            if (data.Number >= 1) { // Mess with colors of the buttons
                if ($("#GlobalNotificationButton").hasClass("btn-secondary")) {
                    $("#GlobalNotificationButton").removeClass("btn-secondary");
                }
                if (!$("#GlobalNotificationButton").hasClass("btn-primary")) {
                    $("#GlobalNotificationButton").addClass("btn-primary");
                }
            } else {
                if ($("#GlobalNotificationButton").hasClass("btn-primary")) {
                    $("#GlobalNotificationButton").removeClass("btn-primary");
                }
                if (!$("#GlobalNotificationButton").hasClass("btn-secondary")) {
                    $("#GlobalNotificationButton").addClass("btn-secondary");
                }
            }
        },
        error: () => {
            alert("Error: Unable to fetch and display number of notifications");
        }
    });
}