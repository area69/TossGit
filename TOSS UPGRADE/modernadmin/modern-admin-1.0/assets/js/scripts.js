function LoadDataTable() {
    $('.zero-configuration').DataTable({
        destroy: true,
        "aLengthMenu": [[5, 10, 25, -1], [5, 10, 25, "All"]],
        "iDisplayLength": 5,
        drawCallback: function () {
            LoadPopOver();
        }
    });
}
function LoadICheck() {
    $('.skin-square input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%'
    });
    $(".select2").select2();
}
function LoadPopOver() {
    $(".popover-trigger").popover({
        html: true,
        container: 'body',
    });
}
function PopoveLostFocus() {
    $('[data-toggle="popover"]').popover('hide');
}

function LoadPickDatetime() {
    $('.pickadate').pickadate({
        format: 'mmmm dd, yyyy',
        formatSubmit: 'yyyy/MM/dd',
        onStart: function () {
            var d = new Date();
            var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

            var todayDate = months[d.getMonth()] + " " + d.getDate() + ", " + d.getFullYear();

            $(this).val(todayDate);
        }
    });
}


//ANGULAR
function AngularGlobalFunctions(ControllerName, ActionName, IDParams) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "(" + IDParams + ");");
    })
}

function AngularGlobalEdit(ControllerName, ActionName, IDParams) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "(" + IDParams + ");");
    })
}

function AngularGlobalView(ControllerName, ActionName, IDParams) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "(" + IDParams + ");");
    })
}

function AngularGlobalDelete(IDParams, ActionName, ControllerName,) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "(" + IDParams + ");");
    })
}

function AngularGlobalDeleteTwoFunc(IDParams, ActionName1, ControllerName, ActionName2) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "(" + IDParams + ");");
        eval("scope." + ActionName2 + "();");
    })
}

function AngularGlobalAlertsCalling(ControllerName, ActionName, ModalName, SuccessMess) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "();");
        eval("$('#" + ModalName + "').modal('toggle');");
        swalSuccess("Success", SuccessMess);
    });
}

function AngularGlobalAlertsCallingtwoAction(ControllerName, ActionName1, ActionName2, ModalName, SuccessMess) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "();");
        eval("scope." + ActionName2 + "();");
        eval("$('#" + ModalName + "').modal('toggle');");
        swalSuccess("Success", SuccessMess);
    });
}

function AngularGlobalAlertsCallingNoSuccessMess(ControllerName, ActionName, ModalName) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "();");
        eval("$('#" + ModalName + "').modal('toggle');");
    });

}

function AngularGlobalAlertsCallingNoSuccessMessParam(ControllerName, ActionName, ModalName,IdParams) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "(" + IdParams+");");
        eval("$('#" + ModalName + "').modal('toggle');");
    });

}

function AngularGlobalAlertsCallingNoModalandSuccess(ControllerName, ActionName, IDParams) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "(" + IDParams + ");");
    });

}

function AngularGlobalAlertsCallingNoModal(ControllerName, ActionName,  SuccessMess) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName + "();");
        swalSuccess("Success", SuccessMess);
    });

}

function AngularGlobalAlertsCallingNoModalNoSuccessWithDoubleFunc(ControllerName, ActionName1, ActionName2) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "();");
        eval("scope." + ActionName2 + "();");
    });
}

function AngularGlobalAlertsCallingNoModalNoSuccessWithTripleFunc(ControllerName, ActionName1, ActionName2, ActionName3) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "();");
        eval("scope." + ActionName2 + "();");
        eval("scope." + ActionName3 + "();");
    });
}
function AngularGlobalAlertsCallingNoModalNoSuccessWith6Func(ControllerName, ActionName1, ActionName2, ActionName3, ActionName4, ActionName5, ActionName6) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "();");
        eval("scope." + ActionName2 + "();");
        eval("scope." + ActionName3 + "();");
        eval("scope." + ActionName4 + "();");
        eval("scope." + ActionName5 + "();");
        eval("scope." + ActionName6 + "();");
    });
}
function AngularGlobalAlertsCallingNoModalSuccessWithTwoFunc(ControllerName, ActionName1, ActionName2, SuccessMess) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "();");
        eval("scope." + ActionName2 + "();");
        swalSuccess("Success", SuccessMess);
    });
}

function AngularGlobalAlertsCallingNoModalWithDoubleFunc(ControllerName, ActionName1, ActionName2, SuccessMess) {
    var scope = angular.element(document.getElementById(ControllerName)).scope();
    scope.$apply(function () {
        eval("scope." + ActionName1 + "();");
        eval("scope." + ActionName2 + "();");
        swalSuccess("Success", SuccessMess);
    });
}

function AngularGlobalSameCtrlNoModalWithDoubleFunction(ControllerName, ActionName1, ActionName2) {
    var scope1 = angular.element(document.getElementById(ControllerName)).scope();
    scope1.$apply(function () {
        eval("scope1." + ActionName1 + "();");
        eval("scope1." + ActionName2 + "();");
    });
}

function swalConfirmation(questionMessage, confirmationMessage, successTitle, action) {
    swal({
        title: questionMessage,
        text: confirmationMessage,
        icon: "warning",
        buttons: {
            cancel: {
                text: "Cancel",
                value: null,
                visible: true,
                className: "btn-secondary",
                closeModal: true
            },
            confirm: {
                text: "Yes, I am sure",
                value: true,
                visible: true,
                className: "btn-c",
                closeModal: false
            }
        }
    })
        .then((willDelete) => {
            if (willDelete) {
                eval(action)
                swalSuccess(successTitle)

            } else {

            }
        });
}

function swalSuccess(title, message) {
    swal({
        title: title,
        text: message,
        icon: 'success',
        //timer: 3000,
        button: {
            text: 'Close',
            className: 'btn-secondary'
        }
    });
}

function swalError(title, message) {
    swal({
        title: title,
        text: message,
        icon: 'error',
        button: {
            text: 'Close',
            className: 'btn-secondary'
        }
    });
}

function OnFailure(response) {
    swalError("Error", "Error occured.");
}

(function (window, undefined) {
    'use strict';

    /*
    NOTE:
    ------
    PLACE HERE YOUR OWN JAVASCRIPT CODE IF NEEDED
    WE WILL RELEASE FUTURE UPDATES SO IN ORDER TO NOT OVERWRITE YOUR JAVASCRIPT CODE PLEASE CONSIDER WRITING YOUR SCRIPT HERE.  */


    $(function () {


    });



})(window);