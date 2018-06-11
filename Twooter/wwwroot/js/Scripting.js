$(document).ready(); {
    $("#tekstgebied").keyup(function () {
        $("#counter").val(256 - $(this).val().length);
    });
}

$(".Hide").collapse({
    toggle: true
})
$(".btn-primary").click(function () {
    $(".Hide").collapse('toggle');
    if ($(".btn-primary").text() === "Favorieten") {
        $(".btn-primary").html("Alles");
    }
    else {
        $(".btn-primary").html("Favorieten"); 
    }
});

function favorite(object) {
    var myData = $(object).attr("id");
    if ($(object).attr("src") === "/images/Foto/LeegSter.png") {
        $.ajax({
            url: "/Twoot/Favoriet/" + myData,
            type: "GET",
            success: function () {
                var newSrc = "/images/Foto/VolSter.png";
                $(object).attr('src', newSrc);

                var parent1 = $(object).parent();
                var parent2 = $(parent1).parent();
                $(parent2).attr("class", "Show twoot");
            },
            error: function () {
                alert("Faal");
            }
        });
    }
    else {
        $.ajax({
            url: "/Twoot/UnFavoriet/" + myData,
            type: "GET",
            success: function () {
                newSrc = "/images/Foto/LeegSter.png";
                $(object).attr('src', newSrc);

                var parent1 = $(object).parent();
                var parent2 = $(parent1).parent();
                $(parent2).attr("class", "Hide collapse in twoot");
            },
            error: function () {
                alert("Faal");
            }
        });
        
    }
}

function volg(object) {
    var myData = $(object).attr("id");
    if ($(object).attr("src") === "/images/Foto/LeegHart.png") {
        $.ajax({
            url: "/Profiel/Volg/" + myData,
            type: "GET",
            success: function () {
                var newSrc = "/images/Foto/VolHart.png";
                $(object).attr('src', newSrc);
            },
            error: function () {
                alert("Faal");
            }
        });
    }
    else {
        $.ajax({
            url: "/Profiel/UnVolg/" + myData,
            type: "GET",
            success: function () {
                newSrc = "/images/Foto/LeegHart.png";
                $(object).attr('src', newSrc);
            },
            error: function () {
                alert("Faal");
            }
        });
    }
}


$("#filterToevoegen").click(function () {
    var woord = $("#FilterWoord").val();

    var object = new Object();
    object.Woord = woord;

    $.ajax({
        url: "/Filter/Lijst/?filter=" + woord,
        type: "POST",
        success: function (data) {
            
        },
        error: function () {
 
        }
    });
});

function filterToevoegen() {
    var woord = $("#FilterWoord").val();



    $.ajax({
        url: "/Filter/VoegToe/",
        data: JSON.stringify(object),
        type: "POST",
        success: function () {
            alert("Eerste ajax succes");
        },
        error: function () {
            alert("Eerste ajax faal");
        }
    });
}

