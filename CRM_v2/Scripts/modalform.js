

$(document).ready(function () {
    $('button#privacylink').click(function () {
                event.preventDefault();
                var url = $(this).attr('href');               
                //var url = "/ReInitStatistica/ReInitTbl";
                $.ajax({
                    url: url,
                    type: "POST",
                    data: {
                        "__RequestVerificationToken":
                        $("input[name=__RequestVerificationToken]").val()
                    },
                    success: function (result) {
                        if (result.success == false) {
                            alert(result.errors[0]);
                        }
                        else if (result.success == true) {
                            
                            $('#myModal').modal('hide');
                        }
                    }
                });
    });
});

$(document).ready(function () {
    $('button#privacylink1').click(function () {
        event.preventDefault();
        var url = $(this).attr('href');
        //var url = "/BackupStatistica/CopyTbl";
        $.ajax({
            url: url,
            type: "POST",
            data: {
                "__RequestVerificationToken":
                $("input[name=__RequestVerificationToken]").val()
            },
            success: function (result) {
                if (result.success == false) {
                    alert(result.errors[0]);
                }
                else if (result.success == true) {

                    $('#myModal1').modal('hide');
                }
            }
        });
    });
});



