$('#btnListarTarefas').click(function (e) {
    CarregarTarefas();
});

$('#btnTestarConexao').click(function (e) {
    TestarConexao();
});

$('#btnInserirTarefa').click(function (e) {
    CadastrarTarefa(e);
    $('#modalCadastroTarefa').modal('hide');
});

$('#btnConcluirTarefa').click(function (e) { 
    FinalizarTarefa();
});

$('#btnAlterarTarefa').click(function (e) {
    AlterarTarefa(e);
});


function CadastrarTarefa(pEvent) {
    pEvent.preventDefault();
    var formData = {
        'titulo': $('input[name=titulo]').val(),
        'descricao': $('textarea[name=descricao]').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Task/InserirTarefa',
        data: formData,
        dataType: 'json'
        
    }).done(function (data) {
        Alertar(data.statusCodigo, data.mensagem);
        CarregarTarefas();
    });
}

function Alertar(tipo, mensagem) {
    $('#divAlertOK').hide();
    $('#divAlertNOK').hide();
    if (tipo === 'OK') {
        $('#divAlertOK').html('<p>' + mensagem + '</p>');
        $('#divAlertOK').show();
    } else {
        $('#divAlertNOK').html('<p>' + mensagem + '</p>');
        $('#divAlertNOK').show();
    }
}

function TestarConexao() {
    $.ajax({
        url: "/task/TestarConexao",
        type: "POST",
        data: null,
        dataType: "JSON",
        success: function (retornoSucc) {
            if (retornoSucc.statusCodigo === "OK") {
                Alertar(retornoSucc.statusCodigo, retornoSucc.mensagem);
            } else {
                Alertar(retornoSucc.statusCodigo, retornoSucc.mensagem);
            }

        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status === 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status === 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            Alertar('NOK', 'Ocorreram problemas: \n=>' + msg);
        }
    });
}

function CarregarTarefas() {

    $('#taskList').show();

    $.ajax({
        url: "/task/ListarTarefas",
        type: "POST",
        data: null,
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (objetoRetorno) {
            if (Object.keys(objetoRetorno).length === 0) {
                $('#tblListContent').html('Não há registros para serem listados');
            }
            var aux = '';
            aux += '<tr>';
            aux += '<th>ID</th>';
            aux += '<th>Titulo</th>';
            aux += '<th>Descricao</th>';
            aux += '<th>STATUS</th>';
            //aux += '<th>Data Criação</th>';
            //aux += '<th>Data Alteração</th>';
            //aux += '<th>Data Exclusão</th>';
            aux += '<th></th>';
            aux += '<th></th>';
            aux += '</tr>';

            $(objetoRetorno).each(function (i) {
                aux += '<tr>';
                aux += '<td>' + objetoRetorno[i].Id + '</td>';
                aux += '<td>' + objetoRetorno[i].Titulo + '</td>';
                aux += '<td>' + objetoRetorno[i].Descricao + '</td>';
                aux += '<td>' + objetoRetorno[i].Status + '</td>';
                //aux += '<td>' + objetoRetorno[i].DataCriacao + '</td>';
                //aux += '<td>' + objetoRetorno[i].DataAtualizacao + '</td>';
                //aux += '<td>' + objetoRetorno[i].DataExclusao + '</td>';
                aux += '<td><a href="/task/VisualizarTarefa?pid=' + objetoRetorno[i].Id + '" class="btn btn-primary btnVisual">Visualizar</a></td>';
                if (objetoRetorno[i].Status != 'F') {
                    aux += '<td><a href="/task/excluir?pid=' + objetoRetorno[i].Id + '" class="btn btn-danger btnExc">Excluir</a></td>';
                }
                aux += '</tr>';
                $('#tblListContent').html(aux);
            });
        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status === 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status === 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            Alertar('NOK', 'Ocorreram problemas: \n=>' + msg);
        }
    });

}

function FinalizarTarefa(){
    $.ajax({
        url: "/task/MarcarFinalizada",
        type: "POST",
        data: { pId: $('#txtId').val() },
        dataType: "JSON",
        success: function (retornoSucc) {
            if (retornoSucc.statusCodigo === 'OK') {
                Alertar(retornoSucc.statusCodigo, retornoSucc.mensagem);
                setTimeout(function () { window.location = '/task'; }, 1900);
                
            } else {
                Alertar(retornoSucc.statusCodigo, retornoSucc.mensagem);
            }

        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status == 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception == 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception == 'timeout') {
                msg = 'Time out error.';
            } else if (exception == 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            Alertar('NOK', 'Ocorreram problemas: \n=>' + msg);
        }
    });
}

function AlterarTarefa(pEvent) {
    pEvent.preventDefault();
    var formData = {
        'id': $('input[name=Id]').val(),
        'titulo': $('input[name=Titulo]').val(),
        'descricao': $('input[name=Descricao]').val()
    };

    $.ajax({
        type: 'POST',
        url: '/Task/EditarTarefa',
        data: formData,
        dataType: 'json'

    }).done(function (data) {
        Alertar(data.statusCodigo, data.mensagem);
        setTimeout(function () { window.location = '/task/VisualizarTarefa?pId=' + $('input[name=Id]').val(); }, 2500);
    });
}