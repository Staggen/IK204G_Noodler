$(document).ready(() => {
    console.log("Script loaded: profile-friends.js");
});

$("#FriendListDiv").on("click", ".removeFriendBtn", RemoveFriend);

function RemoveFriend() {
    var Id = this.getAttribute("data-friend-id");
    $.ajax({
        url: "/Friend/RemoveFriend/" + Id,
        type: "POST",
        dataType: "JSON",
        success: () => {
            Update_Friends();
        },
        error: () => {
            alert("Error: Failure to remove friend");
        }
    });
    if ($("body").hasClass("modal-open")) {
        $("body").toggleClass("modal-open");
    }
}

function Update_Friends() {
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/Profile/Index/");
    var Id = urlArray[1];

    var serviceUrl = "/Profile/UpdateFriendList/" + Id;
    var request = $.post(serviceUrl);
    request.done(function (data) {
        $("#FriendListDiv").html(data);
    }).fail(() => {
        console.log("Error: Failure to update friend list");
    });
}