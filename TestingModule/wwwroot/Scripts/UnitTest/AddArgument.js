$(function () {
    $('#addArg').click(function () {
        var i = 0;
        var k = i + 1;
        var next = 'Args[' + k + ']';
        while (document.getElementsByName(next).length > 0) {
            i = i + 1;
            k = i + 1;
            next = 'Args[' + k + ']';
        }
        var prevArg = document.getElementsByName("Args[" + i + "]")[0].value;
        if (prevArg == "" ) {
            alert("Должны быть заполнены все поля.");
            return;
        }
        i++;
        var html2Add = "<table class='body-content'>" +
            "<tr>" +
            "<td><input type='text' name='Args[" + i + "]' class='form-control' /></td>" +
            "</tr>" +
            "</table>";
        $('#arguments').append(html2Add);
    })
})