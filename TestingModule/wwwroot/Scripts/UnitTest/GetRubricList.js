$(function () {
    $("#bySubject").change(function () {
        $.getJSON("/Duties/UpdateRubric?idSubject=" + $("#bySubject").val(), function (data) {
            $("#byRubric").html('');
            $("#byRubric").append('<option value=""></option>');
            $.each(data, function (index, rubric) {
                $("#byRubric").append('<option value="' + rubric.id + '">' + rubric.name + '</option>')
            })
        });
    })
})