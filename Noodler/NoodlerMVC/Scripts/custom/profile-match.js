$(document).ready(() => {
    console.log("Script loaded: profile-match.js");
    ShowMatchPopUpDiv();
    ShowMatchPopUpDiv();
});

$("#DoWeMatchBtn").on("click", ShowMatchPopUpDiv);
$("#DoWeMatchBtn").on("focusout", ShowMatchPopUpDiv);

function ShowMatchPopUpDiv() {
    $("#MatchPopUpDiv").toggleClass("d-none");
    var ref = $("#DoWeMatchBtn");
    var pop = $("#MatchPopUpDiv");
    new Popper(ref, pop, {
        placement: "right",
        modifiers: {
            offset: {
                enabled: true,
                offset: "0, 10"
            }
        }
    });
    UpdateMatchPercentage();
}

function UpdateMatchPercentage() {
    var currentUrl = window.location.href;
    var urlArray = currentUrl.split("/Profile/Index/");
    var profileId = urlArray[1];
    
    $.ajax({
        url: "/Search/GetMatchPartial?profileId=" + profileId,
        type: "POST",
        contentType: "application/json;charset=UTF-8",
        dataType: "html",
        success: function (data) {
            $("#MatchPopUpDiv").html(data);
            console.log("UpdateMatchPercentage() => Success");
        },
        error: () => {
            console.log("Error: Unable to fetch match data.");
        }
    });
}