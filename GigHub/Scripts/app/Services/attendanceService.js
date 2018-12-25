var AttendanceService = function () {

    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
            .done(done)
            .fail(fail);
    };

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
                url: '/api/attendances?' + $.param({ gigId: gigId }),
                type: 'DELETE'
            })
            .done(done)
            .fail(fail);
    };


    return {
        CreateAttendance: createAttendance,
        DeleteAttendance: deleteAttendance
    }

}();
