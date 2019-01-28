$(document).ready(() => {
    console.log("Script loaded: profile-friends.js");
});

$("#FriendListDiv").on("click", ".friendListCategory", SelectDropdownItem); // Dropdown item
$("#FriendListDiv").on("click", "#InputFriendCategory", ShowFriendCategoryList); // Dropdown list
$("#FriendListDiv").on("mouseleave", "#FriendCategoryDropdownList", ShowFriendCategoryList); // Leave dropdown list
$("#FriendListDiv").on("click", "#FriendCategoryAddBtn", AddFriendCategory);
$("#FriendListDiv").on("click", "#FriendCategorySaveBtn", SaveFriendCategory);
$("#FriendListDiv").on("click", "#FriendCategoryDeleteBtn", DeleteFriendCategory);
$("#FriendListDiv").on("click", ".removeFriendBtn", RemoveFriend);
$("#FriendListDiv").on("click", ".friendCategoryBtn", ShowFriendCategoryDiv); // Friend category div


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
    var friendId = this.getAttribute("data-friend-id");
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

function ShowFriendCategoryList() {
    $("#FriendCategoryDropdownList").toggleClass("d-none");
    ref = $("#InputFriendCategory");
    pop = $("#FriendCategoryDropdownList");
    new Popper(ref, pop, {
        placement: 'bottom'
    });
}

function SelectDropdownItem() {
    var FriendCategoryName = this.childNodes[0].data;
    var FriendshipId = this.getAttribute("data-friendship-id");
    var CategoryId = this.getAttribute("data-category-id");
    $("#InputFriendCategory").val(FriendCategoryName);
    SetFriendCategory(FriendshipId, CategoryId);
}

function AddFriendCategory() {
    var category = { CategoryName: $("#InputFriendCategory").val() };
    $.ajax({
        url: "/api/CategoryApi/Add/" + category,
        type: "POST",
        data: JSON.stringify(category),
        contentType: "application/json;charset=UTF-8",
        success: () => {
            Update_Friends();
            console.log("AddFriendCategory() => Success");
        },
        error: () => {
            console.log("Error: Unable to add friend category.");
        }
    });
}

function SaveFriendCategory() {
    var category = { Id: $("#InputFriendCategory").attr("data-category-id"), CategoryName: $("#InputFriendCategory").val() };
    $.ajax({
        url: "/api/CategoryApi/Edit/" + category,
        type: "POST",
        data: JSON.stringify(category),
        contentType: "application/json;charset=UTF-8",
        success: () => {
            Update_Friends();
            console.log("SaveFriendCategory() => Success");
        },
        error: () => {
            console.log("Error: Unable to edit friend category.");
        }
    });
}

function DeleteFriendCategory() {
    var categoryId = $("#InputFriendCategory").attr("data-category-id");

    $.ajax({
        url: "/api/CategoryApi/Delete/" + categoryId,
        type: "DELETE",
        contentType: "application/json;charset=UTF-8",
        success: () => {
            Update_Friends();
            console.log("DeleteFriendCategory() => Success");
        },
        error: () => {
            console.log("Error: Failure to delete friend category");
        }
    });
}

function SetFriendCategory(Id, FriendCategory) {
    var model = { Id: Id, FriendCategory: FriendCategory };
    $.ajax({
        url: "/Friend/SetFriendCategory/" + model,
        type: "POST",
        data: JSON.stringify(model),
        contentType: "application/json;charset=UTF-8",
        success: () => {
            console.log("SetFriendCategory() => Success");
            Update_Friends();
        },
        error: () => {
            console.log("Error: Unable to set friend category.");
        }
    });
}