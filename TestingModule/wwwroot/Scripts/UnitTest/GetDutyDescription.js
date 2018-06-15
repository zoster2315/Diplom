$(function () {
    $("#byDuty").change(function () {
        $.getJSON("/UnitTests/GetDescription?DutyID=" + $("#byDuty").val(), function (data) {
            $("#DutyDescr").text(data);
        });
    })
})