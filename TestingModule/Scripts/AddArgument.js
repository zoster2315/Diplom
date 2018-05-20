$(function () {
    $('#addArg').click(function () {
        var i = 0;
        var nameControl = document.getElementsByName("NameArgs[" + i + "]")[0];
        do {
            i++
            var nameControl = document.getElementsByName("NameArgs[" + i + "]")[0];
        }
        while (nameControl != null)
        var prevName = document.getElementsByName("NameArgs[" + i + "]")[0].value;
        var prevArg = document.getElementsByName("Args[" + i + "]")[0].value;
        if (prevArg == "" || prevName == "") {
            alert("alert");
            return;
        }
        i++;
        var html2Add = "<table class='body-content'>" +
            "<tr>" +
            "<td><input type='text' name='NameArgs[" + i + "]' class='form-control' /></td>" +
            "<td><input type='text' name='Args[" + i + "]' class='form-control' /></td>" +
            "</tr>" +
            "</table>";
        $('#arguments').append(html2Add);
    })
})