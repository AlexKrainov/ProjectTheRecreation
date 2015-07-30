//function onClickRow(row, rowID) {

//    $(".ReportTable > tbody > tr").each(function () {
//        $(this).attr("style", "background-color: white");
//    });

//    $(row).attr("style", "background-color: #91C8FF");

//    var href = $("#DeleteObject").attr("href") + "/" + rowID;
//    $("#DeleteObject").attr("href", href);
//}

//function onDBClickRow(id) {
//    window.location.href = "../Home/Edit/" + id;
//}

function KeyUpTextBoxInteractiveSearch(o) {
    var value = o["value"];

    if (!value || 0 === value.length) {
        $(".ReportTable > tbody > tr").each(function () {
            $(this).attr("style", "");
        })
    }
    else {
        $(".ReportTable > tbody > tr").each(function () {
            var td = $(this).find("td");
            HideRow($(this), td, value);
        });
    }
}

function KeyUpTextBoxGlobalSearch() {
    ChangeColorButtonGlobalSearch();
}

function ChangeColorButtonGlobalSearch() {
    var value = $("#GlobalShearchTextBox").val();

    if (!value || 0 === value.length) {
        $("#ButtonGlobalShearch").attr("style", "background-color: white");
    }
    else {
        $("#ButtonGlobalShearch").attr("style", "background-color: gray;");
    }
}

$(document).ready(function () {

    ChangeColorButtonGlobalSearch();

    $(".ReportTable > tbody").on("click", "td", function () {

        var colIndex = $(this).parent().children().index($(this));
        if (colIndex == 11)
            return true;
        //Фильтруем по колонке 
        var value = $(this).html();
        var o = document.getElementById("SearchTextBox");
        o["value"] = value;

        $(".ReportTable > tbody > tr").each(function () {
            var arrayTD = $(this).find("td");

            var boo = false;

            $(arrayTD).each(function () {
                if (colIndex === $(this).parent().children().index($(this)))
                    if ($(this).html().toLowerCase().indexOf(value.toLowerCase()) != -1) {
                        boo = true;
                    }
            });

            if (!boo)
                $(this).css("display", "none");
            else
                $(this).attr("style", "");

        });
    });

});

function HideRow(row, arrayTD, value) {
    var index = 0;

    $(arrayTD).each(function () {
        if ($(this).html().toLowerCase().indexOf(value.toLowerCase()) != -1) {
            index = index + 1;
        }
    });

    if (index === 0)
        row.css("display", "none");
    else
        row.attr("style", "");
}

function RemoveAllFilter() {
    var o = document.getElementById("SearchTextBox");

    if (!o["value"] || 0 === o["value"].length) {
        document.getElementById("ResetFilter").click();
        //ChangeColorButtonGlobalSearch();
        //window.location.reload("./Home/Index");
    }

    o["value"] = "";

    $(".ReportTable > tbody > tr").each(function () {
        $(this).attr("style", "");
    })


}

function ShowShearchDialog() {
    var div = $("[id*=GlobalSearchDialog").css("display");
    if (div != "block")
        $("[id*=GlobalSearchDialog").css("display", "block");
    else
        $("[id*=GlobalSearchDialog").css("display", "none");
}



//for (var i = 0; i < td.length; i++) {
//    console.log(td[i].innerHTML());
//var bool = false;
//if (td[i].html.toString().toLowerCase().indexOf(o.value.toLowerCase()) == -1) {

//}
// }
//    $('.ReportTable > tbody > tr:has(td:contains(' + value + '))').hide()
//    $(this).siblings(value).css("visibility","hidden");
//         console.log(td);

//    $(td).each(function () {
//        if ($(this).html().toLowerCase().indexOf(value.toLowerCase()) != -1) {
//            index = index + 1;
//        }
//    });

//    if (index === 0)
//        $(this).css("display", "none");
//    else
//        $(this).attr("style", "");

//$(".ReportTable").on("click", "td", function () {
// var rowIndex = $(this).parent().index();
// var colIndex = $(this).parent().children().index($(this));