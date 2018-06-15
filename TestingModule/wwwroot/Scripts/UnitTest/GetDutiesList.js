$(function () {
    $("#byRubric").change(function () {
        $.getJSON("/Duties/GetDuties?idRubric=" + $("#byRubric").val(), function (data) {
            $("#byDuty").html('');
            $("#byDuty").append('<option value=""></option>');
            $.each(data, function (index, rubric) {
                $("#byDuty").append('<option value="' + rubric.id + '">' + rubric.name + '</option>')
            })
        });
    });
})