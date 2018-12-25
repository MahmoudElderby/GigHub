var gigsController = function (attendanceService) {
    var btn;

    var initGigs = function (container) {

        $(container).on("click", ".js-toggle-attendance", toggleAttendance);

        //$(".js-toggle-attendance").click(toggleAttendance);
    };

    var toggleAttendance = function (e) {
        btn = $(e.target);
        var gigId = btn.attr("data-gigid");

        if (btn.hasClass("btn-info"))
            attendanceService.DeleteAttendance(gigId, done, fail);
        else
            attendanceService.CreateAttendance(gigId, done, fail);
    };

    var fail = function () {
        console.log("Something went wrong while processing you request.");
    };

    var done = function () {
        var text = (btn.text() == "Going") ? "Going?" : "Going";
        btn.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    return { Init: initGigs }
}(AttendanceService);