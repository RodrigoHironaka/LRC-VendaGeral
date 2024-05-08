﻿
//Abrir Modal---------------------------------------------------------------------------
function abrirModal(selector, titulo) {
    $(document).on(TipoClick().toString(), selector, function () {
        var id = $(this).attr("data-id");
        $('table').DataTable().destroy();
        $("#modal").load("/editar/" + id, function () {
            $(".modal-title").html(titulo);
            $("#modal").modal("show");
        });
    });
}
function TipoClick() {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        return 'touchstart';
    } else {
        return 'click';
    }
}
//--------------------------------------------------------------------------------------

//Salvar--------------------------------------------------------------------------------
function enviarAjax(url, method, data, successCallback, errorCallback) {
    $.ajax({
        url: url,
        method: method,
        data: data,
        success: successCallback,
        error: errorCallback
    });
}
function processarRespostaSucesso(retorno) {
    if (retorno == "") {
        location.reload();
    } else {
        $("#modal").html(retorno);
        if ($("#id").val() > 0)
            $(".modal-title").html("ALTERANDO REGISTRO");
        else
            $(".modal-title").html("NOVO REGISTRO");
    }
}
function processarRespostaErro(retorno) {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Houve um erro com o status: ' + retorno.status + '. O log foi gerado, entre em contato com o suporte!',
    });
}
function Salvar(formId, url) {
    var dados = $("#" + formId).serialize();
    enviarAjax(url, 'POST', dados, function (retorno) {
        Swal.fire({
            icon: 'success',
            title: 'Sucesso!',
            text: 'Operação realizada com sucesso.',
            timer: 5000
        }).then(function () {
            location.reload();
        });
    }, function (retorno) {
        processarRespostaErro(retorno);
    });
}
//-------------------------------------------------------------------------------------

//Excluir------------------------------------------------------------------------------
function confirmarExclusao(id, url, sucessoCallback) {
    Swal.fire({
        title: 'Deseja realmente excluir?',
        text: "Este processo não pode ser revertido!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, pode excluir!',
        cancelButtonText: 'Fechar'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                method: "POST",
                url: url,
                data: { id: id },
                success: sucessoCallback,
                error: function () {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Não é possível excluir, por favor inative o registro!',
                    })
                }
            });
        }
    })
}
function Excluir(id, url) {
    confirmarExclusao(id, url, function () {
        Swal.fire({
            icon: 'success',
            title: 'Registro excluído!',
            text: 'O registro foi excluído com sucesso.',
            timer: 5000
        }).then(function () {
            location.reload();
        });
    });
}
//-------------------------------------------------------------------------------------

$(document).ready(function () {
    $('table').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.11.5/i18n/pt-BR.json"
        },
        responsive: true
    });
});

function BuscaCep() {
    $(document).ready(function () {

        function limpa_formulário_cep() {
            // Limpa valores do formulário de cep.
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Endereco_Cep").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                limpa_formulário_cep();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});