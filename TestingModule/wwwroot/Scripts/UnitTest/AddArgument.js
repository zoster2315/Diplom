$(function () {
    $('#addArg').click(function () {
        debugger;
        var i = 0;
        var k = i + 1;
        var next = 'NameArgs[' + k + ']';
        while (document.getElementsByName(next).length > 0) {
            i = i + 1;
            k = i + 1;
            next = 'NameArgs[' + k + ']';
        }
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