$(document).ready(function () {
 
    buscaUf();
    buscarMaisquente();
    buscarMaisfria();

   
});


function buscaUf() {   
        var selectUf = function (info) {
            debugger;
            var slcUF = "<label class='lblTittle'>Selecione um Estado: </label><select class='col-md-4' id='selectUF' name='selectUF' onchange='buscarCidade()' >";
            slcUF += '<option value="0">Selecione</option>'
            for (var i = 0; i < info.length; i++) {
                slcUF += '<option value="' + info[i].id + '">' + info[i].sigla + '</option>';
            }
            slcUF += "</select>";
            $("#selectUF").html(slcUF);
            $("#selectUF").show();
        }

        $.ajax({
            url: "/Home/GetUF",
            type: "GET",           
        }).done(function (info) {
            selectUf(info)          

        }).fail(function (f) {
            console.log(f);
        });    

}
function buscarCidade() {
   
    var id = $("#selectUF option:selected").val();
    debugger;
    var selectCity = function (info) {
        debugger;
        var slcCity = "<label class='lblTittle'>Selecione sua Cidade: </label><select class='col-md-4' id='selectCITY' name='selectCITY' onchange='buscarPrevisao()' >";
        slcCity += '<option value="0">Selecione</option>'
        for (var i = 0; i < info.length; i++) {
            slcCity += '<option value="' + info[i].id + '">' + info[i].nome + '</option>';
        }
        slcCity += "</select>";
        $("#selectCity").html(slcCity);
        $("#selectCity").show();
    }

    $.ajax({
        url: "/Home/GetCitys/"+id,
        type: "GET",
    }).done(function (info) {
        selectCity(info)

    }).fail(function (f) {
        console.log(f);
    });
}

function buscarPrevisao() {

    var id = $("#selectCITY option:selected").val();
    debugger;
    var previsao = function (info) {
        debugger;
        var conteudo = "";            
      
        for (var i = 0; i < info.length; i++) {           
                 
            conteudo += "<div class='card'><div class='card-header'>" + info[i].verification_Date + "</div><div class='card-body'><div class='row'><div class='col-sm-5'></div></div><br /><div class='row'>" + info[i].city + " / " + info[i].uf + " Temperatura Min:" + info[i].verification_Min + " Temperatura Max:" + info[i].verification_Max + " Condição:" + info[i].conditionClimate + "<div class='col-sm-5'></div></div></div></div>";
           
        }
        $("#content").html(conteudo);
        $("#content").show();
       
    }
    $.ajax({
        url: "/Home/GetNextTemperatures/" + id,
        type: "GET",
    }).done(function (info) {
        previsao(info)

    }).fail(function (f) {
        console.log(f);
    });
}

function buscarMaisquente() {

  
    debugger;
    var previsao = function (info) {
        debugger;
        var conteudo = "";

        for (var i = 0; i < info.length; i++) {

            conteudo += "<div class='row'>" + info[i].city + " / " + info[i].uf + " Temperatura Min:" + info[i].verification_Min + " Temperatura Max:" + info[i].verification_Max + " Condição:" + info[i].conditionClimate + "</div><br/>";

        }
        $("#body-hot").html(conteudo);
        $("#body-hot").show();

    }
    $.ajax({
        url: "/Home/GetHotCity/",
        type: "GET",
    }).done(function (info) {
        previsao(info)

    }).fail(function (f) {
        console.log(f);
    });
}

function buscarMaisfria() {


    debugger;
    var previsao = function (info) {
        debugger;
        var conteudo = "";

        for (var i = 0; i < info.length; i++) {

            conteudo += "<div class='row'>" + info[i].city + " / " + info[i].uf + " Temperatura Min:" + info[i].verification_Min + " Temperatura Max:" + info[i].verification_Max + " Condição:" + info[i].conditionClimate + "</div><br/>";

        }
        $("#body-cold").html(conteudo);
        $("#body-cold").show();

    }
    $.ajax({
        url: "/Home/GetColdCity/",
        type: "GET",
    }).done(function (info) {
        previsao(info)

    }).fail(function (f) {
        console.log(f);
    });
}