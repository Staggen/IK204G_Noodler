$(document).ready(() => {
    console.log("Script loaded: profile-visitors.js");
    GetVisitors();
});

function GetVisitors() {
    $.ajax({
        type: "GET",
        url: "/api/AjaxApi/",
        success: function (data) {
            var listOfVisitors = "";
            $.each(data, function (i, item) {
                listOfVisitors += '<a href="/Profile/Index/' + item.VisitFromId + '" class="list-group-item list-group-item-action"><strong>' + (i+1) + '.</strong> ' + item.VisitFrom.FirstName + " " + item.VisitFrom.LastName + '</a>';
            });
            if (listOfVisitors == "") {
                listOfVisitors += '<a href="/Search/Index/" class="list-group-item list-group-item-action">Nobody likes you</a>';
            }
            listOfVisitors += '<hr />';
            $("#VisitorList").html(listOfVisitors);
            console.log("GetVisitors() => Success");
        },
        error: () => {
            console.log("Error: Unable to load latest visitors.");
        }
    });
}