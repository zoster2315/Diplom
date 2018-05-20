$(function () {
    $('#addVal').click(function () {
        var i = 0;
        var k = i + 1;
        var next = 'NameValue[' + k + ']';
        while (document.getElementsByName(next).length > 0) {
            i = i + 1;
            k = i + 1;
            next = 'NameValue[' + k + ']';
        }

        var prevName = document.getElementsByName("NameValue[" + i + "]")[0].value;
        var prevArg = document.getElementsByName("Values[" + i + "]")[0].value;
        if (prevArg == "" || prevName == "") {
            alert("alert");
            return;
        }
        i++;
        var html2Add = "<table class='body-content'>" +
            "<tr>" +
            "<td><input type='text' name='NameValue[" + i + "]' class='form-control'/></td>" +
            "<td><input type='text' name='Values[" + i + "]' class='form-control'/></td>" +
            "</tr>" +
            "</table>";
        $('#values').append(html2Add);
    })
})