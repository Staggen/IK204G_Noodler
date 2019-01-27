$(document).ready(() => {
    console.log("Script loaded: profile-friends.js");
});

// ADD FUNCTIONALITY FOR THE DAMN BUTTONS IN THE FRIEND CATEGORY POPUP DIV, plus you might wish to style them a touch as well...

$("#FriendListDiv").on("click", ".removeFriendBtn", RemoveFriend);
$("#FriendListDiv").on("click", ".friendCategoryBtn", ShowFriendCategoryDiv);

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

function ToggleFriendCategoryPopUpDivDisplay() {
    $("#FriendCategoryPopUpDiv").toggleClass("d-none");
}

function ShowFriendCategoryDiv() {
    ToggleFriendCategoryPopUpDivDisplay();
    var ref = this;
    var pop = $("#FriendCategoryPopUpDiv");
    var friendId = this.getAttribute("data-id");
    var serviceUrl = "/Friend/LoadFriendCategoryDiv/" + friendId;
    var request = $.post(serviceUrl);
    request.done(function (data) {
        new Popper(ref, pop, {
            placement: 'right',
            modifiers: {
                offset: {
                    enabled: true,
                    offset: "0, 10"
                }
            }
        });
        $("#FriendCategoryPopUpDiv").html(data);
    }).fail(() => {
        console.log("Error: Failure to load popup window");
    });
}